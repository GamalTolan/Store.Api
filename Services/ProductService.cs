using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstractions;
using Services.Specifications;
using Shared;
using Shared.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork,IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var mappedBrands = mapper.Map<IEnumerable<BrandResultDto>>(brands);
            return mappedBrands;
        }

        public async Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationParams specifications)
        {
            var specs = new ProductWithFilterSpecificaton(specifications);
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(specs);
            var countSpec = new ProductWithFilterSpecificaton(specifications);
            var totalCount = await unitOfWork.GetRepository<Product, int>().CountAsync(countSpec);
            var mappedProducts = mapper.Map<IEnumerable<ProductResultDto>>(products);
            return new PaginatedResult<ProductResultDto>
                (specifications.PageIndex,specifications.PageSize,totalCount,mappedProducts);
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var mappedTypes = mapper.Map<IEnumerable<TypeResultDto>>(types);
            return mappedTypes;

        }

        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithFilterSpecificaton(id);
            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(spec);
            var mappedProduct = mapper.Map<ProductResultDto>(product);
            return mappedProduct;

        }
    }
}
