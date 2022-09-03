using AutoMapper;
using Odevez.Business.Interfaces;
using Odevez.DTO;
using Odevez.Repository.Models;
using Odevez.Repository.Repositorys.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Business.Business
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductBusiness(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task CreatAsync(ProductDTO product)
        {
            await _productRepository.CreatAsync(_mapper.Map<ProductModel>(product));
        }

        public async Task DeleteAsync(int productId)
        {
            await _productRepository.DeleteAsync(productId);
        }

        public async Task<bool> ExistsByIdAsync(int productId)
        {
            return await _productRepository.ExistsByIdAsync(productId);
        }

        public async Task<ProductDTO> GetByIdAsync(int productId)
        {
            var retorno = await _productRepository.GetByIdAsync(productId);
            return _mapper.Map<ProductDTO>(retorno);
        }

        public async Task<List<ProductDTO>> ListByFilterAsync(int? productId = 0, string description = null)
        {
            var retorno = await _productRepository.ListByFilterAsync(productId, description);
            return _mapper.Map<List<ProductDTO>>(retorno);
        }

        public async Task UpdateAsync(ProductDTO product)
        {
            await _productRepository.UpdateAsync(_mapper.Map<ProductModel>(product));
        }
    }
}
