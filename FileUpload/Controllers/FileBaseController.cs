using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileUpload.Controllers
{
    /// <summary>
    /// 共用的方法放在这里
    /// </summary>
    public class FileBaseController : Controller
    {
        //// GET: FileBase
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public bool SaveFile(string localPath, string fileFullName, HttpPostedFileBase file)
        {
            
            if (!System.IO.Directory.Exists(localPath))
            {
                System.IO.Directory.CreateDirectory(localPath);
            }
            try
            {
                file.SaveAs(Path.Combine(localPath, fileFullName));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}