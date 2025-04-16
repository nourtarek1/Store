﻿using AutoMapper;
using Domian.Contracts;
using Domian.Models;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork , IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync()
        {
            //Get All Proudcts Throught ProductRepository
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync();

            // Mapping IEnumerable<product> To <IEnumerable<ProductResultDto>> : AutoMapper
            var result =  mapper.Map<IEnumerable<ProductResultDto>>(products);
            return result;
        }
        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var product=await unitOfWork.GetRepository<Product, int>().GetAsync(id);
            if (product is null) return null;
            var result = mapper.Map<ProductResultDto>(product);
            return result;


        }

        public async Task<IEnumerable<BrandResultDto>> GetAllBrandAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var result = mapper.Map<IEnumerable<BrandResultDto>>(brands);
            return result;
        }
        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var Typs = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var result = mapper.Map<IEnumerable<TypeResultDto>>(Typs);
            return result;
        }

    }
}
