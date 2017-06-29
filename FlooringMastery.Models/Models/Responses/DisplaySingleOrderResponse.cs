namespace FlooringMastery.Models.Models.Responses
{
    public class DisplaySingleOrderResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Order OrderDetails { get; set; }
    }
}