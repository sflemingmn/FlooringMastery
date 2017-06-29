namespace FlooringMastery.Models.Models.Responses
{
    public class TaxResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Tax TaxDetails { get; set; }
    }
}