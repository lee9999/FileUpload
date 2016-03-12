using Xunit;
using FileUpload.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Moq;
using System.Reflection;
using System.IO;

namespace FileUpload.Controllers.xUnit.Tests
{
    public class FileBaseControllerTests
    {
        [Fact(DisplayName = "对FileUploadController的单元测试")]
        public void SaveFileTest()
        {
            FileBaseController fileBaseController = new FileUploadController();
            var httpPostFileBase = new Mock<HttpPostedFileBase>();
            //string localPathaaa =
            //    Assembly.GetExecutingAssembly().Location;
            string localPath =
                Assembly.GetExecutingAssembly().Location.Remove(Assembly.GetExecutingAssembly().Location.IndexOf("FileUpload.xUnit.Tests.dll"));
            string fileFullName = "xUnitTestTempFile.txt";


            var _stream = new FileStream(localPath + "packages.config",
                      FileMode.Open);

            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var files = new Mock<HttpFileCollectionBase>();
            var file = new Mock<HttpPostedFileBase>();
            context.Setup(x => x.Request).Returns(request.Object);

            files.Setup(x => x.Count).Returns(1);

            // The required properties from my Controller side
            file.Setup(x => x.InputStream).Returns(_stream);
            file.Setup(x => x.ContentLength).Returns((int)_stream.Length);
            file.Setup(x => x.FileName).Returns("packages.config");
            file.Setup(x => x.ContentType).Returns("text/plain");
            file.Setup(x => x.SaveAs(fileFullName));
            files.Setup(x => x.Get(0).InputStream).Returns(file.Object.InputStream);
            request.Setup(x => x.Files).Returns(files.Object);
            request.Setup(x => x.Files[0]).Returns(file.Object);


            ////Act
            bool result = fileBaseController.SaveFile("D:\\代码\\ASP.NET\\FileUpload\\FileUpload\\Upload", fileFullName, file.Object);



            Assert.True(result);
        }
    }
}