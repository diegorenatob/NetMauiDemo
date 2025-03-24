using MyBlog.Application.DTOs;
using MyBlog.Application.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

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

            // Load initially
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

            // 1) Fetch remote + store new in DB
            await _postService.FetchAndStoreNewPostsAsync();

            // 2) Reload from local DB
            await LoadPostsAsync();

            IsRefreshing = false;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}