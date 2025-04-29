using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domian.Exceptions
{
    public class ProudctNotFoundExceptions(int id) :
        NotFoundException($"Product with id {id} Not Found !!")
    {
    }
}
