using Domian.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductWithCountSpecifications :BaseSpecifications<Product,int>
    {
        public ProductWithCountSpecifications(ProductSpecificationsParamter productSpecificationsParamter) 
            :base(
                  P =>
                    (string.IsNullOrEmpty(productSpecificationsParamter.search) || P.Name.ToLower().Contains(productSpecificationsParamter.search.ToLower())) &&
                    (!productSpecificationsParamter.brandId.HasValue || P.BrandId == productSpecificationsParamter.brandId) &&
                    (!productSpecificationsParamter.typeId.HasValue || P.TypeId == productSpecificationsParamter.typeId)
                  )
        {
            
        }
    }
}
