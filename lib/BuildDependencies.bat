%windir%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe .\Net35Essentials/Net35Essentials.sln /p:Configuration=Release /p:Platform="Any CPU"
xcopy  /R /E /Y /q ".\Net35Essentials\src\Net35Essentials\bin\Release" ".\Dependency Builds\Net35Essentials\DLLs\"

PAUSE
