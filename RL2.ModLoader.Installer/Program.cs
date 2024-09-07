using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using NativeFileDialogSharp;

namespace RL2.ModLoader.Installer;

public class Program
{
	static string CurrentPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

	[STAThread]
	static void Main(string[] args) {
		Console.WriteLine("Welcome to the RL2.ModLoader installer");
		Console.WriteLine("Choose your games installation directory");
		Thread.Sleep(500);

		DialogResult result = Dialog.FolderPicker();
		string DataPath = result.Path + "\\Rogue Legacy 2_Data";
		string ManagedPath = DataPath + "\\Managed";

		if (!Directory.Exists(DataPath) || !Directory.Exists(ManagedPath)) {
			Console.WriteLine("The provided path is incorrect");
			return;
		}

		// Copy RL2.ModLoader necessities
		Copy("RL2.ModLoader.xml", ManagedPath);
		Copy("RL2.ModLoader.dll", ManagedPath);
		Copy("RL2.ModLoader.pdb", ManagedPath);
		Copy("RuntimeInitializeOnLoads.json", DataPath);
		Copy("ScriptingAssemblies.json", DataPath);

		// Copy MonoMod files
		string[] MonoModFilenames = [
			"Mono.Cecil.dll",
			"Mono.Cecil.Rocks.dll",
			"MonoMod.Common.dll",
			"MonoMod.Common.xml",
			"MonoMod.RuntimeDetour.dll",
			"MonoMod.RuntimeDetour.xml",
			"MonoMod.Utils.dll",
			"MonoMod.Utils.xml"
		];

		foreach (string MonoModFilename in MonoModFilenames) {
			File.Copy(CurrentPath + "\\MonoMod\\" + MonoModFilename, ManagedPath + "\\" + MonoModFilename, true);
			Console.WriteLine("Copying " + MonoModFilename);
		}

		// Finish up
		Console.WriteLine("\n\nInstallation complete. Press any key to exit...");
		Console.ReadKey();
	}

	private static void Copy(string sourceFile, string destPath)
	{
        File.Copy(CurrentPath + "\\" + sourceFile, destPath + "\\" +  sourceFile, true);
		Console.WriteLine("Copying " + sourceFile);
    }
}
