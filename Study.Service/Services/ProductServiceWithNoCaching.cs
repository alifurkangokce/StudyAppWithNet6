using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Study.Core;
using Study.Core.DTOs;
using Study.Core.Repositories;
using Study.Core.Services;
using Study.Core.UnitOfWorks;

namespace Study.Service.Services
{
    public class ProductServiceWithNoCaching:Service<Product>,IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public ProductServiceWithNoCaching(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository repository1) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _repository = repository1;
            
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var products = await _repository.GetProductsWithCategory();
            var productsDto = _mapper.Map<List<ProductWithCategoryDto>>(products);
            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsDto);
        }
        
    }
}
