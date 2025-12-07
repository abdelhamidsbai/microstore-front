using System.Net.Http.Json;
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
        try
        {
            // Appel au backend (la BaseAddress est injectée via Program.cs et les variables d'environnement)
            var response = await _httpClient.GetAsync("api/products");
            response.EnsureSuccessStatusCode();

            var products = await response.Content.ReadFromJsonAsync<IEnumerable<Product>>();
            return products ?? Enumerable.Empty<Product>();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Erreur lors de l'appel à ProductApi");
            throw; // La page gère l'affichage d'un message utilisateur
        }
    }
}
