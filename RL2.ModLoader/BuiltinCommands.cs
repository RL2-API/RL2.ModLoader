using Rewired.Utils.Libraries.TinyJson;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RL2.ModLoader;

/// <summary>
/// Contains all builtin commands
/// </summary>
public static class BuiltinCommands 
{
	/// <summary>
	/// Displays a list of all available commands
	/// </summary>
	/// <param name="args"></param>
	[Command("help")]
	public static void Help(string[] args) {
		if (args.Length == 0) {
			StringBuilder builder = new();
			builder.AppendLine("Available commands: ");
			foreach (string command in CommandManager.Commands.Keys) {
				builder.AppendFormat("{0}; ", command);
			}
			builder.AppendLine("\nFor more info call the help command with the desired command's name as an argument");
			builder.AppendLine("\nExample usage:");
			builder.Append("\t/help console-config");
			ModLoader.Log(builder.ToString());
			return;
		}

		if (args.Length == 1) {
			if (CommandManager.CommandHelp.TryGetValue(args[0], out string help)) {
				ModLoader.Log(help);
				return;
			} else {
				ModLoader.Log($"Command {args[0]} does not provide a help text");
			}
			return;
		}
	}

	/// <summary>
	/// Sets specific configs for the console window
	/// </summary>
	/// <param name="args"></param>
	[Command("console-config")]
	[CommandHelp(
		$"""
		"console-config" command help. Available sub commands:
			slash - toggles the requirement of the slash at the start of commands
			clear - toggles clearing on close
			
		Example usage:
			/console-config slash
			/console-config clear
		"""
	)]
	public static void ConsoleConfig(string[] args) {
		if (args.Length == 0) {
			ModLoader.Log("");
			return;
		}

		if (args[0] == "slash") {
			Console.Config.CommandSlashRequired = !Console.Config.CommandSlashRequired;
			ModLoader.Log($"CommandSlashRequired = {Console.Config.CommandSlashRequired}");
		}
		else if (args[0] == "clear") {
			Console.Config.ClearContentOnClose = !Console.Config.ClearContentOnClose;
			ModLoader.Log($"ClearContentOnClose = {Console.Config.ClearContentOnClose}");
		}
		else {
			ModLoader.Log($"Subcommand {args[0]} not found. \nUse the \"help console-config\" command to receive info on how to use it");
			return;
		}

		File.WriteAllText(Console.ConfigPath, JsonWriter.ToJson(Console.Config).Prettify());
	}

	/// <summary>
	/// Writes the loaded mods to the logs and console
	/// </summary>
	/// <param name="args"></param>	
	[Command("show-mods")]
	public static void ShowInstalledMods(string[] args) {
		List<string> loaded = [];
		foreach (KeyValuePair<string, SemVersion> entry in ModLoader.LoadedModNamesToVersions) {
			loaded.Add($"{entry.Key} v{entry.Value}");
		}
		ModLoader.Log($"Installed mods: {string.Join(" | ", loaded)}");
	}

	/// <summary>
	/// Creates base source files for a new independent mod
	/// </summary>
	/// <param name="args"></param>
	[Command("create-mod")]
	[CommandHelp(
		"""
		"create-mod" command help.
		Usage scheme:
			create-mod [ModName - required] [Author - optional]

		Example usage:
			create-mod ExampleMod RL2.API_Team
			create-mod TestMod
		
		"""
	)]
	public static void CreateMod(string[] args) {
		if (args.Length == 0) {
			ModLoader.Log("No overload of \"create-mod\" takes in 0 arguments. \nCorrecct usage: \"/create-mod [ModName - required] [Author - optional]\"");
			return;
		}

		string modName = args[0];
		if (string.IsNullOrEmpty(modName)) {
			ModLoader.Log("Argument \"modName\" is required for command \"create-mod\". \nCorrecct usage: \"/create-mod [ModName - required] [Author - optional]\"");
		}

		string author = args.Length > 1 ? args[1] : "";

		string newModPath = ModLoader.ModPath + $"\\{modName}";
		if (Directory.Exists(newModPath)) {
			ModLoader.Log($"A mod with this name: {modName} already exists in your Mods directory");
			return;
		}

		newModPath = ModLoader.ModSources + $"\\{modName}";
		if (Directory.Exists(newModPath)) {
			ModLoader.Log($"A mod with this name: {modName} already exists in your ModSources directory");
			return;
		}

		Directory.CreateDirectory(newModPath);

		EnsureTargetsFile();
		CreateCsproj(newModPath + $"\\{modName}.csproj");
		CreateModManifest(modName, author, newModPath + $"\\{modName}.mod.json");
		CreateModEntrypointFile(modName, newModPath + $"\\{modName}.cs");
		CreateLaunchSettingsJson(newModPath);

		ModLoader.Log($"Mod {modName} was created");
	}

