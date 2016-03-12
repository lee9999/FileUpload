using Xunit;
using FileUpload.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FileUpload.Controllers.xUnit.Tests
{
    public class HomeControllerTests
    {
        [Fact(DisplayName = "对HomeController的Index的测试")]
        public void IndexTest()
        {
            HomeController homeController = new HomeController();
            ViewResult result=homeController.Index() as ViewResult;
            Assert.NotNull(result);
        }
    }
}