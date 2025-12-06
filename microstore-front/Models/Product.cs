namespace microstore_front.Models;

/// <summary>
/// Représente un produit du catalogue.
/// Record immuable pour simplifier la sérialisation JSON (future API).
/// </summary>
public record Product(
    int Id,
    string Name,
    double Price,
    string ImageUrl
);
