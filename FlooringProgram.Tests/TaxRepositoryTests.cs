using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Data.TaxRepositories;
using NUnit.Framework;

namespace FlooringProgram.Tests
{
    [TestFixture]
    public class TaxRepositoryTests 
    {
        [Test]
        public void GetTaxInfoFromRepoTest()
        {
            var taxRepo = new TestTaxRepository();
            var stateTaxList = taxRepo.GetStateTaxInformation();
            var numStates = stateTaxList.Count;

            var state = stateTaxList.FirstOrDefault(s => s.StateAbbreviation == "CA");

            //Validating that all information is accurate
            Assert.AreEqual(state.StateName, "California");
            Assert.AreEqual(state.TaxRate, 7.25);

            //Validating total number of states is returned
            Assert.AreEqual(numStates, 3);
        }
    }
}
