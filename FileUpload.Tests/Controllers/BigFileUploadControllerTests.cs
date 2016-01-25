using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileUpload.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Moq;
using System.Web;
using System.Reflection;

namespace FileUpload.Controllers.Tests
{
    [TestClass()]
    public class BigFileUploadControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            //Arrange
            BigFileUploadController bigFileUploadController = new BigFileUploadController();
            //Act
            ActionResult result = bigFileUploadController.Index() as ActionResult;
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void IsMD5ExistTest()
        {
            #region
            ////Arrange
            //var bigFileUploadController = new Mock<BigFileUploadController>();
            //bigFileUploadController.Setup(a=>a.IsMD5Exist())
            ////Act
            //ActionResult result = bigFileUploadController.IsMD5Exist("ef13915dda1f0dd3e2c5576ca2c7dce2", "fileName", "FileID") as ActionResult;

            ////Assert

            #endregion




            //Arrange
            BigFileUploadController bigFileUploadController = new BigFileUploadController();
            //Act
            ActionResult result = bigFileUploadController.IsMD5Exist("ef13915dda1f0dd3e2c5576ca2c7dce2", "fileName", "FileID") as ActionResult;

            //Assert  失败： System.Web.Mvc.HttpNotFoundResult 成功： System.Web.Mvc.JsonResult
            string a = result.ToString();
            Assert.AreEqual(a, "System.Web.Mvc.JsonResult");
        }


        [TestMethod()]
        public void MD5DoesNotExistTest()
        {
            #region
            ////Arrange
            //var bigFileUploadController = new Mock<BigFileUploadController>();
            //bigFileUploadController.Setup(a=>a.IsMD5Exist())
            ////Act
            //ActionResult result = bigFileUploadController.IsMD5Exist("ef13915dda1f0dd3e2c5576ca2c7dce2", "fileName", "FileID") as ActionResult;

            ////Assert

            #endregion




            //Arrange
            BigFileUploadController bigFileUploadController = new BigFileUploadController();
            //Act
            ActionResult result = bigFileUploadController.IsMD5Exist("this md5 is not exist", "fileName", "FileID") as ActionResult;

            //Assert  失败： System.Web.Mvc.HttpNotFoundResult 成功： System.Web.Mvc.JsonResult
            string a = result.ToString();
            Assert.AreEqual(a, "System.Web.Mvc.HttpNotFoundResult");
        }
        /// <summary>
        /// 未分块文件的测试
        /// </summary>
        [TestMethod()]
        public void BigFileUpTest()
        {
            //Arrange
            //声明
            BigFileUploadController bigFileUploadController = new BigFileUploadController();

            var file = new Mock<HttpPostedFileBase>();
            var httpContext = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();



            //赋值
            string localPath =
                Assembly.GetExecutingAssembly().Location.Remove(Assembly.GetExecutingAssembly().Location.IndexOf("bin"));

            string fileFullName = "UnitTestTempFile.txt";
           
            file.Setup(a => a.FileName).Returns(fileFullName);
            
            request.Setup(a => a.Files.Count).Returns(1);
            httpContext.Setup(a => a.Request).Returns(request.Object);
            ControllerContext controllerContext = new ControllerContext();
            controllerContext.HttpContext = httpContext.Object;
            bigFileUploadController.ControllerContext = controllerContext;
            //使用

            JsonResult result = bigFileUploadController.BigFileUp("guid","md5value"+Guid .NewGuid().ToString(),null,null,"id", fileFullName, "type", "lastModifiedDate", 1024 * 1024, file.Object) as JsonResult;
            string josnResult = result.Data.ToString();
            Console.WriteLine(josnResult);
            //Assert //实际上由于一些问题，就算代码正确文件写入还是失败，可能是引用的问题，暂时不知道怎么解决，比如上面的代码跟踪单步执行的话发现到了保存文件的时候，保存应该结束了，但是似乎抛出了异常？！瞬间到了cache块里面又瞬间跳出来了，一个未知的Exception错误，但是返回值仍然是true，而在Upload里面执行却没有问题，所以感觉是引用出了问题
            Assert.AreNotEqual("{ error = true }", josnResult);
        }





        ///// <summary>
        ///// 分块文件的测试    不知道怎么做，汗！
        ///// </summary>
        //[TestMethod()]
        //public void BigFileUpTestForChunk()
        //{
        //    //Arrange
        //    //声明
        //    BigFileUploadController bigFileUploadController = new BigFileUploadController();

        //    var file = new Mock<HttpPostedFileBase>();
        //    var httpContext = new Mock<HttpContextBase>();
        //    var request = new Mock<HttpRequestBase>();



        //    //赋值
        //    string localPath =
        //        Assembly.GetExecutingAssembly().Location.Remove(Assembly.GetExecutingAssembly().Location.IndexOf("bin"));

        //    string fileFullName = "UnitTestTempFile.txt";

        //    file.Setup(a => a.FileName).Returns(fileFullName);

        //    request.Setup(a => a.Files.Count).Returns(1);
        //    httpContext.Setup(a => a.Request).Returns(request.Object);
        //    ControllerContext controllerContext = new ControllerContext();
        //    controllerContext.HttpContext = httpContext.Object;
        //    bigFileUploadController.ControllerContext = controllerContext;
        //    //使用

        //    JsonResult result = bigFileUploadController.BigFileUp("guid", "md5value" + Guid.NewGuid().ToString(), null, null, "id", fileFullName, "type", "lastModifiedDate", 1024 * 1024, file.Object) as JsonResult;
        //    string josnResult = result.Data.ToString();
        //    Console.WriteLine(josnResult);
        //    //Assert //实际上由于一些问题，就算代码正确文件写入还是失败，可能是引用的问题，暂时不知道怎么解决，比如上面的代码跟踪单步执行的话发现到了保存文件的时候，保存应该结束了，但是似乎抛出了异常？！瞬间到了cache块里面又瞬间跳出来了，一个未知的Exception错误，但是返回值仍然是true，而在Upload里面执行却没有问题，所以感觉是引用出了问题
        //    Assert.AreNotEqual("{ error = true }", josnResult);
        //}
    }
}