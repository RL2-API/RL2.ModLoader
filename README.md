# RL2.ModLoader
[![GitHub Release](https://img.shields.io/github/v/release/RL2-API/RL2.ModLoader.svg?logo=github&style=flat-square)](https://github.com/RL2-API/RL2.ModLoader/releases/latest)
![Docs](https://img.shields.io/badge/Documentation-Online-blue?logo=github&style=flat-square)
[![Website](https://img.shields.io/badge/Website-gray?logo=webtrees&logoColor=white&style=flat-square)](https://rl2-modloader.onrender.com)
![Stability](https://img.shields.io/badge/Stability-Full-Green?style=flat-square)
![Steam version status](https://img.shields.io/badge/Steam-Works-Works?logo=steam&style=flat-square)
![Epic Games version status](https://img.shields.io/badge/Epic_Games-Works-Works?logo=epicgames&style=flat-square)
![Mod Loader Icon](https://raw.githubusercontent.com/RL2-API/RL2.ModLoader/main/Assets/ModLoaderSocialPreview-1600x516.png)

## Overview
RL2.ModLoader is a custom mod loader for [Rogue Legacy 2](https://roguelegacy2.com) - a roguelite created by Cellar Door Games. The project provides a way to load specially prepared modifications into the game.

## Installation
*Video guide available [here](https://youtu.be/KXa7LqFYy5o)*
1. Download the [latest release](https://github.com/RL2-API/RL2.ModLoader/releases/latest)
2. Unpack the downloaded `.zip` file into your directory of choice
3. Run `RL2.ModLoader.Installer.exe`
4. Choose your game installation folder
5. Run the game to generate all necessary directories and files

## Build from source
1. Run `git clone https://github.com/RL2-API/RL2.ModLoader --recurse-submodules` in your terminal
2. Go to `RL2.ModLoader.DevSetup` and run the provided `.exe`
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
