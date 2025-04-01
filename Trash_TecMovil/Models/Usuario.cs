using System.Text.Json.Serialization;

namespace Trash_TecMovil.Models;

public class Usuario
{
    [JsonPropertyName("nombreusuario")]
    public string NombreUsuario { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("contrasena")]
    public string Contrasena { get; set; }
}