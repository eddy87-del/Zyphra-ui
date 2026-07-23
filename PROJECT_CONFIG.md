# Zyphra Virtual Assistant Configuration

## Build Configuration

```xml
<Project Sdk="Microsoft.NET.Sdk.WindowsDesktop">

  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net6.0-windows</TargetFramework>
    <Nullable>enable</Nullable>
    <UseWPF>true</UseWPF>
    <StartupUri>MainWindow.xaml</StartupUri>
    <AssemblyName>ZyphraVirtualAssistant</AssemblyName>
    <RootNamespace>ZyphraVirtualAssistant</RootNamespace>
    <Title>Zyphra Virtual Assistant</Title>
    <Description>Offline Windows Virtual Assistant with Voice Control</Description>
    <Company>Zyphra</Company>
    <Product>Zyphra Virtual Assistant</Product>
    <Version>1.0.0</Version>
    <InformationalVersion>1.0.0-offline</InformationalVersion>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="System.Speech" Version="7.0.0" />
  </ItemGroup>

</Project>
```

This configuration enables:
- ✅ Windows Desktop Application
- ✅ WPF Framework
- ✅ System.Speech for voice recognition
- ✅ Offline functionality
- ✅ Minimal dependencies
