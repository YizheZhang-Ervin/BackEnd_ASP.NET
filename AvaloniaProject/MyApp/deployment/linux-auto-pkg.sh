#!/bin/bash

# 清理
rm -rf ./out/
rm -rf ./staging_folder/

# .NET 发布
# 推荐使用 self-contained 发布，这样用户不需要安装 .NET运行时
dotnet publish "./src/MyProgram.Desktop/MyProgram.Desktop.csproj" \
  --verbosity quiet \
  --nologo \
  --configuration Release \
  --self-contained true \
  --runtime linux-x64 \
  --output "./out/linux-x64"

# 暂存目录
mkdir staging_folder

# Debian control文件
mkdir ./staging_folder/DEBIAN
cp ./src/MyProgram.Desktop.Debian/control ./staging_folder/DEBIAN

# 启动脚本
mkdir ./staging_folder/usr
mkdir ./staging_folder/usr/bin
cp ./src/MyProgram.Desktop.Debian/myprogram.sh ./staging_folder/usr/bin/myprogram
chmod +x ./staging_folder/usr/bin/myprogram # 设置启动脚本的执行权限

# 其他文件
mkdir ./staging_folder/usr/lib
mkdir ./staging_folder/usr/lib/myprogram
cp -f -a ./out/linux-x64/. ./staging_folder/usr/lib/myprogram/ # 从publish目录复制所有文件
chmod -R a+rX ./staging_folder/usr/lib/myprogram/ # 设置所有文件的读权限
chmod +x ./staging_folder/usr/lib/myprogram/myprogram_executable # 设置主可执行文件的执行权限

# 桌面快捷方式
mkdir ./staging_folder/usr/share
mkdir ./staging_folder/usr/share/applications
cp ./src/MyProgram.Desktop.Debian/MyProgram.desktop ./staging_folder/usr/share/applications/MyProgram.desktop

# 桌面图标
# 一个 1024px x 1024px 的 PNG 文件，类似于 VS Code 使用的图标
mkdir ./staging_folder/usr/share/pixmaps
cp ./src/MyProgram.Desktop.Debian/myprogram_icon_1024px.png ./staging_folder/usr/share/pixmaps/myprogram.png

# Hicolor 图标
mkdir ./staging_folder/usr/share/icons
mkdir ./staging_folder/usr/share/icons/hicolor
mkdir ./staging_folder/usr/share/icons/hicolor/scalable
mkdir ./staging_folder/usr/share/icons/hicolor/scalable/apps
cp ./misc/myprogram_logo.svg ./staging_folder/usr/share/icons/hicolor/scalable/apps/myprogram.svg

# 制作 .deb 文件
dpkg-deb --root-owner-group --build ./staging_folder/ ./myprogram_3.1.0_amd64.deb