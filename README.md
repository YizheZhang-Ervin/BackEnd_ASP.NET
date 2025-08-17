# TMPL_ASP.NET

## 0. ENV
```sh
# .NET环境
dotnet --version
dotnet nuget list source
dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget.org
```

## 1. React + ASP.NET Core (ReactAspProject)
```sh
## Create
dotnet new react

## Run
dotnet build
#删除node_modules里原包再npm i --save caniuse-lite browserslist
dotnet run
```

## 2. Avalonia UI（AvaloniaProject/MyApp）
```sh
# 安装框架
dotnet new install Avalonia.Templates
dotnet new list

# 安装依赖
dotnet add package Newtonsoft.Json --version 13.0.1
##或在xxx.csproj中添加<ItemGroup><PackageReference Include="Newtonsoft.Json" Version="13.0.1" /></ItemGroup>

# 新增&启动项目
dotnet new avalonia.app -o MyApp1
dotnet new avalonia.mvvm -o MyApp2
cd MyApp
dotnet run

# 示例
- AvaloniaProject/example.TodoList
- AvaloniaProject/example.GroupBox（用ReactiveUI）
- AvaloniaProject/example.MusicStore

## 原生AOT部署
dotnet publish -r win-x64 -c Release
dotnet publish -r linux-x64 -c Release
dotnet publish -r osx-x64 -c Release
dotnet publish -r osx-arm64 -c Release
```