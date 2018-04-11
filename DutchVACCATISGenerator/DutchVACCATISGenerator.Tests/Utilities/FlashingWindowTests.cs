using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

namespace DutchVACCATISGenerator.Test.Utilities
{
    [TestClass]
    public class FlashingWindowTests
    {
        [TestMethod]
        public void FlashWindowEx_FormIsFlashing_IsTrue()
        {
            //Arrange
            var form = new Form();

            //Act
            var result = FlashingWindow.FlashWindowEx(form);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
