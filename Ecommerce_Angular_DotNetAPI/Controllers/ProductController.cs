using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Ecommerce_Angular_DotNetAPI.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Angular_DotNetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
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
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
        {
            var specification = new ProductWithTypesAndBrandsSpecification();
            var products = await productRepo.ListAsync(specification);
            var productToReturnDto = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            return Ok(productToReturnDto);
        }
        [HttpGet("GetProductById/{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var specification = new ProductWithTypesAndBrandsSpecification(id);
            var product = await productRepo.GetEntityWithSpec(specification);
            ProductToReturnDto productToReturnDto =
                mapper.Map<Product, ProductToReturnDto>(product);
            return Ok(productToReturnDto);
        }
        [HttpGet("GetProductByName/{name}")]
        public async Task<ActionResult<Product>> GetProductByName(string name)
        {
            var specification = new ProductWithTypesAndBrandsSpecification(name);
            var product = await productRepo.GetEntityWithSpec(specification);
            return Ok(product);
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
