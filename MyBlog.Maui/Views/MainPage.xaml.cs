namespace MyBlog.Maui.Views;
using MyBlog.Maui.ViewModels;
public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}