﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.ViewModels.System.Users
{
	public class UserVm
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string PhoneNumber { get; set; }

		public string UserName { get; set; }

		public string Email { get; set; }
	}
}
