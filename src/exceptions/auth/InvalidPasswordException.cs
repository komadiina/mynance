using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mynance.src.exceptions.auth
{
    public class InvalidPasswordException : AuthException
    {
        public InvalidPasswordException(string message) : base(message) { }
    }
}
