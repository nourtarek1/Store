using AutoMapper;
using Domian.Contracts;
using Domian.Exceptions;
using Domian.Models;
using Services.Abstractions;
using Services.Specifications;
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
        public async Task<PaginationResponse<ProductResultDto>> GetAllProductsAsync(ProductSpecificationsParamter productSpecificationsParamter)
        {
            var spec = new ProductsAndBrandsAndTypesSpecifications(productSpecificationsParamter);

            //Get All Proudcts Throught ProductRepository
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);

            var specCount = new ProductWithCountSpecifications(productSpecificationsParamter);
            var count = await unitOfWork.GetRepository<Product,int>().CountAsync(specCount);
            // Mapping IEnumerable<product> To <IEnumerable<ProductResultDto>> : AutoMapper
            var result =  mapper.Map<IEnumerable<ProductResultDto>>(products);

            return new PaginationResponse<ProductResultDto>(productSpecificationsParamter.pageIndex, productSpecificationsParamter.pageSize,count,result);
        }
        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var spec = new ProductsAndBrandsAndTypesSpecifications(id);

            var product =await unitOfWork.GetRepository<Product, int>().GetAsync(spec);
            if (product is null) throw new ProudctNotFoundExceptions(id);
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
