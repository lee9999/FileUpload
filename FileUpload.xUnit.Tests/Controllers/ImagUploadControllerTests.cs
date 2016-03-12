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
    public class ImagUploadControllerTests
    {
        [Fact()]
        public void IndexTest()
        {
            ImagUploadController imagUploadController = new ImagUploadController();
            ViewResult result = imagUploadController.Index() as ViewResult;
            Assert.NotNull(result);
        }
    }
}