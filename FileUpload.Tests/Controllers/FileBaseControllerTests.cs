using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileUpload.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Reflection;
using System.IO;
using Moq;
using System.Runtime.CompilerServices;
using Moq.Protected;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace FileUpload.Controllers.Tests
{
    [TestClass()]
    public class FileBaseControllerTests
    {
        /// <summary>
        /// 因为暂时没法mock正确的HttpPostFileBase,所以暂时没法通过单元测试
        /// </summary>
        [TestMethod()]
        public void SaveFileTest()
        {
            //Arrange

            FileBaseController fileBaseController = new FileUploadController();
            //var httpPostFileBase = new Mock<HttpPostedFileBase>();
            string localPath =
                Assembly.GetExecutingAssembly().Location.Remove(Assembly.GetExecutingAssembly().Location.IndexOf("bin"));

            string fileFullName = "UnitTestTempFile.txt";


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
            
            fileBaseController.ControllerContext=new ControllerContext(context.Object,new RouteData(),fileBaseController);
            ////Act
            bool result = fileBaseController.SaveFile("D:\\代码\\ASP.NET\\FileUpload\\FileUpload\\Upload", fileFullName, file.Object);


            

            //Assert 判断文件是否写入成功
            //DirectoryInfo uploadFolder = new DirectoryInfo(localPath);
            //FileInfo[] fileInfo = uploadFolder.GetFiles("UnitTestTempFile.txt");
            //Assert.IsTrue(fileInfo.Length > 0);
            
            Assert.IsTrue(result);
        }
    }
}