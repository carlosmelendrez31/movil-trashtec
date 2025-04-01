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
            private readonly HttpClient _httpClient;

            public ApiService()
            {
                _httpClient = new HttpClient { BaseAddress = new Uri("https://tu-api-supabase.com/") };
            }

            public async Task<bool> RegistrarBoteAsync(BoteModel bote)
            {
                var response = await _httpClient.PostAsJsonAsync("botes", bote);
                return response.IsSuccessStatusCode;
            }
        }
    }

}
