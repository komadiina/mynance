using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mynance.src.exceptions.auth
{
    public class InvalidFullnameException : AuthException
    {
        public InvalidFullnameException(string message) : base(message) { }
    }
}
