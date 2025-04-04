using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private const string ApiUrl = "https://gn5n9hbf-7196.usw3.devtunnels.ms/api/auth/login"; // Reemplaza con la URL de tu API

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> LoginAsync(string email, string contrasena)
    {
        try
        {
            var json = JsonSerializer.Serialize(new { email, contrasena });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Enviar solicitud al servidor
            var response = await _httpClient.PostAsync(ApiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<AuthResponse>(responseBody);

                if (responseData != null && !string.IsNullOrEmpty(responseData.Token))
                {
                    // Guardar el token para futuros usos
                    SecureStorage.SetAsync("AuthToken", responseData.Token);
                    return responseData.Token;
                }
                else
                {
                    Console.WriteLine("❌ Respuesta no contiene un token válido.");
                    return string.Empty;
                }
            }
            else
            {
                Console.WriteLine($"❌ Error en login: {response.StatusCode}");
                return string.Empty;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error: {ex.Message}");
            return string.Empty;
        }
    }
}

public class AuthResponse
{
    public string Token { get; set; }
}
