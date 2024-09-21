using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class ProductClient
{
    private readonly HttpClient _httpClient;

    public ProductClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Product> GetProductAsync(int productId)
    {
        var response = await _httpClient.GetAsync($"/api/products/{productId}");
        response.EnsureSuccessStatusCode();
        var product = await response.Content.ReadFromJsonAsync<Product>();
        return product;
    }
}
