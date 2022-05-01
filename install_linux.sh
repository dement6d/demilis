#!/bin/bash

cd src
dotnet publish -c Release -r linux-x64 -p:PublishSingleFile=true --self-contained true

sudo mv bin/Release/net6.0/linux-x64/publish/demilis /usr/bin/
sudo chmod +x /usr/bin/demilis

sudo bash -c "echo $'[Desktop Entry]\nName=demilis\nGenericName=Multiple connection TCP listener\nExec=/usr/bin/demilis -p 8080\nTerminal=true\nType=Application\nCategories=Utility;\nIcon=demilis\nPath=/usr/bin' > /usr/share/applications/demilis.desktop"

echo ""
echo "Demilis installed in /usr/bin/demilis"
