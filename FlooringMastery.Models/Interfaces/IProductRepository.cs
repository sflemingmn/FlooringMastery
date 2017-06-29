namespace FlooringMastery.Models.Interfaces
{
    public interface IProductRepository
    {
        Product GetProductByProductType(string productType);
    }
}