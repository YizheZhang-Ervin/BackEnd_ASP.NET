# TMPL_ASP.NET

## 1. React + ASP.NET Core (TemplateProject)
```sh
## Create
- dotnet new react

## Run
- cd TMPL_DOTNET> dotnet build
- 删除node_modules里原包再npm i --save caniuse-lite browserslist
- cd TMPL_DOTNET> dotnet run
```

## 2. Avalonia UI（MyApp）
```sh
# 安装框架
dotnet new install Avalonia.Templates
dotnet new list

# 新增&启动项目
dotnet new avalonia.app -o MyApp1
dotnet new avalonia.mvvm -o MyApp2
cd MyApp
dotnet run

# .NET环境
dotnet --version
dotnet nuget list source
dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget.org

# IDE
- JetBrains Rider + 插件AvaloniaRider
- Visual Studio + Avalonia for Visual Studio扩展
- Visual Studio Code + 插件Avalonia for VSCode

# WPF迁移
# 示例
# 基础
# 深入
# 部署
# 操作指南
```