using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.BLL;
using NUnit.Framework;

namespace FlooringProgram.Tests
{
    [TestFixture]
    public class TaxOperationsTests
    {
        [Test]
        public void GetTaxInfoTest()
        {

            var ops = new TaxOperations();
            var taxList = ops.GetTaxInfo();
            var numStates = taxList.Count;

            var state = taxList.FirstOrDefault(s => s.StateAbbreviation == "CA");

            //Validating that all information is accurate
            Assert.AreEqual(state.StateName, "California");
            Assert.AreEqual(state.TaxRate, 7.25);

            //Validating total number of states is returned
            Assert.AreEqual(numStates, 3);
        }
    }
}

