using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.RazorPages;



using csharptest.Models;

using System.Net.Http;

using System.Text.Json;

using System.Threading.Tasks;

using System.Collections.Generic;


namespace csharptest.Pages;


public class Index2Model : PageModel

{

    private readonly IHttpClientFactory _httpClientFactory;


    public Index2Model(IHttpClientFactory httpClientFactory)

    {

        _httpClientFactory = httpClientFactory;

    }


    public List<Personagem> Personagens { get; set; } = new();


    public async Task OnGetAsync()

    {

        //var client = _httpClientFactory.CreateClient();

        //var response = await client.GetAsync("https://restcountries.com/v3.1/all");

        var client = _httpClientFactory.CreateClient("Listdragonball");

        var response = await client.GetAsync("https://dragonball-api.com/characters"); // eu sei que está mal e que falta o /api/ mas se eu adicionar dá erro porque recebe as informações todas e não consigo filtrar para receber só o nome, id e imagem.



        if (response.IsSuccessStatusCode)

        {

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var dados = JsonSerializer.Deserialize<List<DragonBallApiResponse>>(json, options);


            Personagens = dados.Select(d => new Personagem

            {

                name = d.name?.official,

                id = d.id,

                imgUrl = d.image?.png

            }).ToList();

        }

    }


    /*

private readonly ILogger<IndexModel> _logger;



    public IndexModel(ILogger<IndexModel> logger)

    {

        _logger = logger;

    }


    public void OnGet()

    {


    }

*/

}