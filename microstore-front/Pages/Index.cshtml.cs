using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using microstore_front.Models;
using microstore_front.Services;

namespace microstore_front.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IProductService _productService;

    public IndexModel(ILogger<IndexModel> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    /// <summary>
    /// Liste des produits à afficher dans l'interface.
    /// </summary>
    public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();

    /// <summary>
    /// Message d'erreur en cas d'échec du chargement (affiché dans l'UI).
    /// </summary>
    public string? ErrorMessage { get; set; }

    public async Task OnGetAsync()
    {
        try
        {
            _logger.LogInformation("Chargement du catalogue de produits...");
            Products = await _productService.GetProductsAsync();
            _logger.LogInformation("Catalogue chargé avec succès : {Count} produits", Products.Count());
        }
        catch (Exception ex)
        {
            // GESTION D'ERREUR CRITIQUE : Ne jamais crasher la page
            // En production, si le Backend est down, on affiche un message utilisateur
            _logger.LogError(ex, "Erreur lors du chargement du catalogue produits");

            ErrorMessage = "⚠️ Le catalogue de produits est temporairement indisponible. Veuillez réessayer plus tard.";
            Products = Enumerable.Empty<Product>(); // Liste vide pour éviter les erreurs de rendu
        }
    }
}
