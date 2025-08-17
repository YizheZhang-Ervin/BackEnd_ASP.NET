using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SimpleToDoList.Services;
using SimpleToDoList.ViewModels;
using SimpleToDoList.Views;

namespace SimpleToDoList;

public partial class App : Application
{
    // 初始化
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    // 加载MainViewModel
    private readonly MainViewModel _mainViewModel = new MainViewModel();
    
    // 初始化完成
    public override async void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                // 私有化引用
                DataContext = _mainViewModel
            };
            
            // 监听关闭app请求
            desktop.ShutdownRequested += DesktopOnShutdownRequested;
        }

        // 调用父方法初始化完成方法
        base.OnFrameworkInitializationCompleted();
        
        // 异步初始化MainViewModel 
        await InitMainViewModelAsync();
    }

    // 关闭app前保存TodoList（File I/O是异步的）
    // 检查windows是否可关
    private bool _canClose;
    // 关闭app请求
    private async void DesktopOnShutdownRequested(object? sender, ShutdownRequestedEventArgs e)
    {
        // 首次取消关闭活动
        e.Cancel = !_canClose; // cancel closing event first time

        if (!_canClose)
        {
            // 把items转换成ToDoItem-Model（更适合IO操作）
            var itemsToSave = _mainViewModel.ToDoItems.Select(item => item.GetToDoItem());
            // 保存
            await ToDoListFileService.SaveToFileAsync(itemsToSave);
            
            // 把_canClose设为true，再一次关window
            _canClose = true;
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.Shutdown();
            }
        }
    }
    
    // 从磁盘加载数据
    private async Task InitMainViewModelAsync()
    {
        // 从文件异步加载items
        var itemsLoaded = await ToDoListFileService.LoadFromFileAsync();
        if (itemsLoaded is not null)
        {
            foreach (var item in itemsLoaded)
            {
                _mainViewModel.ToDoItems.Add(new ToDoItemViewModel(item));
            }
        }
    }
}