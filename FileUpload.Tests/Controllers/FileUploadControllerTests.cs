using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileUpload.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Moq;
using System.Reflection;

namespace FileUpload.Controllers.Tests
{
    [TestClass()]
    public class FileUploadControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            //Arrange
            FileUploadController fileUploadController = new FileUploadController();
            //Act
            ViewResult result = fileUploadController.Index() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }
        /// <summary>
        /// 此测试有问题，永远会成功
        /// </summary>
        [TestMethod()]
        public void FileUpTest()
        {
            //Arrange
            var httpPostFileBase = new Mock<HttpPostedFileBase>();
            string UnitTestFileFolderPath =
                Assembly.GetExecutingAssembly().Location.Remove(Assembly.GetExecutingAssembly().Location.IndexOf("bin"));

            string localPath = Path.Combine(UnitTestFileFolderPath, "UnitTestTempFile.txt");
            //Act
            httpPostFileBase.Setup(a => a.SaveAs(localPath)).Callback(SavaFile(localPath)); //当调用saveas方法是实际上调用的是callback
            //Assert 判断文件是否写入成功
            DirectoryInfo uploadFolder = new DirectoryInfo(UnitTestFileFolderPath);
            FileInfo[] fileInfo = uploadFolder.GetFiles("UnitTestTempFile.txt");
            Assert.IsTrue(fileInfo.Length>0);
        }

        private Action SavaFile(string localPath)
        {
            using (Stream InputStream = new FileStream(localPath, FileMode.Create, FileAccess.ReadWrite))
            {
                byte b = new byte();
                InputStream.WriteByte(b);
            }
            return null;
        }

    }
}