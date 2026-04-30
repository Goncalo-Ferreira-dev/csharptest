using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using csharptest.Models;
using System.Text.Json;

namespace csharptest.Pages
{
    public class InfopaisModel : PageModel {
        private readonly IHttpClientFactory _httpClientFactory;

        public InfopaisModel(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }

        public Pais? InfoPais { get; set; }
        public string? CodigoPais { get; set; }

        public async Task<IActionResult> OnGetAsync(string cod) {
            CodigoPais = cod;
            var client = _httpClientFactory.CreateClient("RestCountries");

            var response = await client.GetAsync($"alpha/{cod}");
            if (!response.IsSuccessStatusCode) {
                return NotFound();
            }

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var artigoResponse = JsonSerializer.Deserialize<List<CountryApiResponse>>(json, options)?.FirstOrDefault();

            if (artigoResponse != null) {
                InfoPais = new Pais {
                    OfficialName = artigoResponse.name?.official ?? "",
                    Cca2 = artigoResponse.cca2 ?? "",
                    FlagUrl = artigoResponse.flags?.png ?? ""
                };
            }

            return Page();
        }
    }
}