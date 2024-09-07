@echo off
mkdir package
cd bin/Release
tar -a -cf ../../package/RL2.ModLoader.zip MonoMod NativeFileDialogSharp.dll nfd.dll nfd_x86.dll RL2.ModLoader.dll RL2.ModLoader.Installer.exe RL2.ModLoader.pdb RL2.ModLoader.xml RuntimeInitializeOnLoads.json ScriptingAssemblies.json 
echo Packaged RL2.ModLoader