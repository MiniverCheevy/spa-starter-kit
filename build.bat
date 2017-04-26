rmdir /s /q src\Aurelia.Web\bin\
rmdir /s /q src\Aurelia.Web\obj\
rmdir /s /q src\Core\bin\
rmdir /s /q src\Core\obj\
rmdir /s /q src\Tests\bin\
rmdir /s /q src\Tests\obj\
call dotnet restore
call dotnet build dev-tools.sln
call dotnet build Fernweh.sln