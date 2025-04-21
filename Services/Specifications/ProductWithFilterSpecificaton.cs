using Domain.Contracts;
using Domain.Entities;
using Shared.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductWithFilterSpecificaton : Specification<Product>
    {
        public ProductWithFilterSpecificaton(int id) : base(product => product.Id == id)
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);
        }

        public ProductWithFilterSpecificaton(ProductSpecificationParams specs)
            : base(product => (!specs.BrandId.HasValue || product.BrandId == specs.BrandId) &&
                              (!specs.TypeId.HasValue || product.TypeId == specs.TypeId) &&
                              (string.IsNullOrEmpty(specs.Search) || product.Name.ToLower().Contains(specs.Search.ToLower().Trim())))
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);
            ApplyPagination(specs.PageSize , specs.PageSize);

            if (specs.Sort != null) 
            {
                switch (specs.Sort)
                {
                    case SortingOptions.NameAsc:
                        SetOrderBy(p => p.Name);
                        break;
                    case SortingOptions.NameDesc:
                        SetOrderByDescending(p => p.Name);
                        break;
                    case SortingOptions.PriceAsc:
                        SetOrderBy(p => p.Price);
                        break;
                    case SortingOptions.PriceDesc:
                        SetOrderByDescending(p => p.Price);
                        break;
                    default:
                        SetOrderBy(p => p.Name);
                        break;
                }
            }
        }
    }



}
