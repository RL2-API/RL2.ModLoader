rm -rf package
mkdir package

dotnet publish

cd bin/Release/publish
tar -a -cf ../../../package/RL2.ModLoader.zip package 
tar -a -cf ../../../package/RL2.ModLoader-manual.zip manual
echo Packaged RL2.ModLoader
