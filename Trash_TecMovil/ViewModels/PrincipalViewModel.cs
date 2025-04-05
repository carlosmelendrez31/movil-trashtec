using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Trash_TecMovil;
using Trash_TecMovil.Models;
using Trash_TecMovil.Services;
using Trash_TecMovil.Services.Trash_TecMovil.Services;

public partial class PrincipalViewModel : ObservableObject
{
    private HttpClient _httpClient;
    private const string Esp32Ip = "http://<ESP32_IP>/obtenerllenado";  // Reemplaza <ESP32_IP> con la IP de tu ESP32

    public int Llenado { get; set; }

    public PrincipalViewModel()
    {
        _httpClient = new HttpClient();
    }

   




}

