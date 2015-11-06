using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;

namespace FlooringProgram.Data.TaxRepositories
{
    public class TestTaxRepository : ITaxRepository
    {

        private const string _filePath = @"DataFilesTest\Taxes.txt";
        private List<StateTax> _stateTaxList = new List<StateTax>();

        public List<StateTax> GetStateTaxInformation()
        {

            if (File.Exists(_filePath))
            {

                var reader = File.ReadAllLines(_filePath);

                for (int i = 1; i < reader.Length; i++)
                {
                    var columns = reader[i].Split(',');

                    var state = new StateTax();

                    state.StateAbbreviation = columns[0];
                    state.StateName = columns[1];
                    state.TaxRate = Decimal.Parse(columns[2]);

                    _stateTaxList.Add(state);

                }
            }
            return _stateTaxList;
        }
    }
}