using OnlineMarket.Domain.Entities.Categories;

namespace OnlineMarket.Service.ViewModels.Products;
public class ProductViewModel
{
    public long Id { get; set; }

    public string Name { get; set; } = String.Empty;

    public double Price { get; set; }

    public string Description { get; set; } = String.Empty;

    public string ImagePath { get; set; } = String.Empty;

    public long CategoryId { get; set; }

    public virtual Category Category { get; set; } = default!;
}
