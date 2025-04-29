using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domian.Exceptions
{
    public abstract class BadRequestException(string message) : Exception(message)
    {
    }
}
