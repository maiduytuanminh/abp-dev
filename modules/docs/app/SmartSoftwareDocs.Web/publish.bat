@echo off

dotnet clean
dotnet restore
dotnet build

DEL /F/Q/S "C:\Publishes\SmartSoftwareDocs" > NUL && RMDIR /Q/S "C:\Publishes\SmartSoftwareDocs"

dotnet publish -c Release -r win-x64   --self-contained true -o "C:\Publishes\SmartSoftwareDocs\win-x64\Web"
dotnet publish -c Release -r win-x86   --self-contained true -o "C:\Publishes\SmartSoftwareDocs\win-x86\Web"
dotnet publish -c Release -r osx-x64   --self-contained true -o "C:\Publishes\SmartSoftwareDocs\osx-x64\Web"
dotnet publish -c Release -r linux-x64 --self-contained true -o "C:\Publishes\SmartSoftwareDocs\linux-x64\Web"

cd..\SmartSoftwareDocs.Migrator 

dotnet publish -c Release -r win-x64   --self-contained true -o "C:\Publishes\SmartSoftwareDocs\win-x64\Migrator"
dotnet publish -c Release -r win-x86   --self-contained true -o "C:\Publishes\SmartSoftwareDocs\win-x86\Migrator"
dotnet publish -c Release -r osx-x64   --self-contained true -o "C:\Publishes\SmartSoftwareDocs\osx-x64\Migrator"
dotnet publish -c Release -r linux-x64 --self-contained true -o "C:\Publishes\SmartSoftwareDocs\linux-x64\Migrator"
