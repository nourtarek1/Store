﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domian.Exceptions
{
    public class DeliveryMethodNotFoundException(int id) :
        NotFoundException($"Delivery Method with id {id} Not Found !!")
    {
    }
}
