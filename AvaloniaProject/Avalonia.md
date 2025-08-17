# 知识点

## 1. IDE
```sh
- JetBrains Rider + 插件AvaloniaRider
- Visual Studio + Avalonia for Visual Studio扩展
- Visual Studio Code + 插件Avalonia for VSCode
```

# 2. 基础
```sh
1）界面
- xaml
- 控件：绘制控件(如Border、TextBlock、Image)、布局控件(如Grid、StackPanel)、自定义(用户控件、模板化控件)
- 布局、样式
- 交互性(事件/命令)
- 资产/图像、动画、文件对话框、多点触控、消息框
2）数据
- 数据绑定(数据上下文、数据绑定语法、编译绑定)、数据模板
```

# 3. 深入
```sh
# Reactive UI
dotnet add package Avalonia.ReactiveUI
BuildAvaloniaApp()加.UseReactiveUI();
```

# 4. 部署
```sh
## linux(MyApp/deployment/staging_folder手动打包)
dpkg-deb --root-owner-group --build ./staging_folder/ "./myprogram_${versionName}_amd64.deb"
### linux-auto-pkg.sh自动打包
### 安装 sudo apt install ./myprogram_3.1.0_amd64.deb
### 卸载 sudo apt remove myprogram

## 原生AOT部署
dotnet publish -r win-x64 -c Release
dotnet publish -r linux-x64 -c Release
dotnet publish -r osx-x64 -c Release
dotnet publish -r osx-arm64 -c Release
```