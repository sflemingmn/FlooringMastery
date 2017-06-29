namespace FlooringMastery.Models.Interfaces
{
    public interface ITaxRepository
    {
        Tax GetTaxByState(string taxType);
    }
}