using DutchVACCATISGenerator.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;

namespace DutchVACCATISGenerator.Test.Extensions
{
    [TestClass]
    public class FileExtensionsTests
    {
        private string executablePath;

        public FileExtensionsTests()
        {
            executablePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\";
        }

        [TestMethod]
        public void IsLocked_FileNotLocked_IsFalse()
        {
            //Arrange
            using (var stream = File.Create($@"{executablePath}test.txt"))
            {
                stream.Close();
            }

            //Act
            var result = new FileInfo($@"{executablePath}test.txt").IsLocked();

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsLocked_FileLockedThrowsException_IsTrue()
        {
            //Arrange
            bool result = false;

            //Act
            using (var stream = File.Create($@"{executablePath}test.txt"))
            {
                result = new FileInfo($@"{executablePath}test.txt").IsLocked();
            }

            //Assert
            Assert.IsTrue(result);
        }

        [TestCleanup]
        public void Cleanup()
        {
            File.Delete($@"{executablePath}test.txt");
        }
    }
}
