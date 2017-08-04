To generate EF Database Scripts

update-database –SourceMigration $InitialDatabase -script

There should always be an up to date script Database.Update.sql in the tools folder 
that contains both ef migrations and custom embeded resource scripts from Core\Migrations\Scripts

if you do not already have it install node
https://nodejs.org/en/download/

From the web project

dotnet restore
yarn install

open the solution in visual studio

<ctrl> + f5


from the web project folder
webpack --colors --progress 



if nuget packages give you grief close visual studio and run the following from the solution folder
dotnet build dev-tools.sln
dotnet build [YOURSOLUTION].sln
dotnet restore [YOURSOLUTION].sln
then from the web folder run
dotnet run

to update vendor.js
node node_modules/webpack/bin/webpack.js --config webpack.config.vendor.js
or 
build-vendor.bat

If all else fails delete the contents of these folders

%USERPROFILE%\Documents\IISExpress\config\applicationhost.config
./.vs/config/applicationhost.config

