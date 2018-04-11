using DutchVACCATISGenerator.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace DutchVACCATISGenerator.Test.Logic
{
    [TestClass]
    public class FileLogicTests
    {
        private string executablePath;
        private readonly IFileLogic fileLogic;

        public FileLogicTests()
        {
            executablePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\";
            fileLogic = new FileLogic();
        }

        [TestInitialize]
        public void Initialize()
        {
            //Arrange
            Directory.CreateDirectory($@"{executablePath}temp");
            using (var stream = File.Create($@"{executablePath}temp\test.txt"))
            {
                stream.Close();
            }

            ZipFile.CreateFromDirectory($@"{executablePath}temp", $@"{executablePath}ziptest.zip");
        }

        [TestMethod]
        public void DeleteInstallerFiles_DeletesFiles_AreDeleted()
        {
            //Act
            fileLogic.DeleteInstallerFiles(true);

            //Assert
            Assert.IsFalse(File.Exists($@"{executablePath}ziptest.zip"));
            Assert.IsFalse(Directory.Exists($@"{executablePath}temp"));
        }
    }
}
