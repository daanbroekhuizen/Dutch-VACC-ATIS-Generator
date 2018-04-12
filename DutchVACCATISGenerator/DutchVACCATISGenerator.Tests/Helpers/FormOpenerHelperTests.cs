using DutchVACCATISGenerator.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;
using System;
using System.Windows.Forms;

namespace DutchVACCATISGenerator.Test.Helpers
{
    [TestClass]
    public class FormOpenerHelperTests
    {
        private IFormOpenerHelper formOpenerHelper;

        [TestInitialize]
        public void Initialize()
        {
            formOpenerHelper = new FormOpenerHelper(new Container());
        }

        [TestMethod]
        public void CloseForm_OpenendForm_ClosesForm()
        {
            //Arrange
            formOpenerHelper.ShowModelessForm<Form>();

            //Act
            formOpenerHelper.CloseForm<Form>();

            //Assert
            Assert.IsFalse(formOpenerHelper.IsOpen<Form>());
        }

        [TestMethod]
        public void ShowModelessForm_OpenendForm_IsOpen()
        {
            //Arrange
            formOpenerHelper.ShowModelessForm<Form>();

            //Act
            formOpenerHelper.ShowModelessForm<Form>();

            //Assert
            Assert.IsTrue(formOpenerHelper.IsOpen<Form>());
        }

        [TestMethod]
        public void CloseForm_ClosedForm_DoesNothing()
        {
            //Arrange
            formOpenerHelper.ShowModelessForm<Form>();
            formOpenerHelper.CloseForm<Form>();

            //Act
            formOpenerHelper.CloseForm<Form>();

            //Assert
            Assert.IsFalse(formOpenerHelper.IsOpen<Form>());
        }

        [TestMethod]
        public void Show_OpenedForm_IsOpen()
        {
            //Arrange
            formOpenerHelper.ShowModelessForm<Form>();

            //Act
            formOpenerHelper.Show<Form>();

            //Assert
            Assert.IsTrue(formOpenerHelper.IsOpen<Form>());
        }

        [TestMethod]
        public void Show_ClosedForm_IsClosed()
        {
            //Act
            formOpenerHelper.Show<Form>();

            //Assert
            Assert.IsFalse(formOpenerHelper.IsOpen<Form>());
        }

        [TestMethod]
        public void Hide_OpenedForm_IsHidden()
        {
            //Arrange
            formOpenerHelper.ShowModelessForm<Form>();

            //Act
            formOpenerHelper.Hide<Form>();

            //Assert
            Assert.IsFalse(formOpenerHelper.GetForm<Form>().Visible);
        }

        [TestMethod]
        public void Hide_ClosedForm_IsHidden()
        {
            //Act
            formOpenerHelper.Hide<Form>();

            //Assert
            Assert.IsFalse(formOpenerHelper.IsOpen<Form>());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetForm_NonOpenedForm_ThrowsException()
        {
            //Act
            formOpenerHelper.GetForm<Form>();
        }
    }
}
