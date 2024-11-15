using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mynance.src.exceptions
{
    public class AuthException : BaseException
    {
        public AuthException(string message) : base(message)
        {
        }
    }
}
