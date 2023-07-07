using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.ViewModels.Catalog.Products.Common;

namespace Ecommerce.ViewModels.Catalog.Products.Manage
{
    public class GetProductPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
        public List<int> CategoryId { get; set; }

    }
}
