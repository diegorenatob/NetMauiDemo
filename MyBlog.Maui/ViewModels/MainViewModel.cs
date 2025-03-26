using MyBlog.Application.DTOs;
using MyBlog.Application.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MyBlog.Maui.Views;

namespace MyBlog.Maui.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IPostService _postService;

        public ObservableCollection<PostDto> Posts { get; } = new();

        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                if (_isRefreshing == value) return;
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }

        public ICommand RefreshCommand { get; }

        public MainViewModel(IPostService postService)
        {
            _postService = postService;

            RefreshCommand = new Command(async () => await RefreshPostsAsync());

            Task.Run(LoadPostsAsync);
        }

        private async Task LoadPostsAsync()
        {
            Posts.Clear();
            var posts = await _postService.GetAllPostsAsync();
            foreach (var p in posts)
            {
                Posts.Add(p);
            }
        }

        private async Task RefreshPostsAsync()
        {
            IsRefreshing = true;

            try
            {
                await _postService.FetchAndStoreNewPostsAsync();
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Offline", "No internet connection available.", "OK");
                IsRefreshing = false;
                return;
            }

            await LoadPostsAsync();

            IsRefreshing = false;
        }
        
        public ICommand OpenPostCommand => new Command<PostDto>(async (post) =>
        {
            if (post != null)
            {
                var navParam = new Dictionary<string, object>
                {
                    { "Post", post }
                };
                await Shell.Current.GoToAsync(nameof(PostDetailPage), navParam);
            }
            // Jeito tradicional de passar parametros 
            // var page = new PostDetailPage(post);
            // await Navigation.PushAsync(page);
        });

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion
    }
}