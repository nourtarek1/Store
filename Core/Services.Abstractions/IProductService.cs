﻿using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IProductService
    {
        //Task<IEnumerable<ProductResultDto>> GetAllProductsAsync(ProductSpecificationsParamter productSpecificationsParamter);
        Task<PaginationResponse<ProductResultDto>> GetAllProductsAsync(ProductSpecificationsParamter productSpecificationsParamter);

        Task<ProductResultDto?> GetProductByIdAsync(int id);

        Task<IEnumerable<BrandResultDto>> GetAllBrandAsync();

        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();



    }
}
