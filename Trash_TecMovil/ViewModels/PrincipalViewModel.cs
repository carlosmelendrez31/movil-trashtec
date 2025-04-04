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
        string? llenadoStr = await _bluetoothService.ObtenerLlenadoAsync();
        if (string.IsNullOrEmpty(llenadoStr)) return;

        if (!int.TryParse(llenadoStr, out int llenado))
        {
            await App.Current.MainPage.DisplayAlert("Error", "El valor de llenado no es válido.", "OK");
            return;
        }

        string nombre = await App.Current.MainPage.DisplayPromptAsync("Nuevo Bote", "Ingrese el nombre:");
        if (string.IsNullOrEmpty(nombre)) return;

        string tipo = await App.Current.MainPage.DisplayPromptAsync("Tipo de Bote", "Ingrese el tipo:");
        if (string.IsNullOrEmpty(tipo)) return;

        var bote = new BoteModel { Nombre = nombre, Tipo = tipo, Llenado = llenado };
        Botes.Add(bote);

        // ?? Envía el bote a la API
        bool resultado = await _apiService.AgregarBoteAsync(bote);

        if (!resultado)
        {
            await App.Current.MainPage.DisplayAlert("Error", "No se pudo agregar el bote en la API.", "OK");
            Botes.Remove(bote); // Si falla, lo eliminamos de la colección local
        }
    }

}

