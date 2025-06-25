using MonoMod.RuntimeDetour;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace RL2.ModLoader;

public partial class ModLoader
{
	/// <summary>
	/// Displays loaded mods
	/// </summary>
	public static Hook VersionDisplay = new Hook(
		typeof(System_EV).GetMethod("GetVersionString", BindingFlags.Public | BindingFlags.Static),
		(Func<string> orig) => {
			List<string> loaded = [];
			foreach (KeyValuePair<string, SemVersion> entry in LoadedModNamesToVersions) {
				loaded.Add($"{entry.Key} v{entry.Value}");
			}
			return orig() + "\nRL2.ModLoader v." + ModLoaderVersion.ToString() + "\n" + string.Join("\n", loaded);
		},
		new HookConfig() {
			ID = "RL2.ModLoader::VersionDisplay"
		}
	);
}