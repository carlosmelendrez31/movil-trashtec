using System.Text.Json;
using System.Text;
using Trash_TecMovil.Services.Trash_TecMovil.Services;
using Trash_TecMovil.Models;

namespace Trash_TecMovil.View;

public partial class Principal : ContentPage
{

    private const string ApiUrl = "https://p0tcljpd-7196.usw3.devtunnels.ms/api/Dispositivos/agregar"; // 👉 cambia TU_API por la URL de tu API
    private const string Esp32Ip = "http://192.168.1.100/obtenerllenado"; // 👉 IP fija del ESP32
   

    public Principal()
	{
		InitializeComponent();
       

    }
    

   

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var nombre = await DisplayPromptAsync("Nombre", "Ingresa el nombre del bote:");
        var tipo = await DisplayPromptAsync("Tipo", "Ingresa el tipo del bote:");

        if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(tipo))
        {
            await DisplayAlert("Error", "Nombre y tipo son obligatorios", "OK");
            return;
        }

        var nuevoBote = new
        {
            nombre = nombre,
            tipo = tipo
        };

        var json = JsonSerializer.Serialize(nuevoBote);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            using var client = new HttpClient();
            var response = await client.PostAsync(ApiUrl, content);

            if (response.IsSuccessStatusCode)
                await DisplayAlert("Éxito", "Bote agregado correctamente", "OK");
            else
                await DisplayAlert("Error", "No se pudo agregar el bote", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Fallo al conectar con la API: {ex.Message}", "OK");
        }

    }
    private async void ObtenerLlenado_Clicked(object sender, EventArgs e)
    {
        try
        {
            using var client = new HttpClient();
            var response = await client.GetStringAsync(Esp32Ip);

            if (int.TryParse(response, out int porcentaje))
            {
                lblPorcentaje.Text = $"{porcentaje}%";

                if (porcentaje < 30)
                    semaforo.Color = Colors.Green;
                else if (porcentaje < 70)
                    semaforo.Color = Colors.Gold;
                else
                    semaforo.Color = Colors.Red;
            }
            else
            {
                await DisplayAlert("Error", "Respuesta inválida del ESP32", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo obtener el llenado: {ex.Message}", "OK");
        }
    }
}