using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBlog.Application.DTOs;
using MyBlog.Maui.ViewModels;

namespace MyBlog.Maui.Views;

public partial class PostDetailPage : ContentPage
{
    private readonly PostDetailViewModel _viewModel;
    

    public PostDetailPage(PostDetailViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}
