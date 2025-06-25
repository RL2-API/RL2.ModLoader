using System;

namespace RL2.ModLoader;

/// <summary>
/// Provides additional help for the command
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class CommandHelpAttribute : Attribute
{
	/// <summary>
	/// Block access to the parameterless constructor
	/// </summary>
	private CommandHelpAttribute() { }

	/// <summary>
	/// The name of the command
	/// </summary>
	public string HelpText = "";

	/// <summary>
	/// Register a help text for a command
	/// </summary>
	/// <param name="helpText"></param>
	/// <remarks>The marked method must be <see langword="static"/>!</remarks>
	public CommandHelpAttribute(string helpText) {
		HelpText = helpText;
	}
}