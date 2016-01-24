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

namespace FileUpload.Controllers.Tests
{
    [TestClass()]
    public class FileBaseControllerTests
    {
        [TestMethod()]
        public void SaveFileTest()
        {
            //Arrange

            FileBaseController fileBaseController = new FileUploadController();
            var httpPostFileBase = new Mock<HttpPostedFileBase>();
            string localPath =
                Assembly.GetExecutingAssembly().Location.Remove(Assembly.GetExecutingAssembly().Location.IndexOf("bin"));

            string fileFullName = "UnitTestTempFile.txt";
            byte[] buffer = new byte[1 * 1024 * 1024];
            using (FileStream stream = new FileStream(localPath + "\\UnitTestTempFile.txt", FileMode.OpenOrCreate))
            {
                stream.Read(buffer, 0, buffer.Length);
                var file = new Mock<HttpPostedFileBase>();
                file.Setup(a => a.SaveAs(localPath + fileFullName));



                //Act

                fileBaseController.SaveFile(localPath, fileFullName, file.Object);

                // file.Object.SaveAs();
            }



            //HttpPostedFile httpPostedFile = new HttpPostedFile(fileFullName, "text/plain",(HttpInputStream) stream);
            //HttpPostedFileBase file =new HttpPostedFileWrapper();
            //HttpPostedFileBase file = new HttpPostedFile(fileFullName,"text/plain",stream);






            //Assert 判断文件是否写入成功
            DirectoryInfo uploadFolder = new DirectoryInfo(localPath);
            FileInfo[] fileInfo = uploadFolder.GetFiles("UnitTestTempFile.txt");
            Assert.IsTrue(fileInfo.Length > 0);
        }
    }
}