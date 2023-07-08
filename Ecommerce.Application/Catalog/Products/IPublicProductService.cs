using Ecommerce.ViewModels.Catalog.Common;
using Ecommerce.ViewModels.Catalog.Products;

namespace Ecommerce.Application.Catalog.Products
{
    public interface IPublicProductService
    {
        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request);
        //Task<List<ProductViewModel>> GetAll();
    }
}
