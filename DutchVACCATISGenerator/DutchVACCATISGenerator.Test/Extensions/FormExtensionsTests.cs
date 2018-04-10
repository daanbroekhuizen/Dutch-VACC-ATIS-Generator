using DutchVACCATISGenerator.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

namespace DutchVACCATISGenerator.Test.Extensions
{
    [TestClass]
    public class FormExtensionsTests
    {
        [TestMethod]
        public void SetRelativeBottom_SetsPosition_CorrectPosition()
        {
            //Arrange
            var form1 = new Form();
            var form2 = new Form();

            form1.Height = 100;
            form1.Left = 50;
            form1.Top = 50;

            //Act
            form2.SetRelativeBottom(form1.Bounds);

            //Assert
            Assert.AreEqual(form2.Left, form1.Left);
            Assert.AreEqual(form2.Top, form1.Bottom);
        }

        [TestMethod]
        public void SetRelativeRight_SetsPosition_CorrectPosition()
        {
            //Arrange
            var form1 = new Form();
            var form2 = new Form();

            form1.Height = 100;
            form1.Left = 50;
            form1.Top = 50;

            //Act
            form2.SetRelativeRight(form1.Bounds);

            //Assert
            Assert.AreEqual(form2.Left, form1.Right);
            Assert.AreEqual(form2.Top, form1.Top);
        }
    }
}
