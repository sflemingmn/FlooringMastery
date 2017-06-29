using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FlooringMastery.Data.Repositories
{
    public class ProductTestRepository : IProductRepository
    {
        private List<Product> _productList = new List<Product>();

        public ProductTestRepository()
        {
            using (StreamReader sr = new StreamReader(Settings.ProductFilePath))
            {
                string line;
                sr.ReadLine();

                while ((line = sr.ReadLine()) != null)
                {
                    Product product = new Product();
                    string[] columns = line.Split(',');

                    product.ProductType = columns[0];
                    product.CostPerSquareFoot = decimal.Parse(columns[1]);
                    product.LaborCostPerSquareFoot = decimal.Parse(columns[2]);

                    _productList.Add(product);
                }
            }
        }

        public Product GetProductByProductType(string productType)
        {
            return _productList.SingleOrDefault(p => p.ProductType == productType);
        }
    }
}