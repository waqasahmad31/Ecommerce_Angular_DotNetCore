using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Ecommerce_Angular_DotNetAPI.Dtos;
using Ecommerce_Angular_DotNetAPI.Errors;
using Ecommerce_Angular_DotNetAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ecommerce_Angular_DotNetAPI.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> productRepo;
        private readonly IGenericRepository<ProductBrand> productBrandRepo;
        private readonly IGenericRepository<ProductType> productTypeRepo;
        private readonly IMapper mapper;

        public ProductController(IGenericRepository<Product> productRepo,
            IGenericRepository<ProductBrand> productBrandRepo,
            IGenericRepository<ProductType> productTypeRepo,
            IMapper mapper)
        {
            this.productRepo = productRepo;
            this.productBrandRepo = productBrandRepo;
            this.productTypeRepo = productTypeRepo;
            this.mapper = mapper;
        }

        // *** Product Code here *** //
        #region
        [HttpGet("GetProducts")]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
            [FromQuery]ProductSpecificationParams productPramas)
        {
            var specification = new ProductWithTypesAndBrandsSpecification(productPramas);

            var countSpec = new ProductWithFilterForCountSpecification(productPramas);

            var totalItems = await productRepo.CountAsynx(countSpec);

            var products = await productRepo.ListAsync(specification);

            var data = mapper.
                Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>(productPramas.PageIndex,
                productPramas.PageSize, totalItems, data));
        }

        [HttpGet("GetProductById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var specification = new ProductWithTypesAndBrandsSpecification(id);

            var product = await productRepo.GetEntityWithSpec(specification);

            if (product == null) return NotFound(new ApiResponse(404));

            return Ok(mapper.Map<Product, ProductToReturnDto>(product));
        }

        #endregion

        // *** Brands Code Here *** //
        #region

        [HttpGet("GetBrands")]
        public async Task<ActionResult<List<ProductBrand>>> GetBrands()
        {
            return Ok(await productBrandRepo.GetAllAsync());
        }
        [HttpGet("GetProductBrandById/{id}")]
        public async Task<ActionResult<ProductBrand>> GetProducBrandtById(int id)
        {
            return Ok(await productBrandRepo.GetByIdAsync(id));
        }
        #endregion

        // *** Product Type Code Here *** //
        #region        

        [HttpGet("GetProducTypes")]
        public async Task<ActionResult<List<ProductType>>> GetProductypes()
        {
            return Ok(await productTypeRepo.GetAllAsync());
        }
        [HttpGet("GetProductTypeById/{id}")]
        public async Task<ActionResult<ProductType>> GetProductypeById(int id)
        {
            return Ok(await productTypeRepo.GetByIdAsync(id));
        }
        #endregion
    }
}
