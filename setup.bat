@echo off
curl -OL https://github.com/RL2-API/RL2.ModLoader.DevSetup/releases/download/v2.1.0/RL2.ModLoader.DevSetup.tar.gz
mkdir RL2.ModLoader.DevSetup
tar -xzf ./"RL2.ModLoader.DevSetup.tar.gz" -C ./RL2.ModLoader.DevSetup
del "RL2.ModLoader.DevSetup.tar.gz"

cd RL2.ModLoader.DevSetup
RL2.ModLoader.DevSetup.exe
cd ..