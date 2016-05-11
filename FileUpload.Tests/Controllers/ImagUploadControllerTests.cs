using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileUpload.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.IO;
using System.Web;
using System.Reflection;
using Moq;

namespace FileUpload.Controllers.Tests
{
    [TestClass()]
    public class ImagUploadControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            //Arrange
            ImagUploadController imagController = new ImagUploadController();
            //Act
            ViewResult result = imagController.Index() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }


        /// <summary>
        /// 判断是否是真正的图片
        /// </summary>
        [TestMethod()]
        public void IsRealImagTest()
        {
            //Arrange
            ImagUploadController imagController = new ImagUploadController();
            string UnitTestFileFolderPath =
                Assembly.GetExecutingAssembly().Location.Remove(Assembly.GetExecutingAssembly().Location.IndexOf("bin"));

            //Act
            string path = Path.Combine(UnitTestFileFolderPath, "UnitTestFileFolder");//保存的路径
            DirectoryInfo floder = new DirectoryInfo(path);
            foreach (var filefullname in floder.GetFiles())
            {
                using (FileStream fs = new FileStream(path + "//" + filefullname.Name, FileMode.Open))
                {
                    BinaryReader br = new BinaryReader(fs);
                    if (!imagController.IsRealImag(fs, br))
                    {
                        //Assert
                        Console.WriteLine("文件类型" + filefullname.Name + "验证错误");
                        Assert.Fail();
                    }
                    else
                    {
                        Console.WriteLine("文件类型" + filefullname.Name + "验证无误");
                    }
                }
            }


            #region
            //foreach (var fileClass in fileClassList)
            //{
            //    if (fileClass != "7173" && fileClass != "255216" && fileClass != "6677" && fileClass != "13780")
            //    {
            //        //文件后缀不符合要求

            //        //Assert
            //        Console.WriteLine("文件类型" + fileClass + "验证错误");
            //        Assert.Fail();
            //    }
            //    else
            //    {

            //        //Assert
            //        Console.WriteLine("文件类型" + fileClass + "验证无误");
            //        //Assert.IsNotNull("success");
            //    }
            //}
            #endregion
        }
        /// <summary>
        /// 通过后缀名判断是否是图片
        /// </summary>
        [TestMethod()]
        public void IsFileExAcceptableTest()
        {

            //Arrange
            ImagUploadController imagController = new ImagUploadController();
            string UnitTestFileFolderPath =
                Assembly.GetExecutingAssembly().Location.Remove(Assembly.GetExecutingAssembly().Location.IndexOf("bin"));
            //Act

            //imagController.IsFileExAcceptable()
            string path = Path.Combine(UnitTestFileFolderPath, "UnitTestFileFolder");//保存的路径
            DirectoryInfo floder = new DirectoryInfo(path);
            foreach (var filefullname in floder.GetFiles())
            {
                Console.WriteLine(filefullname.Name);
                Console.WriteLine(filefullname.Extension);
                if (!imagController.IsFileExAcceptable(filefullname.Extension.ToLower()))
                {
                    //Assert
                    Console.WriteLine("后缀名" + filefullname.Extension + "验证错误");
                    Assert.Fail();
                }
                else
                {
                    Console.WriteLine("后缀名" + filefullname.Extension + "验证无误");
                }
            }

        }
        /// <summary>
        /// 对ImagUp方法进行测试,传入非图片
        /// </summary>
        [TestMethod()]
        public void ImagUpTestForNonImage()
        {
            //Arrange
            //声明
            ImagUploadController imagUploadController = new ImagUploadController();
            var httpPostFileBase = new Mock<HttpPostedFileBase>();
            var file = new Mock<HttpPostedFileBase>();
            var httpContext = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();



            //赋值
            string localPath =
                Assembly.GetExecutingAssembly().Location.Remove(Assembly.GetExecutingAssembly().Location.IndexOf("bin"));

            string fileFullName = "UnitTestTempFile.txt";
            byte[] buffer = new byte[1 * 1024 * 1024];
            //初始化moq对象

            file.Setup(a => a.FileName).Returns(fileFullName);

            request.Setup(a => a.Files.Count).Returns(1);
            httpContext.Setup(a => a.Request).Returns(request.Object);
            ControllerContext controllerContext = new ControllerContext();
            controllerContext.HttpContext = httpContext.Object;
            imagUploadController.ControllerContext = controllerContext;
            //使用

            JsonResult result = imagUploadController.ImagUp("id", fileFullName, "type", "lastModifiedDate", 1024 * 1024, file.Object) as JsonResult;
            string josnResult = result.Data.ToString();
            Console.WriteLine(josnResult);
            //Assert //实际上由于一些问题，就算代码正确文件写入还是失败，可能是引用的问题，暂时不知道怎么解决，比如上面的代码跟踪单步执行的话发现到了保存文件的时候，保存应该结束了，但是似乎抛出了异常？！瞬间到了cache块里面又瞬间跳出来了，一个未知的Exception错误，但是返回值仍然是true，而在Upload里面执行却没有问题，所以感觉是引用出了问题
            Assert.AreEqual("{ error = True }", josnResult);
        }


        /// <summary>
        /// 对ImagUp方法进行测试,传入图片
        /// </summary>
        //[TestMethod()]
        //public void ImagUpTestForImage()
        //{
        //    //Arrange
        //    //声明
        //    ImagUploadController imagUploadController = new ImagUploadController();
        //    var httpPostFileBase = new Mock<HttpPostedFileBase>();
        //    var file = new Mock<HttpPostedFileBase>();
        //    var httpContext = new Mock<HttpContextBase>();
        //    var request = new Mock<HttpRequestBase>();



        //    //赋值
        //    string localPath =
        //        Assembly.GetExecutingAssembly().Location.Remove(Assembly.GetExecutingAssembly().Location.IndexOf("bin"));

        //    string fileFullName = "UnitTestTempFile.png";
        //    byte[] buffer = new byte[1 * 1024 * 1024];
        //    //初始化moq对象

        //    file.Setup(a => a.FileName).Returns(fileFullName);

        //    request.Setup(a => a.Files.Count).Returns(1);
        //    httpContext.Setup(a => a.Request).Returns(request.Object);
        //    ControllerContext controllerContext = new ControllerContext();
        //    controllerContext.HttpContext = httpContext.Object;
        //    imagUploadController.ControllerContext = controllerContext;
        //    //使用


        //    //Act
        //    string path = Path.Combine(localPath, "UnitTestFileFolder");//保存的路径
        //    DirectoryInfo floder = new DirectoryInfo(path);
        //    foreach (var filefullname in floder.GetFiles())
        //    {
        //        using (FileStream fs = new FileStream(path + "//" + filefullname.Name, FileMode.Open))
        //        {
        //            BinaryReader br = new BinaryReader(fs);

        //            HttpPostedFileBase fff = new FileStream(path + "//" + filefullname.Name, FileMode.Open);
        //            request.Setup(a => a.Files).Returns();
        //              这里的返回值必须是真正的图片，不会弄了
        //        }
        //    }










        //    JsonResult result = imagUploadController.ImagUp("id", fileFullName, "type", "lastModifiedDate", 1024 * 1024, file.Object) as JsonResult;
        //    string josnResult = result.Data.ToString();
        //    Console.WriteLine(josnResult);
        //    //Assert //实际上由于一些问题，就算代码正确文件写入还是失败，可能是引用的问题，暂时不知道怎么解决，比如上面的代码跟踪单步执行的话发现到了保存文件的时候，保存应该结束了，但是似乎抛出了异常？！瞬间到了cache块里面又瞬间跳出来了，一个未知的Exception错误，但是返回值仍然是true，而在Upload里面执行却没有问题，所以感觉是引用出了问题
        //    Assert.AreEqual("{ error = True }", josnResult);
        //}
    }
}