	/// <summary>
	/// Creates a new RL2.Mods.targets file if needed
	/// </summary>
	public static void EnsureTargetsFile() {
		string targetsPath = ModLoader.ModSources + "\\RL2.ModLoader.targets";
		if (File.Exists(targetsPath)) return;

		string dataPath = UnityEngine.Application.dataPath.Replace("/", "\\");
		File.WriteAllText(targetsPath,
			$"""
			<Project ToolsVersion="14.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
				<!-- Path properties -->
				<PropertyGroup>
					<RL2_RootPath>{dataPath.Substring(0, dataPath.LastIndexOf('\\'))}\</RL2_RootPath>
					<RL2_DataPath>{dataPath}\</RL2_DataPath>
					<RL2_LibsPath>{dataPath}\Managed\</RL2_LibsPath>
					<RL2_ModsPath>{ModLoader.ModPath}\</RL2_ModsPath>
					<RL2_ModSourcesPath>{ModLoader.ModSources}\</RL2_ModSourcesPath>
				</PropertyGroup>

				<!-- Default configuration -->
				<PropertyGroup>
					<GenerateAssemblyInfo>False</GenerateAssemblyInfo>
					<Nullable>enable</Nullable>
					<LangVersion>latest</LangVersion>
					<AppendTargetFrameworkToOutputPath>false</AppendTargetFrameworkToOutputPath>
					<AppendRuntimeIdentifierToOutputPath>false</AppendRuntimeIdentifierToOutputPath>
				</PropertyGroup>

				<!-- Default mod references -->
				<ItemGroup>
					<Reference Include="$(RL2_LibsPath)*.dll">
						<Private>false</Private>
					</Reference>
				</ItemGroup>

				<!-- Utility build task -->
				<Target Name="CopyToMods" AfterTargets="PostBuildEvent">
					<ItemGroup>
						<Compiled Include="$(TargetDir)$(AssemblyName)*" />
						<ModJson Include="$(ProjectDir)*.mod.json" />
						<JsonData Include="$(ProjectDir)**\*.json" Exclude="$(ProjectDir)**\obj\**\*.*;$(ProjectDir)**\Properties\**\*.*"/>
						<PNGs Include="$(ProjectDir)**\*.png"/>
					</ItemGroup>

					<Message Text="Copying $(AssemblyName) to the Mods directory..." Importance="high" />
					<Copy SourceFiles="@(Compiled)" DestinationFolder="$(RL2_ModsPath)\$(AssemblyName)" />
					<Copy SourceFiles="@(ModJson)" DestinationFolder="$(RL2_ModsPath)\$(AssemblyName)" />
					<Copy SourceFiles="@(JsonData)" DestinationFolder="$(RL2_ModsPath)\$(AssemblyName)\%(RecursiveDir)" />
					<Copy SourceFiles="@(PNGs)" DestinationFolder="$(RL2_ModsPath)\$(AssemblyName)\%(RecursiveDir)" />

					<Message Text="Preparing $(AssemblyName)'s package folder..." Importance="high" />
					<Copy SourceFiles="@(Compiled)" DestinationFolder="$(TargetDir)\$(AssemblyName)" />
					<Copy SourceFiles="@(ModJson)" DestinationFolder="$(TargetDir)\$(AssemblyName)" />
					<Copy SourceFiles="@(JsonData)" DestinationFolder="$(TargetDir)\$(AssemblyName)\%(RecursiveDir)" />
					<Copy SourceFiles="@(PNGs)" DestinationFolder="$(TargetDir)\$(AssemblyName)\%(RecursiveDir)" />
				</Target>
			</Project>
			"""
		);
	}

	/// <summary>
	/// Creates a new independent mod .csproj file
	/// </summary>
	/// <param name="path">Full file path with extension</param>
	public static void CreateCsproj(string path) {
		File.WriteAllText(path,
			$"""
			<Project Sdk="Microsoft.NET.Sdk">
				<Import Project="../RL2.ModLoader.targets" />

				<PropertyGroup>
					<TargetFramework>net48</TargetFramework>
				</PropertyGroup>

				<ItemGroup>
				</ItemGroup>

			</Project>
			"""
		);
	}

	/// <summary>
	/// Creates a new .mod.json file
	/// </summary>
	/// <param name="modName"></param>
	/// <param name="author"></param>
	/// <param name="newModPath">Full file path with extension</param>
	public static void CreateModManifest(string modName, string author, string newModPath) {
		ModManifest modManifest = new ModManifest() {
			Name = modName,
			Author = author,
			Version = "1.0.0",
			ModAssembly = $"{modName}.dll",
			LoadAfter = []
		};

		File.WriteAllText(newModPath, JsonWriter.ToJson(modManifest).Prettify());
	}

	/// <summary>
	/// Creates a new .cs file for the independent mod
	/// </summary>
	/// <param name="modName"></param>
	/// <param name="path">Full file path with extension</param>
	public static void CreateModEntrypointFile(string modName, string path) {
		File.WriteAllText(path,
			$$"""
			using RL2.ModLoader;

			namespace {{modName}};

			[ModEntrypoint]
			public class {{modName}}
			{
				public {{modName}}() { }
			}
			"""
		);
	}


	/// <summary>
	/// Creates the launchSettings.json file, allowing users to launch their mod from VS
	/// </summary>
	/// <param name="path"></param>
	public static void CreateLaunchSettingsJson(string path) {
		Directory.CreateDirectory(path + "\\Properties");
		File.WriteAllText(path + "\\Properties\\launchSettings.json",
			"""
			{
				"profiles" : {
					"Rogue Legacy 2 (Steam)" : {
						"commandName": "Executable",
						"executablePath": "$(RL2_RootPath)Rogue Legacy 2.exe",
						"commandLineArgs": "",
						"workingDirectory": "$(RL2_RootPath)",
						"nativeDebugging": true
					}
				}
			}
			"""
		);
	}
}