﻿using System;

namespace FlooringMastery.Models
{
    public class Order
    {
        public DateTime OrderDate { get; set; }
        public int OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public string State { get; set; }
        public decimal TaxRate { get; set; }
        public string ProductType { get; set; }
        public decimal Area { get; set; }
        public decimal CostPerSquareFoot { get; set; }
        public decimal LaborCostPerSquareFoot { get; set; }

        public decimal MaterialCost
        {
            get
            {
                return (Area * CostPerSquareFoot);
            }
        }

        public decimal LaborCost
        {
            get
            {
                return (Area * LaborCostPerSquareFoot);
            }
        }

        public decimal Tax
        {
            get
            {
                return ((MaterialCost + LaborCost) * (TaxRate / 100));
            }
        }

        public decimal Total
        {
            get
            {
                return (MaterialCost + LaborCost + Tax);
            }
        }
    }
}