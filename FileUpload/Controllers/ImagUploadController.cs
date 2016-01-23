using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileUpload.Controllers
{
    public class ImagUploadController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        // GET: ImagUpload
        //解决文件上传最大4M的显示可在web.config中设置，当前设置，最大上传大小2GB，最大上传时间一小时
        public ActionResult ImagUp(string id, string name, string type, string lastModifiedDate, int size, HttpPostedFileBase file)
        {
            #region 判断文件后缀名
            //做文件类型验证 文件后缀名统一转为小写
            string ex = Path.GetExtension(file.FileName).ToLower();
            //gif,jpg,jpeg,bmp,png
            if (ex != ".gif" && ex != ".jpg" && ex != ".jpeg" && ex != ".bmp" && ex != ".png")
            {
                //文件后缀不符合要求
                //return HttpNotFound();
                //return Json(new { jsonrpc = 2.0, error = new { code = 102, message = "保存失败" }, id = id });//这个错误处理不管用
                return Json(new { error = true });//新的错误返回方式，更加轻量！
            }
            #endregion

            #region 或者： 验证文件类型file.ContentType,其实也是验证文件后缀
            //if (file.ContentType != "image/gif" && file.ContentType != "image/jpg" && file.ContentType != "image/jpeg" && file.ContentType != "image/bmp" && file.ContentType != "image/png")
            //{
            //    //文件类型不符合要求
            //    return Json(new { jsonrpc = 2.0, error = new { code = 102, message = "保存失败" }, id = "id" });
            //}
            #endregion



            #region 真正验证文件类型！改后缀也不行

            //FileStream fs = (FileStream)file.InputStream;



            Stream fs = file.InputStream;
            string fileClass = string.Empty;
            BinaryReader br = new BinaryReader(fs);
            byte buffer;
            byte[] b = new byte[2];
            buffer = br.ReadByte();
            b[0] = buffer;
            fileClass = buffer.ToString();
            buffer = br.ReadByte();
            b[1] = buffer;
            fileClass += buffer.ToString();

            //br.Close(); 释放资源在finally语句块中，提前释放会将file的内容清空
            //fs.Close();
            if (fileClass != "7173" && fileClass != "255216" && fileClass != "6677" && fileClass != "13780") //jpe和jpeg fileclass相同
            {
                //return HttpNotFound();//jsonrpc更加轻量，似乎我的用法有问题，解决不了
                //return Json(new { jsonrpc = 2.0, error = new { code = 102, message = "保存失败" }, id = id });//这个错误处理不管用
                return Json(new {error=true});//新的错误返回方式，更加轻量！
            }

            #endregion
            //开始保存文件
            string filePathName = String.Empty;
            string localPath = Path.Combine(HttpRuntime.AppDomainAppPath, "Upload");
            if (Request.Files.Count == 0)
            {
                //return HttpNotFound();
                //return Json(new { jsonrpc = 2.0, error = new { code = 102, message = "保存失败" }, id = id });
                return Json(new { error = true });//新的错误返回方式，更加轻量！
            }

            filePathName = Guid.NewGuid().ToString("N") + ex;
            if (!System.IO.Directory.Exists(localPath))
            {
                System.IO.Directory.CreateDirectory(localPath);
            }
            try
            {
                file.SaveAs(Path.Combine(localPath, filePathName));
            }
            catch (Exception)
            {
                //异常处理   Log4Net 
                //return HttpNotFound();
                return Json(new { error = true });//新的错误返回方式，更加轻量！
            }
            finally
            {
                br.Close();
                fs.Close();
            }
            return Json(new
            {
                jsonrpc = "2.0",
                id = id,
                filePath = "/Upload/" + filePathName
            });

        }
    }
}