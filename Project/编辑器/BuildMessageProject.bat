set VCVarsallPath=%VS120COMNTOOLS%..\..\VC\vcvarsall.bat
call "%VCVarsallPath%"
cd /d %~dp0

msbuild "..\..\source\Messages\Messages.csproj" /p:Configuration=Release /m
".\Precompile\precompile.exe" "..\..\source\Messages\bin\Release\Messages.dll" -o:ProtobufSerializer.dll -t:ProtobufSerializer
copy /y "ProtobufSerializer.dll" "..\..\source\ghclient\Assets\Resources\Common\Lib\ProtobufSerializer.dll"
copy /y "..\..\source\Messages\bin\Release\Messages.dll" "..\..\source\ghclient\Assets\Resources\Common\Lib\Messages.dll"
copy /y "..\..\source\Messages\bin\Release\protobuf-net.dll" "..\..\source\ghclient\Assets\Resources\Common\Lib\protobuf-net.dll"
del "ProtobufSerializer.dll"