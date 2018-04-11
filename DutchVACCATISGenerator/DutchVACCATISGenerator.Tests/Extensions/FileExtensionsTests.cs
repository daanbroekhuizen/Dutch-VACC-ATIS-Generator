using DutchVACCATISGenerator.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            var stream = File.Create($@"{executablePath}test.txt");

            //Act
            var result = new FileInfo($@"{executablePath}test.txt").IsLocked();
            stream.Close();

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
