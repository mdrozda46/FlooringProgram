using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Data.OrderRepositories;
using FlooringProgram.Models;

namespace FlooringProgram.Data.TaxRepositories
{
    public class TaxRepositoryFactory
    {
        public static ITaxRepository CreateTaxRepository()
        {
            var mode = ConfigurationManager.AppSettings["mode"].ToString().ToUpper();

            switch (mode)
            {
                case "TEST":
                    return new TestTaxRepository();
                    
                default:
                    return new ProdTaxRepository();
                    
            }
        }
    }
}