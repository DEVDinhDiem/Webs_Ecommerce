using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Data.Enums;

namespace Ecommerce.Data.Entities
{
    public class Category
    {
        public int Id { set; get; }
        //new column
        public string Name { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }

        public int SortOrder { set; get; }
        public bool IsShowOnHome { set; get; }
        public int? ParentId { set; get; }
        public Status Status { set; get; }

        public string SeoAlias { set; get; }
        public List<ProductInCategory> ProductInCategories { get; set; }

        //public List<CategoryTranslation> CategoryTranslations { get; set; }

    }
}
