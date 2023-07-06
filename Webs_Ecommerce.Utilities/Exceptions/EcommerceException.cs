using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Utilities.Exceptions
{
    public class EcommerceException : Exception
    {
        public EcommerceException()
        {
        }

        public EcommerceException(string message)
            : base(message)
        {
        }

        public EcommerceException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

