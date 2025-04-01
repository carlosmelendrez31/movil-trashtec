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
    private readonly BluetoothService _bluetoothService;
    private readonly ApiService _apiService;

    [ObservableProperty]
    private ObservableCollection<BoteModel> botes = new();

    public ICommand AgregarBoteCommand { get; }

    public PrincipalViewModel(BluetoothService bluetoothService, ApiService apiService)
    {
        _bluetoothService = bluetoothService;
        _apiService = apiService;
        AgregarBoteCommand = new AsyncRelayCommand(AgregarBoteAsync);
    }

    private async Task AgregarBoteAsync()
    {
        // Conectar a Bluetooth y obtener el llenado
        string? llenadoStr = await _bluetoothService.ObtenerLlenadoAsync();
        if (string.IsNullOrEmpty(llenadoStr)) return;

        int llenado = int.Parse(llenadoStr);

        // Solicitar datos del usuario
        string nombre = await App.Current.MainPage.DisplayPromptAsync("Nuevo Bote", "Ingrese el nombre del bote:");
        string tipo = await App.Current.MainPage.DisplayPromptAsync("Nuevo Bote", "Ingrese el tipo de bote:");

        if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(tipo)) return;

        // Enviar los datos a la API
        var nuevoBote = new BoteModel
        {
            Nombre = nombre,
            Tipo = tipo,
            Llenado = llenado
        };

        bool exito = await _apiService.RegistrarBoteAsync(nuevoBote);
        if (exito)
        {
            Botes.Add(nuevoBote);
        }
        else
        {
            await App.Current.MainPage.DisplayAlert("Error", "No se pudo registrar el bote.", "OK");
        }
    }
}
