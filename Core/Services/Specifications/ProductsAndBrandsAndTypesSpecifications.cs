using Domian.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductsAndBrandsAndTypesSpecifications :BaseSpecifications<Product,int>
    {
        public ProductsAndBrandsAndTypesSpecifications(int id) :base(P =>P.Id == id)
        {
            ApllyIncludes();

        }
        public ProductsAndBrandsAndTypesSpecifications(ProductSpecificationsParamter productSpecificationsParamter) 
            : base(
                  P =>
                    (string.IsNullOrEmpty(productSpecificationsParamter.search) || P.Name.ToLower().Contains(productSpecificationsParamter.search.ToLower())) &&
                    (!productSpecificationsParamter.brandId.HasValue || P.BrandId==productSpecificationsParamter.brandId) &&
                    (!productSpecificationsParamter.typeId.HasValue || P.TypeId == productSpecificationsParamter.typeId)
                  )
        {
            ApllyIncludes();
            ApplySorting(productSpecificationsParamter.sort);
            ApplyPagination(productSpecificationsParamter.pageIndex, productSpecificationsParamter. pageSize);
        }
        private void ApllyIncludes()
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }

        private void ApplySorting(string ? sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "namedesc":
                        AddOrderByDescending(P => P.Name);
                        break;
                            case "priceesc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;


                }
            }
            else
            {
                AddOrderBy(P => P.Name);

            }
        }
    }
}
