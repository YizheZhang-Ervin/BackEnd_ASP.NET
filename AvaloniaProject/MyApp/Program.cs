using Avalonia;
using System;

namespace MyApp;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);
    // 【手动管理】
    // public static int Main(string[] args) 
    // => BuildAvaloniaApp().Start(AppMain, args);

    // Avalonia configuration, don't remove; also used by visual designer.
    // 这个方法是为了IDE预览器基础设施而需要的
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();

    // 【手动管理】应用程序入口点。Avalonia已完全初始化。
    // static void AppMain(Application app, string[] args)
    // {
    //     // 一个取消令牌源，它将
    //     // 用于停止主循环
    //     var cts = new CancellationTokenSource();
    //     // 在这里启动你的代码
    //     new Window().Show();
    //     // 启动主循环
    //     app.Run(cts.Token);
    // }
}
