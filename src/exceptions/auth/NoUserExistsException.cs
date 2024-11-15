using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mynance.src.exceptions.auth
{
    public class NoUserExistsException : AuthException
    {
        public NoUserExistsException(string message) : base(message) { }
    }
}
