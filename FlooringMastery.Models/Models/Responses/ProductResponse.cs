namespace FlooringMastery.Models.Models.Responses
{
    public class ProductResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Product ProductDetails { get; set; }
    }
}