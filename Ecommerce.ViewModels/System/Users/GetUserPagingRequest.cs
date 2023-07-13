using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.ViewModels.Catalog.Common;

namespace Ecommerce.ViewModels.System.Users
{
	public class GetUserPagingRequest : PagingRequestBase
	{
		public string Keyword { get; set; }
	}
}
