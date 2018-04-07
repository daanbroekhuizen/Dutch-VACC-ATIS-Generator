using DutchVACCATISGenerator.Test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DutchVACCATISGenerator.Test.Types
{
    [TestClass]
    public class METARTests
    {
        [TestMethod]
        public void MultipleMETARs()
        {
            foreach (var METAR in METARHelper.EHAMMETARs)
                new DutchVACCATISGenerator.Types.METAR(METAR);

            foreach (var METAR in METARHelper.EHEHMETARs)
                new DutchVACCATISGenerator.Types.METAR(METAR);

            foreach (var METAR in METARHelper.EHRDMETARs)
                new DutchVACCATISGenerator.Types.METAR(METAR);
        }

        [TestMethod]
        public void METAR()
        {
            //Act
            var METAR = new DutchVACCATISGenerator.Types.METAR("EHAM 161725Z 25015KT 9999 VCSH FEW030 05/00 Q0992 NOSIG");
        }
    }
}
