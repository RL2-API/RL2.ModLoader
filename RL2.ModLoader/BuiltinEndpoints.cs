using MonoMod.RuntimeDetour;
using System;
using System.Reflection;

namespace RL2.ModLoader;

public partial class ModLoader
{
	/// <summary>
	/// Provides an endpoint for mods not using the RL2.API by trigering the OnLoad event
	/// </summary>
	public static Hook OnGameLoad = new Hook(
		typeof(OnGameLoadManager).GetMethod("Run", BindingFlags.NonPublic | BindingFlags.Static),
		(Action orig) => {
			orig();
			OnLoad?.Invoke();
		},
		new HookConfig() {
			ID = "RL2.ModLoader::OnGameLoad"
		}
	);

	/// <summary>
	/// Provides an endpoint for mods not using the RL2.API by trigering the OnUnload event
	/// </summary>
	public static Hook OnGameUnload = new Hook(
		typeof(GameManager).GetMethod("OnApplicationQuit", BindingFlags.NonPublic | BindingFlags.Instance),
		(Action<GameManager> orig, GameManager self) => {
			Log("Unloading mods...");
			OnUnload?.Invoke();
			orig(self);
		},
		new HookConfig() {
			ID = "RL2.ModLoader::OnGameUnload"
		}
	);

	/// <summary>
	/// Event ran after the whole game is loaded
	/// </summary>
	public static event Action? OnLoad;

	/// <summary>
	/// Event ran before the whole game is unloaded
	/// </summary>
	public static event Action? OnUnload;
}