﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domian.Exceptions
{
    public class BasketDeleteBadRequest()
        : BadRequestException($" Invalid Operation When Delete Basket ")
    {
    }
}
