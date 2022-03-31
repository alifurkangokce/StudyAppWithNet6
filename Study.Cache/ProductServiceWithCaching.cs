using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Study.Core;
using Study.Core.DTOs;
using Study.Core.Repositories;
using Study.Core.Services;
using Study.Core.UnitOfWorks;
using Study.Service.Exceptions;

namespace Study.Cache
{
    public class ProductServiceWithCaching:IProductService
    {
        private const string CacheProductKey = "productsCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductServiceWithCaching(IMapper mapper, IMemoryCache cache, IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _cache = cache;
            _repository = repository;
            _unitOfWork = unitOfWork;
            if (!_cache.TryGetValue(CacheProductKey,out _))
            {
                _cache.Set(CacheProductKey, _repository.GetProductsWithCategory().Result);
            }
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult(_cache.Get<IEnumerable<Product>>(CacheProductKey));

        }

        public  Task<Product> GetByIdAsync(int id)
        {
            var product = _cache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x => x.Id == id);
            if (product==null)
            {
                throw new NotFoundException($"{nameof(Product)} not found");
            }
            return  Task.FromResult(product);
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            return _cache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable();
        }

        public async Task<Product> AddAsync(Product entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            return entity;
        }

        public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            return entities;
        }

        public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Product entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        public async Task RemoveAsync(Product entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        public  async Task RemoveRangeAsync(IEnumerable<Product> entity)
        {
            _repository.RemoveRange(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        public Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var products = _cache.Get<IEnumerable<Product>>(CacheProductKey);
            var productsWithCategoryDto = _mapper.Map<List<ProductWithCategoryDto>>(products);
            return Task.FromResult(CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsWithCategoryDto));

        }

        public async Task CacheAllProductsAsync()
        {
            _cache.Set(CacheProductKey,await _repository.GetAll().ToListAsync());
        }
    }
}
