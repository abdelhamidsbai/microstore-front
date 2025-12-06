using microstore_front.Models;

namespace microstore_front.Services;

/// <summary>
/// Implémentation Mock du service produit.
/// Retourne des données fictives pour le développement sans dépendance Backend.
/// Simule la latence réseau pour tester le comportement asynchrone.
/// </summary>
public class MockProductService : IProductService
{
    private static readonly List<Product> _mockProducts = new()
    {
        new Product(1, "Laptop Dell XPS 15", 1299.99, "https://placehold.co/300x200?text=Laptop"),
        new Product(2, "Souris Logitech MX Master 3", 99.99, "https://placehold.co/300x200?text=Souris"),
        new Product(3, "Clavier Mécanique Keychron K2", 89.99, "https://placehold.co/300x200?text=Clavier"),
        new Product(4, "Écran 4K LG 27 pouces", 449.99, "https://placehold.co/300x200?text=Ecran"),
        new Product(5, "Casque Sony WH-1000XM5", 349.99, "https://placehold.co/300x200?text=Casque")
    };

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        // Simule la latence réseau d'un appel API réel
        await Task.Delay(500);

        return _mockProducts;
    }
}
