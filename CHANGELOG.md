# RL2.ModLoader Changelog

# v1.1.1 - UNRELEASED
### Fixes
- Make Config.ClearContentOnClose work and fix ` symbol in console - [#3](https://github.com/RL2-API/RL2.ModLoader/pull/3) by Dzhake

# v1.1.0 - The Console QoL update
### Additions:
- Help command with 2 usage modes:
    1. `/help` - displays a list of available commands
    2. `/help command_name` - displays help text of passed in command, if that command provides it
- CommandHelp attribute
    - Used to add help text to the `/help` command
- Command history
    - Press `Up` and `Down` to scroll through the history
- Console config
    - Allows for disabling clearing the command prompt on close and the requirement for a starting `/` before commands
### Changes:
- Revised mod template
    - Utilizes a new `RL2.ModLoader.targets` file that will be generated in `Game_Path/Rogue Legacy 2_Data/ModSources`
    - This includes references to all game files as well as a special build step to copy them where they should be

# v1.0.4
### Additions:
- All installed mods and their versions are displayed below the RL2.ModLoader version number

# v1.0.3
### Changes:
- All RL2.ModLoader detours now have their own IDs

# v1.0.2
### Bug fixes:
- Commands are now loaded from standalone mods correctly
- ModManifest sorting
- Command invoking now works properly
### Changes:
- The mod loaders version is now displayed in a new line after the game version
- Entries specified in LoadAfter property of a mod manifest are now logged before mod loading
### Additions:
- Builtin `create-mod` command
	- Usage `/create-mod [ModName - required] [Author - optional]`
	- Creates base project files for an independent mod
- Builtin `show-mods` command
	- Usage `/show-mods`
	- Displays a list of all enabled mods and their versions

# v1.0.1
- Bug fixes:
	- Load order issues, again

# v1.0.0
- Bug fixes:
	- Load order
	- `enabled.json` creation fail
- Finish the separation started in 0.1.0
- RL2.ModLoader version display in title screen is back

# v0.1.0
- Separated the mod loader and API from th main games asembly.
	- This should allow for an easier compilation process for contributors/self-sufficeint modders.
- Split the mod loader into two projects: RL2.ModLoader and RL2.API.
	- This would allow modders to choose not to use the API, but create an alternate framework or not use one at all and hook everything themselves.
		- If a modder chooses not to use the API they get more freedom in how to structure their project, for a cost of potentially more work.
		- Mods not using RL2.API are related to as `not API compliant`.
	- Provided RL2.ModLoader.DevSetup for easier developer setup.
	- Provided RL2.ModLoader.Installer for easier installation.
- All features ported to the new version
- **RL2.ModLaoder now works on both the Epic Games Store and the Steam version of the game**

# v0.0.2
- Moved to .NET Framework 4.8
- MonoMod is now included alongside the API releases.
- Added ModifyGeneratedCharacterData method to ModSystem.
	- Ran after a heir is generated, allows for modyfying data of that heir.
- Added ModifyCharacterRandomization.
	- Ran each time a heir is generated with the Contrarian trait (before GenerateRandomCharcter ends).
	- Ran each time a Transmogrifier is picked up.
- Fixed ModifyRoomIcon changes not applying after death.

# v0.0.1-alpha-4
- Fixed ModifyRoomIcon changes not applying after world reload.
- Fixed mod map icons not being offset properly.
- Fixed older versions of Assembly-CSharp beig copied as references during decompilation.

# v0.0.1-alpha-3
- Added ModSystem.ModifyRoomIcon.
	- Allows to set a map icon for a room that is passed in as the argument.
- XML documentation is now generated on build and provided alongside the release.

# v0.0.1-alpha-2
- Fixes to `generate-mod-skeleton`.
	- The generated `*.csproj` file is now valid.
	- Generated `*.mod.json` file is now using pretty JSON format and follows symantic versioning rules.
	- Change default path to mod assembly to default VS2022 path (`bin\Debug\net40\*.dll`).
- Added warning in logs when an assembly sepcified in a `*.mod.json` file doesn't exist.
- Fixed a bug during mod loading that could set the Mod.Path variable to incorrect value.
- Fixed PreKill - now returning true causes the entity to stay at 1HP.
- Added SwapTexture method.
	- Takes in an instance of the texture you want to change on the enemy and a replacement, then it performs the swap.

# v0.0.1-alpha
- Initial release
