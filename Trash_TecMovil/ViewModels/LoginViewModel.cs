using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Trash_TecMovil.Models;

public class LoginViewModel
{
    private readonly HttpClient _httpClient;

    // Usuario inicializado para evitar problemas
    public Usuario Usuario { get; set; } = new Usuario();

    public LoginViewModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> IniciarSesion(string email, string contrasena)
    {
        try
        {
            // Serialización de datos
            var json = JsonSerializer.Serialize(new { email, contrasena });
            Console.WriteLine($"➡ Enviando JSON: {json}");

            // Configuración del contenido de la solicitud
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Petición al servidor
            HttpResponseMessage response = await _httpClient.PostAsync("https://gn5n9hbf-7196.usw3.devtunnels.ms/api/auth/login", content);

            // Log del código de estado
            Console.WriteLine($"📥 Código de estado: {response.StatusCode}");

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"✅ Respuesta del servidor: {responseBody}");

                // Deserialización de la respuesta
                var loginResponse = JsonSerializer.Deserialize<LoginResponse>(responseBody);

                if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.Token))
                {
                    Console.WriteLine($"🔑 Token recibido: {loginResponse.Token}");

                    // Almacenar token de manera segura
                    await SecureStorage.SetAsync("AuthToken", loginResponse.Token);
                    await SecureStorage.SetAsync("nombreusuario", ObtenerNombreUsuarioDesdeToken(loginResponse.Token));
                    return true;
                }
                else
                {
                    Console.WriteLine("❌ Error: La respuesta no contiene un token válido.");
                }
            }
            else
            {
                // Log de error cuando la respuesta no es exitosa
                string errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"❌ Error en la respuesta del servidor. Código: {response.StatusCode}. Detalles: {errorContent}");
            }
        }
        catch (HttpRequestException httpEx)
        {
            Console.WriteLine($"❌ Error HTTP: {httpEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error general: {ex.Message}");
        }
        return false;
    }

    private string ObtenerNombreUsuarioDesdeToken(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            return jwtToken.Claims.FirstOrDefault(c => c.Type == "nombreusuario")?.Value ?? "Usuario";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error al procesar el token JWT: {ex.Message}");
            return "Usuario";
        }
    }
}

public class LoginResponse
{
    public string Token { get; set; }
}
