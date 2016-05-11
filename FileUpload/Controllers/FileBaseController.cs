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
        /// <summary>
        /// 保存文件的共用方法
        /// </summary>
        /// <param name="localPath">保存的路径</param>
        /// <param name="fileFullName">文件名</param>
        /// <param name="file">文件</param>
        /// <returns></returns>
        public bool SaveFile(string localPath, string fileFullName, HttpPostedFileBase file)
        {
            if (localPath == string.Empty || fileFullName == String.Empty || file == null)
            {
                return false;
            }

            if (!System.IO.Directory.Exists(localPath))
            {
                System.IO.Directory.CreateDirectory(localPath);
            }

            try
            {
                file.SaveAs(Path.Combine(localPath, fileFullName));
                DirectoryInfo uploadFolder = new DirectoryInfo(localPath);
                FileInfo[] fileInfo = uploadFolder.GetFiles(fileFullName);
                bool isExists = false;
                foreach (var info in fileInfo)
                {
                    if (info.Name == fileFullName)
                    {
                        isExists = true;
                    }

                }
                return isExists;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}