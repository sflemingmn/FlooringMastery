using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;

namespace FlooringMastery.BLL.Managers
{
    public class TaxManager
    {
        private ITaxRepository _taxRepository;

        public TaxManager(ITaxRepository taxRepository)
        {
            _taxRepository = taxRepository;
        }

        public Tax TaxByState(string taxState)
        {
            return _taxRepository.GetTaxByState(taxState);
        }
    }
}