using AutoMapper;
using Domian.Contracts;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServicesManager(IUnitOfWork unitOfWork , IMapper mapper) : IServicesManager
    {
        public IProductService ProductService { get; } = new ProductService(unitOfWork, mapper);
    }
}
