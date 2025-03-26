using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using MyBlog.Application.DTOs;

namespace MyBlog.Maui.ViewModels
{
    [QueryProperty(nameof(Post), "Post")]
    public partial class PostDetailViewModel : ObservableObject
    {
        [ObservableProperty]
        private PostDto _post;
     
       // Código generado automáticamente
        // public PostDto Post
        // {
        //     get => _post;
        //     set => SetProperty(ref _post, value);
        // }
        public PostDetailViewModel()
        {
         
        }
    }
}