using Ecommerce.ViewModels.Catalog.Common;
using Ecommerce.ViewModels.Catalog.ProductImages;
using Ecommerce.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Catalog.Products
{
    public interface IProductService
    {
        Task<int> Create(ProductCreateRequest request);

        Task<int> Update(ProductUpdateRequest request);

        Task<int> Delete(int productId);

        Task<ProductViewModel> GetById(int productId);

        Task<bool> UpdatePrice(int productId, decimal newPrice);

        Task<bool> UpdateStock(int productId, int addedQuantity);

        Task AddViewcount(int productId);

        Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);

        Task<ProductImageViewModel> GetImageById(int imageId);
        Task<int> AddImage(int productId, ProductImageCreateRequest productImage);
        Task<int> RemoveImage(int imageId);
        Task<int> UpdateImage(int imageId, ProductImageUpdateRequest productImage);
		Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request);

		Task<List<ProductImageViewModel>> GetListImages(int productId);
    }
}

