using Avalonia.Controls;
using Avalonia.MusicStore.Messages;
using Avalonia.MusicStore.ViewModels;
using CommunityToolkit.Mvvm.Messaging;

namespace Avalonia.MusicStore.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (Design.IsDesignMode)
                return;

            // 注册消息处理器（购买专辑消息）
            WeakReferenceMessenger.Default.Register<MainWindow, PurchaseAlbumMessage>(this, static (w, m) =>
            {
                // MusicStore窗口
                var dialog = new MusicStoreWindow
                {
                    DataContext = new MusicStoreViewModel()
                };
                // 用对话框内容回消息
                m.Reply(dialog.ShowDialog<AlbumViewModel?>(w));
            });
        }
    }
}
