@echo OFF
msbuild /p:Configuration=Release /t:Rebuild
ECHO "FileUpload.dll is in FileUpload/bin/Release/"
