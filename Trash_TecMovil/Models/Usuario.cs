using System.Text.Json.Serialization;

namespace Trash_TecMovil.Models;

public class Usuario
{
    [JsonPropertyName("nombreusuario")]
    public string nombreusuario { get; set; }

    [JsonPropertyName("email")]
    public string email { get; set; }

    [JsonPropertyName("contrasena")]
    public string contrasena { get; set; }
}