using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FlooringMastery.Data.Repositories
{
    public class TaxTestRepository : ITaxRepository
    {
        private List<Tax> _stateTax = new List<Tax>();

        public TaxTestRepository()
        {
            using (StreamReader sr = new StreamReader(Settings.TaxFilePath))
            {
                string line;
                sr.ReadLine();

                while ((line = sr.ReadLine()) != null)
                {
                    Tax taxes = new Tax();
                    string[] columns = line.Split(',');

                    taxes.StateAbbreviation = columns[0];
                    taxes.StateName = columns[1];
                    taxes.TaxRate = decimal.Parse(columns[2]);

                    _stateTax.Add(taxes);
                }
            }
        }

        public Tax GetTaxByState(string taxState)
        {
            return _stateTax.SingleOrDefault(p => p.StateAbbreviation == taxState);
        }
    }
}