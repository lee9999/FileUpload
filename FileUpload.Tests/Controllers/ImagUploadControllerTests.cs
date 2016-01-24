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

namespace FileUpload.Controllers.Tests
{
    [TestClass()]
    public class ImagUploadControllerTests
    {
        //[TestMethod()]
        //public void ImagUpTest()
        //{
        //    //Arrange
        //    //Act
        //    //Assert
        //}

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

        [TestMethod()]
        public void IsFileExAcceptableTest()
        {
            
            //Console.WriteLine(Assembly.GetExecutingAssembly().Location.Remove(Assembly.GetExecutingAssembly().Location.IndexOf("bin")));
            
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
    }
}