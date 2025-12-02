<h1 align="center">
    <img src="https://raw.githubusercontent.com/RL2-API/RL2.ModLoader/main/Assets/ModLoaderSocialPreview-1600x516.png" alt="Mod Loader Banner"/>
    RL2.ModLoader <br/>
    <a href="https://github.com/RL2-API/RL2.ModLoader/releases/latest"><img src="https://img.shields.io/github/v/release/RL2-API/RL2.ModLoader.svg?logo=github&style=flat-square" alt="GitHub Release" /></a>
    <a href="https://github.com/RL2-API/RL2.ModLoader/wiki"><img src="https://img.shields.io/badge/Documentation-Online-blue?logo=github&style=flat-square" alt="Docs" /></a>
    <a href="https://rl2-api.org"><img src="https://img.shields.io/badge/Website-gray?logo=webtrees&logoColor=white&style=flat-square" alt="Website" /></a>
    <img src="https://img.shields.io/badge/Steam-Works-Green?logo=steam&style=flat-square" alt="Steam Version Status" />
    <img src="https://img.shields.io/badge/Epic_Games-Works-Green?logo=epicgames&style=flat-square" alt="EGS Version Status" />
</h1>

## Overview
RL2.ModLoader is a custom mod loader for [Rogue Legacy 2](https://roguelegacy2.com) - a roguelite created by Cellar Door Games. The project provides a way to load specially prepared modifications into the game.

## Installation
*Video guide available [here](https://youtu.be/KXa7LqFYy5o)*
1. Download the [latest release](https://github.com/RL2-API/RL2.ModLoader/releases/latest)
2. Unpack the downloaded `.zip` file into your directory of choice
3. Run `RL2.ModLoader.Installer.exe`
4. Choose your game installation folder
5. Run the game to generate all necessary directories and files

## Building from source
1. Run `git clone https://github.com/RL2-API/RL2.ModLoader` in your terminal
2. Run either `setup.sh` or `setup.bat`.
    - If you ran `setup.sh` go to `RL2.ModLoader.DevSetup` and run the provided `.exe`
3. Choose your game installation folder
4. Open the solution and build it or run `dotnet build -c Release` from the RL2.ModLoader directory in your terminal
5. Go to the build directory and run `RL2.ModLoader.Installer.exe`
6. Choose your game installation folder
7. Run the game to generate all necessary directories and files

## Installing mods
1. Download your mod
	- Unpack if necessary
2. Move all the files into `GameInstallation/Rogue Legacy 2_Data/Mods`
3. Launch the game

## Latest changes:
Visit our [changelog](https://github.com/RL2-API/RL2.ModLoader/blob/main/CHANGELOG.md)

## License
RL2.ModLoader is licensed under the [LGPL v3.0 license](https://github.com/RL2-API/RL2.ModLoader/blob/main/LICENSE.md)
