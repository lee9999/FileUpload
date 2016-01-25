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
            //httpPostFileBase.Setup(a => a.SaveAs(localPath + fileFullName));
            //bool result= fileBaseController.SaveFile(localPath, fileFullName, httpPostFileBase.Object);
            //var file = new Mock<HttpPostedFileBase>();
            //file.Setup(a => a.SaveAs(localPath + fileFullName));

            ////Act
            bool result = fileBaseController.SaveFile(localPath, fileFullName, httpPostFileBase.Object);

            

            




            #region MyRegion



            ////byte[] buffer = new byte[1 * 1024 * 1024];
            //using (FileStream stream = new FileStream(localPath + "\\UnitTestTempFile.txt", FileMode.OpenOrCreate))
            //{
            //    //stream.Read(buffer, 0, buffer.Length);
            //    //var file = new Mock<HttpPostedFileBase>();
            //    //httpPostFileBase.Setup(a => a.SaveAs(localPath + fileFullName));
            //    //file.Setup(a => a.InputStream).Returns(stream);
                

            //    //Act

            //    //fileBaseController.SaveFile(localPath, fileFullName, httpPostFileBase.Object);

            //    // file.Object.SaveAs();
            //}
            #endregion

            #region MyRegion



            //HttpPostedFile httpPostedFile = new HttpPostedFile(fileFullName, "text/plain",(HttpInputStream) stream);
            //HttpPostedFileBase file =new HttpPostedFileWrapper();
            //HttpPostedFileBase file = new HttpPostedFile(fileFullName,"text/plain",stream);





            #endregion

            //Assert 判断文件是否写入成功
            //DirectoryInfo uploadFolder = new DirectoryInfo(localPath);
            //FileInfo[] fileInfo = uploadFolder.GetFiles("UnitTestTempFile.txt");
            //Assert.IsTrue(fileInfo.Length > 0);

            //实际上由于一些问题，就算代码正确文件写入还是失败，可能是引用的问题，暂时不知道怎么解决，比如上面的代码跟踪单步执行的话发现到了保存文件的时候，保存应该结束了，但是似乎抛出了异常？！瞬间到了cache块里面又瞬间跳出来了，一个未知的Exception错误，但是返回值仍然是true，而在Upload里面执行却没有问题，所以感觉是引用出了问题
            Assert.IsTrue(result);
        }
    }
}