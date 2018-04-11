using DutchVACCATISGenerator.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace DutchVACCATISGenerator.Test.Extensions
{
    [TestClass]
    public class ZipArchiveExtensionsTests
    {
        private string executablePath;

        public ZipArchiveExtensionsTests()
        {
            executablePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\";
        }
       
        [TestInitialize]
        public void Initialize()
        {
            //Arrange
            Directory.CreateDirectory($@"{executablePath}ziptest");
            using (var stream = File.Create($@"{executablePath}ziptest\test.txt"))
            {
                stream.Close();
            }

            ZipFile.CreateFromDirectory($@"{executablePath}ziptest", $@"{executablePath}ziptest.zip");

            Directory.Delete($@"{executablePath}ziptest", true);
        }

        [TestMethod]
        public void ExtractToDirectory_ExtractsZIPWithoutOverwrite_Passes()
        {
            //Act
            using (var zipFile = ZipFile.Open($@"{executablePath}ziptest.zip", ZipArchiveMode.Read))
            {
                zipFile.ExtractToDirectory($"{executablePath}ziptest", false);
            }

            //Assert
            Assert.IsTrue(Directory.Exists($"{executablePath}ziptest"));
            Assert.IsTrue(File.Exists($@"{executablePath}ziptest.zip"));
            Assert.IsTrue(File.Exists($@"{executablePath}ziptest\test.txt"));
        }

        [TestMethod]
        public void ExtractToDirectory_ExtractsZIPWithOverwrite_Passes()
        {
            //Act
            using (var zipFile = ZipFile.Open($@"{executablePath}ziptest.zip", ZipArchiveMode.Read))
            {
                zipFile.ExtractToDirectory($"{executablePath}ziptest", true);
            }

            //Assert
            Assert.IsTrue(Directory.Exists($"{executablePath}ziptest"));
            Assert.IsTrue(File.Exists($@"{executablePath}ziptest.zip"));
            Assert.IsTrue(File.Exists($@"{executablePath}ziptest\test.txt"));
        }

        [TestCleanup]
        public void Cleanup()
        {
            Directory.Delete($@"{executablePath}ziptest", true);
            File.Delete($@"{executablePath}ziptest.zip");
        }
    }
}
