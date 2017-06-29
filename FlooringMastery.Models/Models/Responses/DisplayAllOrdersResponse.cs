using System.Collections.Generic;

namespace FlooringMastery.Models.Models.Responses
{
    public class DisplayAllOrdersResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<Order> OrderDetails { get; set; }
    }
}