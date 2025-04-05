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
            HttpResponseMessage response = await _httpClient.PostAsync("https://p0tcljpd-7196.usw3.devtunnels.ms/api/auth/login", content);

            // Log del código de estado
            Console.WriteLine($"📥 Código de estado: {response.StatusCode}");

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"✅ Respuesta del servidor: {responseBody}");

                // Deserialización de la respuesta
                var loginResponse = JsonSerializer.Deserialize<LoginResponse>(responseBody);

                if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.token))
                {
                    Console.WriteLine($"🔑 Token recibido: {loginResponse.token}");

                    // Almacenar token de manera segura
                    await SecureStorage.SetAsync("AuthToken", loginResponse.token);
                    await SecureStorage.SetAsync("nombreusuario", ObtenerNombreUsuarioDesdeToken(loginResponse.token));
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


    public string ObtenerNombreUsuarioDesdeToken(string token)
    {
        try
        {
            // El JWT tiene 3 partes separadas por ".", nos interesa el segundo: el payload
            var partes = token.Split('.');
            if (partes.Length < 2)
                return string.Empty;

            var payload = partes[1];

            // Agregar padding si es necesario (Base64 debe tener longitud múltiplo de 4)
            switch (payload.Length % 4)
            {
                case 2: payload += "=="; break;
                case 3: payload += "="; break;
            }

            var bytes = Convert.FromBase64String(payload);
            var json = Encoding.UTF8.GetString(bytes);

            var payloadData = JsonSerializer.Deserialize<Dictionary<string, object>>(json);

            // Intenta obtener el nombre del claim
            if (payloadData != null &&
                payloadData.TryGetValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", out object nombre))
            {
                return nombre.ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error al obtener el nombre desde el token: {ex.Message}");
        }

        return string.Empty;
        
    }
}


public class LoginResponse
{
    public string token { get; set; }
}
