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


vendor.js is not updated during a normal build, if you add new vendor libraries manually run the following to update vendor.js

node node_modules/webpack/bin/webpack.js --config webpack.config.vendor.js
or 
build-vendor.bat




If all else fails delete the contents of these folders

%USERPROFILE%\Documents\IISExpress\config\applicationhost.config
./.vs/config/applicationhost.config

Updating Controllers

Controllers are generated from the Rest attributes on your Core/Operations commands and queries

The query decoarated with this attribute

[Rest(Verb.Get, RestResources.UserList, Roles = new[] { RoleNames.Administrator })]

Will Generate the controller UserListController's Get Method with an endpoint of
UserList\Get

you can generate or update controllers with the command 

./tools/spawn web

for more details about other code generation options use

./tools/spawn

and review the associated configuration in spawn.json
