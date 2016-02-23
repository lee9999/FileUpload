using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace FileUpload.Controllers
{
    public class FileUploadController : FileBaseController
    {
        public ViewResult Index()
        {
            return View();
        }
        // GET: FileUpload
        //解决文件上传最大4M的显示可在web.config中设置，当前设置，最大上传大小2GB，最大上传时间一小时
        public ActionResult FileUp(string id, string name, string type, string lastModifiedDate, int size, HttpPostedFileBase file)
        {
            string fileFullName = String.Empty;
            string localPath = string.Empty;
            #region MyRegion
            //string localPath =
            //    Assembly.GetExecutingAssembly().Location.Remove(Assembly.GetExecutingAssembly().Location.IndexOf("bin"));
            //localPath = Path.Combine(localPath, "Upload"); 
            #endregion
            try
            {
                localPath = Path.Combine(HttpRuntime.AppDomainAppPath, "Upload");
            }
            catch (Exception)
            {

                 localPath = "D:\\代码\\ASP.NET\\FileUpload\\FileUpload\\Upload";//单元测试用
            }
            if (Request.Files.Count == 0)
            {
                //return HttpNotFound();
                //return Json(new { jsonrpc = 2.0, error = new { code = 102, message = "保存失败" }, id = "id" });
                return Json(new { error = true });//新的错误返回方式，更加轻量！
            }
            string ex = Path.GetExtension(file.FileName);
            //没有做文件类型验证
            fileFullName = Guid.NewGuid().ToString("N") + ex;
            //也可以根据需要抽取到FileBaseController里面
            if (!SaveFile(localPath, fileFullName, file))
            {
                return Json(new { error = true });
            }
            else
            {
                return Json(new
                {
                    jsonrpc = "2.0",
                    id = id,
                    filePath = "/Upload/" + fileFullName
                });
            }

            #region
            //if (!System.IO.Directory.Exists(localPath))
            //{
            //    System.IO.Directory.CreateDirectory(localPath);
            //}
            //try
            //{
            //    file.SaveAs(Path.Combine(localPath, fileFullName));
            //}
            //catch (Exception)
            //{
            //    //异常处理   Log4Net 
            //    //return HttpNotFound();
            //    return Json(new { error = true });//新的错误返回方式，更加轻量！
            //}
            //return Json(new
            //{
            //    jsonrpc = "2.0",
            //    id = id,
            //    filePath = "/Upload/" + fileFullName
            //});
            #endregion

        }

        #region 方法移到了FileBaseController
        //private bool SaveFile(string localPath, string fileFullName, HttpPostedFileBase file)
        //{
        //    if (!System.IO.Directory.Exists(localPath))
        //    {
        //        System.IO.Directory.CreateDirectory(localPath);
        //    }
        //    try
        //    {
        //        file.SaveAs(Path.Combine(localPath, fileFullName));
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //} 
        #endregion
    }
}