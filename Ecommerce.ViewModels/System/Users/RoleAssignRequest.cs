using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.ViewModels.Catalog.Common;

namespace Ecommerce.ViewModels.System.Users
{
	public class RoleAssignRequest
	{
		public Guid Id { get; set; }
		public List<SelectItem> Roles { get; set; } = new List<SelectItem>();
	}
}
