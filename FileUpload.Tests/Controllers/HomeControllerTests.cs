using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileUpload.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FileUpload.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            //Arrange
            HomeController homeController = new HomeController();
            //Act
            ViewResult result = homeController.Index() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }
    }
}