using microstore_front.Models;

namespace microstore_front.Services;

/// <summary>
/// Implémentation API du service produit (À ACTIVER PLUS TARD).
/// Consommera l'API Backend via HttpClient.
/// </summary>
public class ApiProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ApiProductService> _logger;

    public ApiProductService(HttpClient httpClient, ILogger<ApiProductService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        // TODO: Implémenter l'appel à l'API réelle
        // Exemple:
        // try
        // {
        //     var response = await _httpClient.GetAsync("/api/products");
        //     response.EnsureSuccessStatusCode();
        //     return await response.Content.ReadFromJsonAsync<IEnumerable<Product>>() ?? Enumerable.Empty<Product>();
        // }
        // catch (HttpRequestException ex)
        // {
        //     _logger.LogError(ex, "Erreur lors de l'appel à l'API Backend");
        //     throw; // Sera géré par le PageModel
        // }

        throw new NotImplementedException("L'API Backend n'est pas encore prête. Utilisez MockProductService.");
    }
}
