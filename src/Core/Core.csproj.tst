<?xml version="1.0" encoding="utf-8"?>
<Project xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props" Condition="Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')" />
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProjectGuid>{F298643E-3DAE-4A0D-914B-CE85DF3B0F99}</ProjectGuid>
    <OutputType>Library</OutputType>
    <AppDesignerFolder>Properties</AppDesignerFolder>
    <RootNamespace>Fernweh.Core</RootNamespace>
    <AssemblyName>Fernweh.Core</AssemblyName>
    <TargetFrameworkVersion>v4.6.1</TargetFrameworkVersion>
    <FileAlignment>512</FileAlignment>
    <TargetFrameworkProfile />
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>bin\Debug\</OutputPath>
    <DefineConstants>DEBUG;TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>bin\Release\</OutputPath>
    <DefineConstants>TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089, processorArchitecture=MSIL">
      <HintPath>..\..\packages\EntityFramework.6.1.3\lib\net45\EntityFramework.dll</HintPath>
    </Reference>
    <Reference Include="EntityFramework.SqlServer, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089, processorArchitecture=MSIL">
      <HintPath>..\..\packages\EntityFramework.6.1.3\lib\net45\EntityFramework.SqlServer.dll</HintPath>
    </Reference>
    <Reference Include="Newtonsoft.Json, Version=10.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed, processorArchitecture=MSIL">
      <HintPath>..\..\packages\Newtonsoft.Json.10.0.2\lib\net45\Newtonsoft.Json.dll</HintPath>
    </Reference>
    <Reference Include="System" />
    <Reference Include="System.ComponentModel.DataAnnotations" />
    <Reference Include="System.Core" />
    <Reference Include="System.Data.Entity.Design" />
    <Reference Include="System.Runtime.Caching" />
    <Reference Include="System.Transactions" />
    <Reference Include="System.Web.Extensions" />
    <Reference Include="System.Xml.Linq" />
    <Reference Include="System.Data.DataSetExtensions" />
    <Reference Include="Microsoft.CSharp" />
    <Reference Include="System.Data" />
    <Reference Include="System.Net.Http" />
    <Reference Include="System.Xml" />
    <Reference Include="Voodoo, Version=2.2.18.0, Culture=neutral, processorArchitecture=MSIL">
      <HintPath>..\..\packages\Voodoo.Patterns.2.2.18\lib\net46\Voodoo.dll</HintPath>
    </Reference>
  </ItemGroup>
  <ItemGroup>
    <Compile Include="AppSettings.cs" />
    <Compile Include="Constants.generated.cs" />
    <Compile Include="Context\ExceptionalContext\Error.cs" />
    <Compile Include="Context\ExceptionalContext\IErrorContext.cs" />
    <Compile Include="Context\ExceptionTranslators\DbEntityValidationExceptionTranslator.cs" />
    <Compile Include="Context\ExceptionTranslators\ForiegnKeyExceptionTranslation.cs" />
    <Compile Include="Context\ExceptionTranslators\SqlExceptionTranslator.cs" />
    <Compile Include="Context\FernwehContext.cs" />
    <Compile Include="Context\PerformanceExtensions.cs" />
    <Compile Include="Context\UniqueConstraintExceptionTranslation.cs" />
    <Compile Include="Identity\AppPrincipal.cs" />
    <Compile Include="Identity\PasswordManager.cs" />
    <Compile Include="Identity\UserStore.cs" />
    <Compile Include="Infrastructure\ClientInfo.cs" />
    <Compile Include="Infrastructure\ContextFactory.cs" />
    <Compile Include="Infrastructure\IContextFactory.cs" />
    <Compile Include="Infrastructure\IRequestContextProvider.cs" />
    <Compile Include="Infrastructure\RequestContext.cs" />
    <Compile Include="Infrastructure\Settings.cs" />
    <Compile Include="Infrastructure\UtcToLocalTimeConverter.cs" />
    <Compile Include="IOC.cs" />
    <Compile Include="Logging\ITraceWriter.cs" />
    <Compile Include="Logging\TraceLogger.cs" />
    <Compile Include="Migrations\201704181511077_Initial.cs" />
    <Compile Include="Migrations\201704181511077_Initial.Designer.cs">
      <DependentUpon>201704181511077_Initial.cs</DependentUpon>
    </Compile>
    <Compile Include="Migrations\Configuration.cs" />
    <Compile Include="ModelExtensions\ModelExtensions.cs" />
    <Compile Include="Models\ApplicationSetting.cs" />
    <Compile Include="Models\Identity\User.cs" />
    <Compile Include="Operations\ApplicationSettings\ApplicationSettingListQuery.cs" />
    <Compile Include="Operations\ApplicationSettings\Extras\ApplicationSettingExtensions.cs" />
    <Compile Include="Operations\CurrentUsers\BuildPrincipalCommand.cs" />
    <Compile Include="Operations\CurrentUsers\Extras\BuildPrincipalRequest.cs" />
    <Compile Include="Operations\CurrentUsers\GetCurrentUserCommand.cs" />
    <Compile Include="Operations\Errors\ErrorAddCommand.cs" />
    <Compile Include="Operations\Errors\ErrorDetailQuery.cs" />
    <Compile Include="Operations\Errors\ErrorJsonDeserializer.cs" />
    <Compile Include="Operations\Errors\ErrorListQuery.cs" />
    <Compile Include="Operations\Errors\Extras\ErrorExtensions.cs" />
    <Compile Include="Operations\Errors\Extras\ErrorExtensions.generated.cs" />
    <Compile Include="Operations\Errors\Extras\ErrorMessage.cs" />
    <Compile Include="Operations\Errors\Extras\ErrorMessages.cs" />
    <Compile Include="Operations\Errors\Extras\ErrorQueryRequest.cs" />
    <Compile Include="Operations\Errors\Extras\ErrorQueryResponse.cs" />
    <Compile Include="Operations\Errors\Extras\ErrorRepository.cs" />
    <Compile Include="Operations\Errors\Extras\ErrorRequest.cs" />
    <Compile Include="Operations\Errors\Extras\IError.cs" />
    <Compile Include="Operations\Errors\Extras\MobileErrorRequest.cs" />
    <Compile Include="Operations\Errors\MobileErrorAddCommand.cs" />
    <Compile Include="Operations\Lists\ListItem.cs" />
    <Compile Include="Operations\Lists\Lists.generated.cs" />
    <Compile Include="Operations\Lists\ListsHelper.generated.cs" />
    <Compile Include="Operations\Lists\ListsQuery.cs" />
    <Compile Include="Operations\Lists\ListsRequest.cs" />
    <Compile Include="Operations\Lists\ListsResponse.generated.cs" />
    <Compile Include="Operations\Roles\Extras\RoleExtensions.cs" />
    <Compile Include="Operations\Roles\Extras\RoleExtensions.generated.cs" />
    <Compile Include="Operations\Roles\Extras\RoleMessage.cs" />
    <Compile Include="Operations\Roles\Extras\RoleMessages.cs" />
    <Compile Include="Operations\Roles\Extras\RoleQueryRequest.cs" />
    <Compile Include="Operations\Roles\Extras\RoleQueryResponse.cs" />
    <Compile Include="Operations\Roles\Extras\RoleRepository.cs" />
    <Compile Include="Operations\Users\Extras\UserExtensions.cs" />
    <Compile Include="Operations\Users\Extras\UserExtensions.generated.cs" />
    <Compile Include="Operations\Users\Extras\UserMessage.cs" />
    <Compile Include="Operations\Users\Extras\UserMessages.cs" />
    <Compile Include="Operations\Users\Extras\UserQueryRequest.cs" />
    <Compile Include="Operations\Users\Extras\UserQueryResponse.cs" />
    <Compile Include="Operations\Users\Extras\UserRepository.cs" />
    <Compile Include="Operations\Users\UserDeleteCommand.cs" />
    <Compile Include="Operations\Users\UserDetailQuery.cs" />
    <Compile Include="Operations\Users\UserListQuery.cs" />
    <Compile Include="Operations\Users\UserSaveCommand.cs" />
    <Compile Include="Properties\AssemblyInfo.cs" />
    <Compile Include="RestResources.generated.cs" />
    <Compile Include="RoleNames.cs" />
    <Compile Include="Security\Encryption.cs" />
    <Compile Include="Security\Encryptor.cs" />
    <Compile Include="Operations\ApplicationSettings\Extras\ApplicationSettingMessage.cs" />
    <Compile Include="Operations\ApplicationSettings\Extras\ApplicationSettingMessages.cs" />
    <Compile Include="Operations\ApplicationSettings\Extras\ApplicationSettingRepository.cs" />
    <Compile Include="Operations\ApplicationSettings\Extras\ApplicationSettingQueryResponse.cs" />
    <Compile Include="Operations\ApplicationSettings\ApplicationSettingDeleteCommand.cs" />
    <Compile Include="Operations\ApplicationSettings\ApplicationSettingSaveCommand.cs" />
    <Compile Include="Operations\ApplicationSettings\Extras\ApplicationSettingExtensions.generated.cs" />
    <Compile Include="Operations\ApplicationSettings\ApplicationSettingDetailQuery.cs" />
    <Compile Include="Operations\ApplicationSettings\Extras\ApplicationSettingQueryRequest.cs" />
  </ItemGroup>
  <ItemGroup>
    <None Include="app.config" />
    <None Include="Operations\Errors\error.json" />
    <None Include="packages.config" />
  </ItemGroup>
  <ItemGroup>
    <EmbeddedResource Include="Migrations\201704181511077_Initial.resx">
      <DependentUpon>201704181511077_Initial.cs</DependentUpon>
    </EmbeddedResource>
  </ItemGroup>
  <ItemGroup>
    <Content Include="Migrations\ReadMe.txt" />
    <EmbeddedResource Include="Migrations\Scripts\Lookups.sql" />
    <Content Include="Migrations\Scripts\ReadMe.txt" />
    <EmbeddedResource Include="Migrations\Scripts\Script.sql" />
  </ItemGroup>
  <Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />
</Project>