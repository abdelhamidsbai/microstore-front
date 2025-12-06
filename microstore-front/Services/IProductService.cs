using microstore_front.Models;

namespace microstore_front.Services;

/// <summary>
/// Contrat de service pour la récupération des produits.
/// Permet l'abstraction : Mock en développement, HttpClient en production.
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Récupère la liste de tous les produits disponibles.
    /// </summary>
    /// <returns>Collection de produits (vide si erreur gérée côté implémentation).</returns>
    Task<IEnumerable<Product>> GetProductsAsync();
}
