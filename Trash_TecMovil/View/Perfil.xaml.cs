using Microsoft.Maui.Storage;
using System;
using System.Text.Json;

namespace Trash_TecMovil.View
{
    public partial class Perfil : ContentPage
    {
        public Perfil()
        {
            InitializeComponent();
            CargarDatosUsuario();
        }

        private async void CargarDatosUsuario()
        {
            var nombre = await SecureStorage.GetAsync("nombreusuario") ?? "Usuario";
            var token = await SecureStorage.GetAsync("AuthToken");

            NombreLabel.Text = $"Hola, {nombre} ??";

            if (!string.IsNullOrEmpty(token))
            {
                var correo = ObtenerCorreoDesdeToken(token);
                CorreoLabel.Text = correo ?? "No disponible";
            }
        }

        private string ObtenerCorreoDesdeToken(string token)
        {
            try
            {
                var partes = token.Split('.');
                if (partes.Length < 2) return null;

                var payload = partes[1];
                switch (payload.Length % 4)
                {
                    case 2: payload += "=="; break;
                    case 3: payload += "="; break;
                }

                var bytes = Convert.FromBase64String(payload);
                var json = System.Text.Encoding.UTF8.GetString(bytes);
                var payloadData = JsonSerializer.Deserialize<Dictionary<string, object>>(json);

                if (payloadData != null &&
                    payloadData.TryGetValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", out object correo))
                {
                    return correo?.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"? Error al obtener correo desde el token: {ex.Message}");
            }

            return null;
        }
    }
}
