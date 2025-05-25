@echo off
mkdir package
cd bin/Release/publish
tar -a -cf ../../package/RL2.ModLoader.zip package 
tar -a -cf ../../package/RL2.ModLoader-manual.zip manual
echo Packaged RL2.ModLoader