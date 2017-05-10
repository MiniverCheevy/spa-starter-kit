rmdir /s /q src\Aurelia.Web\bin\
rmdir /s /q src\Aurelia.Web\obj\
rmdir /s /q src\Core\bin\
rmdir /s /q src\Core\obj\
rmdir /s /q src\Tests\bin\
rmdir /s /q src\Tests\obj\
call dotnet build dev-tools.sln
call dotnet build Aurelia.sln
call dotnet restore Aurelia.sln
call dotnet run Aurelia.sln
REM call dotnet restore React.sln
REM call dotnet build React.sln
