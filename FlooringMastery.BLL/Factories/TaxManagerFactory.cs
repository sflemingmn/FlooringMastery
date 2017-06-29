using FlooringMastery.BLL.Managers;
using FlooringMastery.Data.Repositories;
using System;
using System.Configuration;

namespace FlooringMastery.BLL.Factories
{
    public static class TaxManagerFactory
    {
        public static TaxManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "TestData":
                    return new TaxManager(new TaxTestRepository());

                case "ProdData":
                    return new TaxManager(new TaxRepository());

                default:
                    throw new Exception("Invalid App.config Mode Value");
            }
        }
    }
}