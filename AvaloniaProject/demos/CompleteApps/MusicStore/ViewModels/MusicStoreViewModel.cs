using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.MusicStore.Messages;
using Avalonia.MusicStore.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Avalonia.MusicStore.ViewModels
{
    public partial class MusicStoreViewModel : ViewModelBase
    {
        // 取消
        private CancellationTokenSource? _cancellationTokenSource;

        [ObservableProperty]
        public partial string? SearchText { get; set; }

        [ObservableProperty]
        public partial bool IsBusy { get; private set; }

        [ObservableProperty]
        public partial AlbumViewModel? SelectedAlbum { get; set; }

        // 能够进行通知的集合
        public ObservableCollection<AlbumViewModel> SearchResults { get; } = new();


        /// <summary>
        /// 购买专辑命令（响应式命令） This relay command sends a message indicating that the selected album has been purchased, which will notify music store view to close.
        /// </summary>
        [RelayCommand]
        private void BuyMusic()
        {
            if (SelectedAlbum != null)
            {
                WeakReferenceMessenger.Default.Send(new MusicStoreClosedMessage(SelectedAlbum));
            }
        }

        /// <summary>
        /// Performs an asynchronous search for albums based on the provided term and updates the results.
        /// </summary>
        private async Task DoSearch(string? term)
        {
            // 可取消的图像加载
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _cancellationTokenSource.Token;

            IsBusy = true;
            SearchResults.Clear();

            var albums = await Album.SearchAsync(term);

            foreach (var album in albums)
            {
                var vm = new AlbumViewModel(album);
                SearchResults.Add(vm);
            }

            // 因此，如果仍有正在加载专辑封面的现有请求，它将被取消。
            // 同样，由于 _cancellationTokenSource 可能会被另一个线程异步替换，因此您必须使用存储为局部变量的副本进行操作
            if (!cancellationToken.IsCancellationRequested)
            {
                LoadCovers(cancellationToken);
            }

            IsBusy = false;
        }

        /// <summary>
        /// 加载封面图像 Asynchronously loads album cover images for each result, unless the operation is canceled.
        /// </summary>
        private async void LoadCovers(CancellationToken cancellationToken)
        {
            foreach (var album in SearchResults.ToList())
            {
                await album.LoadCover();

                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Triggered when the search text in music store view changes and initiates a new search operation.
        /// </summary>
        partial void OnSearchTextChanged(string? value)
        {
            _ = DoSearch(SearchText);
        }
    }
}
