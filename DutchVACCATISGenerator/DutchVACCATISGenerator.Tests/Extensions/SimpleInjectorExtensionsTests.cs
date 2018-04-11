using DutchVACCATISGenerator.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;

namespace DutchVACCATISGenerator.Test.Extensions
{
    [TestClass]
    public class SimpleInjectorExtensionsTests
    {
        [TestMethod]
        public void Bootstrap_BootstrapApplication_NoErrors()
        {
            //Arrange
            var container = new Container();

            //Act
            container.Bootstrap();

            //Assert
            Assert.IsFalse(container.IsVerifying);
        }
    }
}
