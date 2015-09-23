using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DutchVACCATISGenerator.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RealRunwayBackgroundWorker()
        {
            DutchVACCATISGenerator form = new DutchVACCATISGenerator();

            PrivateObject pForm = new PrivateObject(form);

            pForm.Invoke("realRunwayBackgroundWorker_DoWork", new Object[] { null, null});


           
        }
    }
}
