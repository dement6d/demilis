#!/bin/bash

cd src
dotnet publish -c Release -r linux-x64 -p:PublishSingleFile=true --self-contained true

sudo mv bin/Release/net6.0/linux-x64/publish/demilis /usr/bin/
sudo chmod +x /usr/bin/demilis

echo ""
echo "Demilis installed in /usr/bin/demilis"
