using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;

namespace FlooringMastery.BLL.Managers
{
    public class ProductManager
    {
        private IProductRepository _productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product ProductType(string productType)
        {
            return _productRepository.GetProductByProductType(productType);
        }
    }
}