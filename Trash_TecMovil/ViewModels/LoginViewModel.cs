using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

public class LoginViewModel
{
    private readonly HttpClient _httpClient;

    public LoginViewModel()
    {
        _httpClient = new HttpClient();
    }

    public async Task<bool> IniciarSesion(string email, string contrasena)
    {
        var json = JsonSerializer.Serialize(new { email, contrasena });
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync("https://localhost:7916/api/auth/login", content);

        if (response.IsSuccessStatusCode)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            var loginResponse = JsonSerializer.Deserialize<LoginResponse>(responseBody);

            // 🔹 Guardar el token en Preferences
            Preferences.Set("AuthToken", loginResponse.Token);

            // 🔹 Decodificar el nombreusuario desde el token
            var nombreUsuario = ObtenerNombreUsuarioDesdeToken(loginResponse.Token);
            Preferences.Set("NombreUsuario", nombreUsuario);

            return true;
        }
        else
        {
            Console.WriteLine("❌ Error en el inicio de sesión.");
            return false;
        }
    }

    private string ObtenerNombreUsuarioDesdeToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var nombreUsuario = jwtToken.Claims.FirstOrDefault(c => c.Type == "nombreusuario")?.Value;
        return nombreUsuario ?? "Usuario";
    }
}

// 🔹 Modelo para recibir la respuesta de la API
public class LoginResponse
{
    public string Token { get; set; }
    public List<string> Dispositivos { get; set; }
}
