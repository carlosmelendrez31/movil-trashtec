using System.Text.Json;
using System.Text;
using Trash_TecMovil.Services.Trash_TecMovil.Services;
using Trash_TecMovil.Models;
using System.Net.Http.Headers;

namespace Trash_TecMovil.View;

public partial class Principal : ContentPage
{

    private const string ApiUrl = "https://p0tcljpd-7196.usw3.devtunnels.ms/api/Dispositivos/agregar"; 
    private const string Esp32Ip = "http://192.168.100.200/obtenerllenado"; 
    private int _llenado;
    private bool _boteAgregado;

    [Obsolete]
    public Principal()
	{
		InitializeComponent();
        _llenado = 0;
        _boteAgregado = false;

        // Llamada periódica para actualizar el llenado
        Device.StartTimer(TimeSpan.FromSeconds(1), () =>
        {
            if (_boteAgregado)
            {
                GetLlenadoAsync();
            }
            return true; // Continuar con el temporizador
        });
        CargarNombreUsuario();

    }

    private string ObtenerNombreDesdeToken(string token)
    {
        var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var nombreClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "name");
        return nombreClaim?.Value ?? "Usuario";
    }

    private async void CargarNombreUsuario()
    {
        var token = await SecureStorage.GetAsync("auth_token");

        if (!string.IsNullOrEmpty(token))
        {
            var nombre = ObtenerNombreDesdeToken(token);
            NombreUsuarioLabel.Text = $"Hola, {nombre} 👋";
            NombreUsuarioLabel.IsVisible = true;
        }
    }


    private async Task AgregarBoteAsync()
    {
        if (string.IsNullOrWhiteSpace(NombreBote.Text) || string.IsNullOrWhiteSpace(TipoBote.Text))
            return;

        var json = $"{{ \"nombre\": \"{NombreBote.Text}\", \"tipo\": \"{TipoBote.Text}\" }}";
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using var client = new HttpClient();

        // 👉 Obtener el token guardado tras login
        var token = await SecureStorage.GetAsync("auth_token");

        if (!string.IsNullOrEmpty(token))
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.PostAsync(ApiUrl, content);

        if (response.IsSuccessStatusCode)
        {
            _boteAgregado = true;

            NombreLabel.Text = $"Bote: {NombreBote.Text}";
            NombreLabel.IsVisible = true;
            LlenadoLabel.IsVisible = true;
            SemaforoBox.IsVisible = true;

            NombreBote.IsVisible = false;
            TipoBote.IsVisible = false;
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "No se pudo agregar el bote", "OK");
        }
    }

    // Método para obtener el llenado desde el ESP32
    private async Task GetLlenadoAsync()
    {
        try
        {
            using var client = new HttpClient();
            var resultado = await client.GetStringAsync(Esp32Ip);

            if (int.TryParse(resultado, out int porcentaje))
            {
                LlenadoLabel.Text = $"Llenado: {porcentaje}%";

                // Cambiar el color del semáforo según porcentaje
                if (porcentaje >= 80)
                    SemaforoBox.Color = Colors.Red;
                else if (porcentaje >= 40)
                    SemaforoBox.Color = Colors.Orange;
                else
                    SemaforoBox.Color = Colors.Green;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error al obtener llenado: {ex.Message}");
        }
    }
    

    // Método vinculado al botón para agregar el bote
    private async void OnAgregarBoteClicked(object sender, EventArgs e)
    {
        await AgregarBoteAsync();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {

        await AgregarBoteAsync();
    }
}




