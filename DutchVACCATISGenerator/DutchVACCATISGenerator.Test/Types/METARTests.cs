using DutchVACCATISGenerator.Test.Helpers;
using DutchVACCATISGenerator.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DutchVACCATISGenerator.Test.Types
{
    [TestClass]
    public class METARTests
    {
        [TestMethod]
        public void NewMETAR_IndividualMETAR_InitializesNewMETAR()
        {
            //Act
            var METAR = new METAR(METARHelper.METAR);

            //Assert
            Assert.IsNotNull(METAR);
        }

        [TestMethod]
        public void NewMETAR_EindhovenMETARs_InitializesNewMETARs()
        {
            //Arrange
            var METARs = new List<METAR>();

            //Act
            foreach (var METAR in METARHelper.EHEHMETARs)
                METARs.Add(new METAR(METAR));

            //Assert
            Assert.AreEqual(METARs.Count, METARHelper.EHEHMETARs.Count);
        }

        [TestMethod]
        public void NewMETAR_RotterdamMETARs_InitializesNewMETARs()
        {
            //Arrange
            var METARs = new List<METAR>();

            //Act
            foreach (var METAR in METARHelper.EHRDMETARs)
                METARs.Add(new METAR(METAR));

            //Assert
            Assert.AreEqual(METARs.Count, METARHelper.EHRDMETARs.Count);
        }

        [TestMethod]
        public void NewMETAR_SchipholMETARs_InitializesNewMETARs()
        {
            //Arrange
            var METARs = new List<METAR>();

            //Act
            foreach (var METAR in METARHelper.EHAMMETARs)
                METARs.Add(new METAR(METAR));

            //Assert
            Assert.AreEqual(METARs.Count, METARHelper.EHAMMETARs.Count);
        }
    }
}
