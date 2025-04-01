using Microsoft.Extensions.Logging;
using Trash_TecMovil.View;
using Trash_TecMovil.Services;
using System.Runtime.CompilerServices;
using CommunityToolkit.Maui;
using Trash_TecMovil.Services.Trash_TecMovil.Services;

namespace Trash_TecMovil
{


    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            }).UseMauiCommunityToolkit();
            // Registrar servicios
            builder.Services.AddSingleton<BluetoothService>();
            builder.Services.AddSingleton<ApiService>();
            builder.Services.AddSingleton<PrincipalViewModel>();
            return builder.Build();
        }
    }
}