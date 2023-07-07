using Ecommerce.ViewModels.Catalog.Products;
using Ecommerce.ViewModels.Catalog.Products.Common;
using Ecommerce.ViewModels.Catalog.Products.Public;

namespace Ecommerce.Application.Catalog.Products
{
    public interface IPublicProductService
    {
        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request);
    }
}
