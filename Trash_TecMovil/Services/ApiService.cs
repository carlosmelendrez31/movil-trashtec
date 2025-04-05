using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Trash_TecMovil.Models;

namespace Trash_TecMovil.Services
{



    namespace Trash_TecMovil.Services
    {
        public class ApiService
        {
            public async Task<bool> AgregarBoteAsync(BoteModel bote)
            {
                try
                {
                    var json = JsonSerializer.Serialize(bote);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    using var client = new HttpClient();
                    var response = await client.PostAsync("https://p0tcljpd-7196.usw3.devtunnels.ms/api/Dispositivos/agregar", content);

                    return response.IsSuccessStatusCode;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error al enviar bote: {ex.Message}");
                    return false;
                }
            }
        }
    }

}
