using FlooringMastery.BLL.Managers;
using FlooringMastery.Data.Repositories;
using System;
using System.Configuration;

namespace FlooringMastery.BLL.Factories
{
    public static class ProductManagerFactory
    {
        public static ProductManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "TestData":
                    return new ProductManager(new ProductTestRepository());

                case "ProdData":
                    return new ProductManager(new ProductRepository());

                default:
                    throw new Exception("Invalid App.config Mode Value");
            }
        }
    }
}