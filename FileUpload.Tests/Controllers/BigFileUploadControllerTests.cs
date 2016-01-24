using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileUpload.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Moq;

namespace FileUpload.Controllers.Tests
{
    [TestClass()]
    public class BigFileUploadControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            //Arrange
            BigFileUploadController bigFileUploadController = new BigFileUploadController();
            //Act
            ActionResult result = bigFileUploadController.Index() as ActionResult;
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void IsMD5ExistTest()
        {
            #region
            ////Arrange
            //var bigFileUploadController = new Mock<BigFileUploadController>();
            //bigFileUploadController.Setup(a=>a.IsMD5Exist())
            ////Act
            //ActionResult result = bigFileUploadController.IsMD5Exist("ef13915dda1f0dd3e2c5576ca2c7dce2", "fileName", "FileID") as ActionResult;

            ////Assert

            #endregion




            //Arrange
            BigFileUploadController bigFileUploadController = new BigFileUploadController();
            //Act
            ActionResult result = bigFileUploadController.IsMD5Exist("ef13915dda1f0dd3e2c5576ca2c7dce2", "fileName", "FileID") as ActionResult;

            //Assert  失败： System.Web.Mvc.HttpNotFoundResult 成功： System.Web.Mvc.JsonResult
            string a = result.ToString();
            Assert.AreEqual(a, "System.Web.Mvc.JsonResult");
        }
    }
}