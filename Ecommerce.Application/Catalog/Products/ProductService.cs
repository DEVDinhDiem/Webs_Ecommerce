using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Ecommerce.Application.Common;
using Ecommerce.Data.EF;
using Ecommerce.Data.Entities;
using Ecommerce.Utilities.Exceptions;
using Ecommerce.ViewModels.Catalog.Common;
using Ecommerce.ViewModels.Catalog.ProductImages;
using Ecommerce.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.Catalog.Products
{
    public class ProductService : IProductService
    {
        private readonly EcommerceDbContext _context;
        private readonly IStorageService _storageService;
        public ProductService(EcommerceDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }


        public async Task AddViewcount(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {

                Name = request.Name,
                Description = request.Description,
                Details = request.Details,
                SeoDescription = request.SeoDescription,
                SeoTitle = request.SeoTitle,
                SeoAlias = request.SeoAlias,
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,

            };
            //Save image
            if (request.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new EcommerceException($"Cannot find a product: {productId}");

            var images = _context.ProductImages.Where(i => i.ProductId == productId);
            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }

            _context.Products.Remove(product);

            return await _context.SaveChangesAsync();
        }
        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            //var productTranslations = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == request.Id 
            /*&& x.LanguageId == request.LanguageId);*/

            if (product == null) throw new EcommerceException($"Cannot find a product with id: {request.Id}");

            product.Name = request.Name;
            product.SeoAlias = request.SeoAlias;
            product.SeoDescription = request.SeoDescription;
            product.SeoTitle = request.SeoTitle;
            product.Description = request.Description;
            product.Details = request.Details;

            //Save image
            if (request.ThumbnailImage != null)
            {
                var thumbnailImage = await _context.ProductImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.ProductId == request.Id);
                if (thumbnailImage != null)
                {
                    thumbnailImage.FileSize = request.ThumbnailImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(request.ThumbnailImage);
                    _context.ProductImages.Update(thumbnailImage);
                }
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new EcommerceException($"Cannot find a product with id: {productId}");
            product.Price = newPrice;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int productId, int addedQuantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new EcommerceException($"Cannot find a product with id: {productId}");
            product.Stock += addedQuantity;
            return await _context.SaveChangesAsync() > 0;
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<ProductViewModel> GetById(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            //var productTranslation = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == productId
            //&& x.LanguageId == languageId);

            var productViewModel = new ProductViewModel()
            {
                Id = product.Id,
                DateCreated = product.DateCreated,
                Description = product != null ? product.Description : null,
                //LanguageId = productTranslation.LanguageId,
                Details = product != null ? product.Details : null,
                Name = product != null ? product.Name : null,
                OriginalPrice = product.OriginalPrice,
                Price = product.Price,
                SeoAlias = product != null ? product.SeoAlias : null,
                SeoDescription = product != null ? product.SeoDescription : null,
                SeoTitle = product != null ? product.SeoTitle : null,
                Stock = product.Stock,
                ViewCount = product.ViewCount
            };
            return productViewModel;
        }
        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request)
        {
            //1. Select join
            var query = from p in _context.Products
                            //join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId into ppic
                        from pic in ppic.DefaultIfEmpty()
                        join c in _context.Categories on pic.CategoryId equals c.Id into picc
                        from c in picc.DefaultIfEmpty()
                            //join pi in _context.ProductImages on p.Id equals pi.ProductId into ppi
                            //from pi in ppi.DefaultIfEmpty()
                            //where pt.LanguageId == request.LanguageId && pi.IsDefault == true
                            //select new { p, pt, pic, pi };
                        select new { p, pic };
            //2. filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.p.Name.Contains(request.Keyword));

            if (request.CategoryId != null && request.CategoryId.Count > 0)
            {
                //query = query.Where(p => p.pic.CategoryId == request.CategoryId);
                query = query.Where(p => request.CategoryId.Contains(p.pic.CategoryId));
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.p.Description,
                    Details = x.p.Details,
                    //LanguageId = x.p.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.p.SeoAlias,
                    SeoDescription = x.p.SeoDescription,
                    SeoTitle = x.p.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount,
                    //ThumbnailImage = x.pi.ImagePath
                }).ToListAsync();

            //4. Select and projection
            //var pagedResult = new PagedResult<ProductVm>()
            //var pagedResult = new PagedResult<ProductViewModel>()
            //{
            //    TotalRecords = totalRow,
            //    PageSize = request.PageSize,
            //    PageIndex = request.PageIndex,
            //    Items = data
            //};
            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pagedResult;
        }
        public async Task<ProductImageViewModel> GetImageById(int imageId)
        {
            var image = await _context.ProductImages.FindAsync(imageId);
            if (image == null)
                throw new EcommerceException($"Cannot find an image with id {imageId}");

            var viewModel = new ProductImageViewModel()
            {
                Caption = image.Caption,
                DateCreated = image.DateCreated,
                FileSize = image.FileSize,
                Id = image.Id,
                ImagePath = image.ImagePath,
                IsDefault = image.IsDefault,
                ProductId = image.ProductId,
                SortOrder = image.SortOrder
            };
            return viewModel;
        }
        public async Task<List<ProductImageViewModel>> GetListImages(int productId)
        {
            return await _context.ProductImages.Where(x => x.ProductId == productId)
                .Select(i => new ProductImageViewModel()
                {
                    Caption = i.Caption,
                    DateCreated = i.DateCreated,
                    FileSize = i.FileSize,
                    Id = i.Id,
                    ImagePath = i.ImagePath,
                    IsDefault = i.IsDefault,
                    ProductId = i.ProductId,
                    SortOrder = i.SortOrder
                }).ToListAsync();
        }

        public async Task<int> AddImage(int productId, ProductImageCreateRequest request)
        {
            var productImage = new ProductImage()
            {
                Caption = request.Caption,
                DateCreated = DateTime.Now,
                IsDefault = request.IsDefault,
                ProductId = productId,
                SortOrder = request.SortOrder
            };

            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }
            _context.ProductImages.Add(productImage);
            await _context.SaveChangesAsync();
            return productImage.Id;
        }

        public async Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null) throw new EcommerceException($"Cannot find an image with id {imageId}");

            productImage.Caption = request.Caption;
            productImage.DateCreated = DateTime.Now;
            productImage.IsDefault = request.IsDefault;
            productImage.ProductId = request.productId;
            productImage.SortOrder = request.SortOrder;


            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }
            _context.ProductImages.Update(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveImage(int imageId)
        {
            var ProductImage = await _context.ProductImages.FindAsync(imageId);
            if (ProductImage == null)
                throw new EcommerceException($"Cannot find an image with id {imageId}");
            _context.ProductImages.Remove(ProductImage);
            return await _context.SaveChangesAsync();
        }
		public async Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request)
		{
			//1. Select join
			var query = from p in _context.Products
							//join pt in _context.ProductTranslations on p.Id equals pt.ProductId
						join pic in _context.ProductInCategories on p.Id equals pic.ProductId
						join c in _context.Categories on pic.CategoryId equals c.Id
						//select new { p, pt, pic };
						select new { p, pic };
			//2. filter
			if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
			{
				query = query.Where(p => p.pic.CategoryId == request.CategoryId);
			}
			//3. Paging
			int totalRow = await query.CountAsync();

			var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
				.Take(request.PageSize)
				.Select(x => new ProductViewModel()
				{
					Id = x.p.Id,
					Name = x.p.Name,
					DateCreated = x.p.DateCreated,
					Description = x.p.Description,
					Details = x.p.Details,
					//LanguageId = x.p.LanguageId,
					OriginalPrice = x.p.OriginalPrice,
					Price = x.p.Price,
					SeoAlias = x.p.SeoAlias,
					SeoDescription = x.p.SeoDescription,
					SeoTitle = x.p.SeoTitle,
					Stock = x.p.Stock,
					ViewCount = x.p.ViewCount
				}).ToListAsync();

			//4. Select and projection
			var pagedResult = new PagedResult<ProductViewModel>()
			{
				TotalRecord = totalRow,
				Items = data
			};
			return pagedResult;
		}


	}
}