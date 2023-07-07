﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.ViewModels.Catalog.Products.Common
{
    public class PagingRequestBase
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalRecords { get; set; }

        public int PageCount
        {
            get
            {
                var pageCount = (double)TotalRecords / PageSize;
                return (int)Math.Ceiling(pageCount);
            }
        }
    }
}