using Microsoft.EntityFrameworkCore;
using OnlineMarket.DataAccess.Interfaces;
using OnlineMarket.Service.Common.Helpers;
using OnlineMarket.Service.Common.Utils;
using OnlineMarket.Service.Interfaces.Products;
using OnlineMarket.Service.ViewModels.Products;
using System.Diagnostics;

namespace OnlineMarket.Service.Services.Products;
public class ProductService : IProductService
{
    private readonly IUnitOfWork _repository;
    public ProductService(IUnitOfWork unitOfWork)
    {
        this._repository = unitOfWork;
    }
    public async Task<IEnumerable<ProductBaseViewModel>> GetAllAsync(PaginationParams @params)
    {
        var query = from product in _repository.Products.GetAll()
                    let discountPrice = _repository.ProductDiscounts.GetAll().Where(discount =>
                        discount.StartDate < TimeHelper.GetCurrentServerTime() 
                        && discount.EndDate > TimeHelper.GetCurrentServerTime()
                        && discount.ProductId == product.Id).Sum(x=>x.Price)
                    orderby product.CreatedAt descending
                    select new ProductBaseViewModel()
                    {
                        Id=product.Id,
                        Name = product.Name,
                        ImagePath = product.ImagePath,
                        OriginalPrice = product.Price,
                        DiscountPrice = discountPrice,
                        ResultPrice = product.Price - discountPrice
                    };
        return  await query.Skip((@params.PageNumber - 1) * @params.PageSize)
                          .Take(@params.PageSize).AsNoTracking()
                          .ToListAsync();
    }
}
