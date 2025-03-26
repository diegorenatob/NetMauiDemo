using Microsoft.Maui.Networking;
using MyBlog.Application.Interfaces;

namespace MyBlog.Maui.Services
{
    public class MauiConnectivityService : IConnectivityService
    {
        public bool HasInternetConnection()
        {
            return Connectivity.Current.NetworkAccess == NetworkAccess.Internet;
        }
    }
}