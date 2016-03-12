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
    public class FileUploadControllerTests
    {
        [Fact()]
        public void IndexTest()
        {
            FileUploadController fileUploadController = new FileUploadController();
            ViewResult result = fileUploadController.Index() as ViewResult;
            Assert.NotNull(result);
        }
    }
}