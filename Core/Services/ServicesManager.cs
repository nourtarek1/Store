﻿using AutoMapper;
using Domian.Contracts;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServicesManager(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IBasketRepository basketRepository
        ) : IServicesManager
    {
        public IProductService ProductService { get; } = new ProductService(unitOfWork, mapper);

        public IBasketService BasketService { get; } = new BasketService(basketRepository, mapper);
    }
}
