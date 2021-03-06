﻿using System;
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                string thisDir = System.IO.Directory.GetCurrentDirectory();
                int cutIndex = thisDir.LastIndexOf("FileUpload.Tests");
                localPath = thisDir.Substring(0, cutIndex) + "FileUpload\\Upload";
            }
            if (Request.Files.Count == 0)
            {
                return Json(new { error = true });
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


        }
    }
}