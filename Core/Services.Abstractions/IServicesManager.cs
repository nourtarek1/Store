﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IServicesManager
    {
        public IProductService ProductService { get;  }
    }
}
