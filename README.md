# Accurate IGT

A BepInEx plugin for The Room Three that pauses the in-game timer during loading screens.

## Installation

Download and install the latest stable release of [BepInEx 5](https://github.com/BepInEx/BepInEx/releases/latest) (x86) to the game's installed directory (default for Steam on Windows is `%localappdata%\steam\steamapps\common\TheRoomThree`), then place Accurate_IGT.dll in the `BepInEx\plugins` directory.

## Building  

Requires .NET SDK 6 or newer.

- Create a folder named lib in the project directory and copy Assembly-CSharp.dll from the the game files for The Room Three (located in `%programfiles(x86)%\steam\steamapps\common\TheRoomThree\TheRoomThree_Data\managed` for the Steam release on Windows).
- Run dotnet build in the project directory.
