using DutchVACCATISGenerator.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchVACCATISGenerator.Test.Logic
{
    [TestClass]
    public class AutoUpdateTests
    {
        private readonly IAutoUpdateLogic autoUpdateLogic;

        public AutoUpdateTests()
        {
            autoUpdateLogic = new AutoUpdateLogic();
        }

        [TestMethod]
        public void AutoUpdateTest()
        {
            //Act
            autoUpdateLogic.AutoUpdate().Wait();
        }
    }
}
