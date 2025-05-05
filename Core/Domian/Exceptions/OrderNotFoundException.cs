using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domian.Exceptions
{
    public class OrderNotFoundException(Guid id)
        :NotFoundException($" Order with id {id} Not Found !!")
    {
    }
}
