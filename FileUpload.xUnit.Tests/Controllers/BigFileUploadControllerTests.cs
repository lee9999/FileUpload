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
    public class BigFileUploadControllerTests
    {
        [Fact()]
        public void IndexTest()
        {
            BigFileUploadController bigFileUploadController = new BigFileUploadController();
            ViewResult result = bigFileUploadController.Index() as ViewResult;
            Assert.NotNull(result);
        }
    }
}