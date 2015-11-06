using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Data.TaxRepositories;
using FlooringProgram.Models;

namespace FlooringProgram.BLL
{
    public class TaxOperations
    {
        private ITaxRepository _taxRepo;

        public List<StateTax> GetTaxInfo()
        {
            _taxRepo = TaxRepositoryFactory.CreateTaxRepository();
            return _taxRepo.GetStateTaxInformation();
        }


    }
}
