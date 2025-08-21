using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.MusicStore.Messages;
using Avalonia.MusicStore.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Avalonia.MusicStore.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        public ObservableCollection<AlbumViewModel> Albums { get; } = new();

        public MainWindowViewModel()
        {
            // 加载专辑
            LoadAlbums();
        }

        /// <summary>
        /// 从VM发消息 This relay command send a message to initiate album purchase, adds the result to the collection and saves it to disk.
        /// 绑定和同步命令一样，此处方法名多一个Async在{Binding AddAlbumCommand}里也会识别
        /// </summary>
        [RelayCommand]
        private async Task AddAlbumAsync()
        {
            var album = await WeakReferenceMessenger.Default.Send(new PurchaseAlbumMessage());
            if (album is not null)
            {
                Albums.Add(album);
                await album.SaveToDiskAsync();
            }
        }

        /// <summary>
        /// Loads albums and their covers from cache.
        /// </summary>
        private async void LoadAlbums()
        {
            // 加载缓存
            var albums = (await Album.LoadCachedAsync()).Select(x => new AlbumViewModel(x)).ToList();
            foreach (var album in albums)
            {
                Albums.Add(album);
            }
            var coverTasks = albums.Select(album => album.LoadCover());
            await Task.WhenAll(coverTasks);
        }
    }
}
