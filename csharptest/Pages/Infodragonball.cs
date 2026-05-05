using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using csharptest.Models;
using System.Text.Json;

namespace csharptest.Pages
{
    public class ListdragonballModel : PageModel {
        private readonly IHttpClientFactory _httpClientFactory;

        public ListdragonballModel(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }

        public Personagem? InfoPersonagem { get; set; }
        public string? id { get; set; }

        public async Task<IActionResult> OnGetAsync(string cod) {
            id = cod;
            var client = _httpClientFactory.CreateClient("Listdragonball");

            var response = await client.GetAsync($"alpha/{cod}");
            if (!response.IsSuccessStatusCode) {
                return NotFound();
            }

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var artigoResponse = JsonSerializer.Deserialize<List<DragonBallApiResponse>>(json, options)?.FirstOrDefault();

            if (artigoResponse != null) {
                InfoPersonagem = new Personagem {
                    name = artigoResponse.name?.official ?? "",
                    id = artigoResponse.id ?? "",
                    imgUrl = artigoResponse.image?.png ?? ""
                };
            }

            return Page();
        }
    }
}