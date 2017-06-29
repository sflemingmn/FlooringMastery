using FlooringMastery.BLL.Managers;
using FlooringMastery.Data;
using FlooringMastery.Data.Repositories;
using System;
using System.Configuration;

namespace FlooringMastery.BLL.Factories
{
    public static class OrderManagerFactory
    {
        public static OrderManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "TestData":
                    string testPath = ConfigurationManager.AppSettings["TestPath"].ToString();
                    return new OrderManager(new OrderTestRepository(testPath));

                case "ProdData":
                    string path = ConfigurationManager.AppSettings["Path"].ToString();
                    return new OrderManager(new OrderRepository(path));

                default:
                    throw new Exception("Invalid App.config Mode Value");
            }
        }
    }
}