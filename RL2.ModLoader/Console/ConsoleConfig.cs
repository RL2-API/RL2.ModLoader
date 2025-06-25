using System;

namespace RL2.ModLoader;

/// <summary>
/// Represents console_config.json
/// </summary>
[Serializable]
public class ConsoleConfig {
	/// <summary>
	/// Determines whether commands need to be called with a `/` at the beginning
	/// </summary>
	public bool CommandSlashRequired = true;

	/// <summary>
	/// Determines whether the command line will be cleared when closing the window
	/// </summary>
	public bool ClearContentOnClose = true;
}