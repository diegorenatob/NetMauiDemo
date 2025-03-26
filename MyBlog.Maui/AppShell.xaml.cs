using MyBlog.Maui.Views;

namespace MyBlog.Maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(PostDetailPage), typeof(PostDetailPage));

	}
}

