using System;
using Rewired.Utils.Libraries.TinyJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RL2.ModLoader;

/// <summary>
/// A component representing the in game console
/// </summary>
public class Console : MonoBehaviour
{
	/// <summary>
	/// Console config
	/// </summary>
	public static ConsoleConfig Config = new();
	
	/// <summary>
	/// Console config path
	/// </summary>
	public static readonly string ConfigPath = ModLoader.ModPath + "\\conosle_config.json";

	private bool visible = false;
	private List<string> history = [];
	private int historyIndex = 0;

	private uint consoleLines = 15;  // number of messages to keep
	private Queue logQueue = new Queue();
	private string command = string.Empty;
	private GUIStyle style = new GUIStyle() {
		normal = new GUIStyleState() {
			background = Texture2D.grayTexture
		}
	};

	 void OnEnable() {
		Application.logMessageReceived += HandleLog;
		if (File.Exists(ConfigPath)) 
			Config = JsonParser.FromJson<ConsoleConfig>(File.ReadAllText(ConfigPath)) ?? new ConsoleConfig();
	}

	private void OnDisable() {
		Application.logMessageReceived -= HandleLog;
	}

	private void HandleLog(string logString, string stackTrace, LogType type) {
		string message = logString;
		if (type == LogType.Exception) {
			message += "\n" + stackTrace;
		}
		foreach (string line in message.Split('\n')) {
			logQueue.Enqueue(line);
		}
		while (logQueue.Count > consoleLines) {
			logQueue.Dequeue();
		}
	}

	private void OnGUI() {
		if (Event.current.type == EventType.KeyDown) {
			if (Event.current.keyCode == KeyCode.BackQuote) {
				visible = !visible;
				if (Config.ClearContentOnClose) command = string.Empty;
			}

			if (visible) {
				if (Event.current.keyCode == KeyCode.Return) {
					if (command != string.Empty) {
						Debug.Log(command);
						if (command[0] == '/') {
							RunCommand(command.Substring(1));
						}
						else if (!Config.CommandSlashRequired) {
							RunCommand(command);
						}
					}
					command = string.Empty;
				}
				if (Event.current.keyCode == KeyCode.Escape) {
					if (Config.ClearContentOnClose) command = string.Empty;
					visible = false;
					historyIndex = 0;
				}
				if (Event.current.keyCode == KeyCode.UpArrow && history.Count > historyIndex) {
					command = $"/{history[history.Count - historyIndex - 1]}";
					historyIndex++;
				}
				if (Event.current.keyCode == KeyCode.DownArrow && 0 < historyIndex) {
					historyIndex--;
					command = $"/{history[history.Count - historyIndex - 1]}";
				}

			}
		}

		if (visible) {
			GUILayout.BeginArea(new Rect(Screen.width * 0.02f, Screen.height - 395, Screen.width * 0.96f, 330), style);
			GUILayout.Label(string.Join("\n", logQueue.ToArray()));
			GUILayout.EndArea();

			GUILayout.BeginArea(new Rect(Screen.width * 0.02f, Screen.height - 25f, Screen.width * 0.96f, 20f));

			GUI.SetNextControlName("command");
			command = GUILayout.TextField(command);

			GUILayout.EndArea();

			GUI.FocusControl("command");
		}

		if (command.EndsWith("`", StringComparison.Ordinal))
			command = command.Remove(command.Length - 1);
	}

	void RunCommand(string commandString) {
		historyIndex = 0;
		history.Remove(commandString);
		CommandManager.RunCommand(commandString);
		history.Add(commandString);
	}
}