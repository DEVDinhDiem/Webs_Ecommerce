using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Data.Entities;
using Ecommerce.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>().HasData(
               new AppConfig() { Key = "HomeTitle", Value = "This is home page of Ecommerce" },
               new AppConfig() { Key = "HomeKeyword", Value = "This is keyword of Ecommerce" },
               new AppConfig() { Key = "HomeDescription", Value = "This is description of Ecommerce" }
               );
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    Name = "Áo nam",
                    SeoAlias = "ao-nam",
                    SeoDescription = "Sản phẩm áo thời trang nam",
                    SeoTitle = "Sản phẩm áo thời trang nam",
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 1,
                    Status = Status.Active
                    
                },
                 new Category()
                 {
                     Id = 2,
                     Name = "Men Shirt",
                     SeoAlias = "men-shirt",
                     SeoDescription = "The shirt products for men",
                     SeoTitle = "The shirt products for men",
                     IsShowOnHome = true,
                     ParentId = null,
                     SortOrder = 2,
                     Status = Status.Active
                     
                 });

            modelBuilder.Entity<Product>().HasData(
           new Product()
           {
               Id = 1,
               Name = "Áo sơ mi nam trắng Việt Tiến",
               SeoAlias = "ao-so-mi-nam-trang-viet-tien",
               SeoDescription = "Áo sơ mi nam trắng Việt Tiến",
               SeoTitle = "Áo sơ mi nam trắng Việt Tiến",
               Details = "Áo sơ mi nam trắng Việt Tiến",
               Description = "Áo sơ mi nam trắng Việt Tiến",
               DateCreated = DateTime.Now,
               OriginalPrice = 100000,
               Price = 200000,
               Stock = 0,
               ViewCount = 0
               
           });
            modelBuilder.Entity<ProductInCategory>().HasData(
                new ProductInCategory() { ProductId = 1, CategoryId = 1 }

                );
            // any guid
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "dinhdiemdlx@gmail.com",
                NormalizedEmail = "dinhdiemdlx@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Abcd1234$"),
                SecurityStamp = string.Empty,
                FirstName = "Dinh",
                LastName = "Diem",
                Dob = new DateTime(2020, 01, 31)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }
    }
}
