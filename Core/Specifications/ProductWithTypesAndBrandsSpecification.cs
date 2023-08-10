﻿using Core.Entities;
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
        public ProductWithTypesAndBrandsSpecification(string name):base(x=>x.Name == name)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}
