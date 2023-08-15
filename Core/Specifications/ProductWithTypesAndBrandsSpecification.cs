using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductWithTypesAndBrandsSpecification()
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
        public ProductWithTypesAndBrandsSpecification(int id):base(x=>x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
        public ProductWithTypesAndBrandsSpecification(ProductSpecificationParams productPramas)
            : base(x =>
                (string.IsNullOrEmpty(productPramas.Search) || x.Name.ToLower().Contains
                    (productPramas.Search)) &&
                (!productPramas.BrandId.HasValue || x.ProductBrandId == productPramas.BrandId) &&
                (!productPramas.TypeId.HasValue || x.ProductTypeId == productPramas.TypeId)
            )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(productPramas.PageSize * (productPramas.PageIndex - 1), 
                productPramas.PageSize);

            if (!string.IsNullOrEmpty(productPramas.Sort))
            {
                switch(productPramas.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(x => x.Price);
                        break;
                    case "nameAsc":
                        AddOrderBy(x => x.Name);
                        break;
                    case "nameDesc":
                        AddOrderByDescending(x => x.Name);
                        break;
                    default:
                        
                        break;

                }
            }
        }
    }
}
