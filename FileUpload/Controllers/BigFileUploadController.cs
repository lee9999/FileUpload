using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using FileUpload.Models;

namespace FileUpload.Controllers
{
    public class BigFileUploadController : FileBaseController
    {
        // GET: BigFileUpload
        public ActionResult Index()
        {
            return View();
        }

        // GET: FileUpload
        //解决文件上传最大4M的显示可在web.config中设置，当前设置，最大上传大小2GB，最大上传时间一小时
        public ActionResult BigFileUp(string guid, string md5value, string chunks, string chunk, string id, string name, string type, string lastModifiedDate, int size, HttpPostedFileBase file)
        {
            //object lockObj = null;

            if (Request.Files.Count == 0)
            {
                //return HttpNotFound();
                return Json(new { error = true });
            }



            string ex = Path.GetExtension(file.FileName);
            string fileFullName = String.Empty;
            string localPath = String.Empty;
            try
            {
                localPath = Path.Combine(HttpRuntime.AppDomainAppPath, "Upload");
            }
            catch (Exception)
            {

                localPath = "D:\\代码\\ASP.NET\\FileUpload\\FileUpload\\Upload";//单元测试用
            }




            //取得分块和信号信息
            int fenkuai = 0;
            int xuhao = 0;
            if (chunks != null && chunk != null)
            {
                fenkuai = Convert.ToInt32(chunks);
                xuhao = Convert.ToInt32(chunk);
                //isOk = fenkuai;//每次写入文件后-1，到0了就可以合并了(已放弃)
            }
            else
            {
                //文件没有分块直接保存
                //return HttpNotFound();
                fileFullName = Guid.NewGuid().ToString("N") + ex;
                //file.SaveAs(Path.Combine(HttpRuntime.AppDomainAppPath + "\\Upload", fileFullName));

                if (!SaveFile(localPath, fileFullName, file))
                {
                    return Json(new { error = true });
                }
                else
                {
                    
                }
                FileModel model = new FileModel();
                FileUpload.Models.FileInfo upfile = new FileUpload.Models.FileInfo();
                upfile.FileMD5 = md5value;
                upfile.FileName = fileFullName;
                model.FileInfoSet.Add(upfile);
                model.SaveChanges();
                return Json(new
                {
                    jsonrpc = "2.0",
                    id = id,
                    filePath = "/Upload/" + fileFullName
                });
            }


            string tempPath = "Upload";
            if (guid != null)
            {
                tempPath = tempPath + "\\" + guid.ToString();
            }
            //string localPath = Path.Combine(HttpRuntime.AppDomainAppPath, tempPath);//保存的路径
            tempPath = Path.Combine(HttpRuntime.AppDomainAppPath, tempPath);//保存的路径


            //没有做文件类型验证
            //fileFullName = Guid.NewGuid().ToString("N") + ex;
            fileFullName = "temp" + xuhao.ToString() + ex;
            //应该先把分块的文件存到以guid命名的临时文件夹下，待文件夹下的文件数目达到分块数目后拼接成一个文件，并删除临时文件
            if (!SaveFile(tempPath, fileFullName, file))
            {
                return Json(new { error = true });
            }
            else
            {
                //return Json(new
                //{
                //    jsonrpc = "2.0",
                //    id = id,
                //    filePath = "/Upload/" + fileFullName
                //});
            }


            #region MyRegion
            //if (!System.IO.Directory.Exists(tempPath))
            //{
            //    System.IO.Directory.CreateDirectory(tempPath);//不存在该文件夹就新建一个
            //}
            //try
            //{
            //    //if (isOk == 1)
            //    //{

            //    //}
            //    file.SaveAs(Path.Combine(tempPath, fileFullName));
            //    //isOk = isOk - 1;

            //}
            //catch (Exception)
            //{
            //    //异常处理   Log4Net 
            //    //return HttpNotFound();
            //    return Json(new { error = true });//新的错误返回方式，更加轻量！
            //} 
            #endregion


            //finally
            if (xuhao == 0)//第一个分块负责上传的进程留下来合并文件
            {
                //此处有多线程问题，所以可能出现合并出多个文件的情况，而且下面只判断了文件数目，可能文件还没写入完成！（已解决）

                //待文件夹下的文件数目达到分块数目后拼接成一个文件，并删除临时文件
                //fileFullName文件夹
                int count = 0;
                DirectoryInfo TempFolder = new DirectoryInfo(tempPath);
                foreach (var tempFile in TempFolder.GetFiles())
                {
                    count = count + 1;
                }
                if (count == fenkuai)
                {
                    //while (isOk != 0)
                    //{
                    //    Thread.Sleep(1000);
                    //}
                    //lock (lockObject)
                    //{

                    //}
                    //达到合并的要求了
                    //序号从0开始
                    string path = Path.Combine(HttpRuntime.AppDomainAppPath, "Upload");//保存的路径
                    //string path = Path.Combine(HttpRuntime.AppDomainAppPath, "Upload\\"+guid);//测试：保存的路径

                    string filefullname = Guid.NewGuid().ToString() + ex;
                    //下面应该用using块实现
                    FileStream fa = new FileStream(path + "\\" + filefullname, FileMode.Append, FileAccess.Write);
                    for (int i = 0; i < count; i++)
                    {
                        byte[] buffer = new byte[11 * 1024 * 1024];
                        //FileStream fileTemp = new FileStream(tempPath + "\\temp"+i+ex, FileMode.Open, FileAccess.Read,FileShare.ReadWrite);

                        //解决了文件占用问题
                        int tryTimes = 0;
                        FileStream fileTemp = null;
                        while (fileTemp == null)
                        {
                            try
                            {
                                fileTemp = new FileStream(tempPath + "\\temp" + i + ex, FileMode.Open, FileAccess.Read);
                                tryTimes++;
                                if (tryTimes > 100) //次数大于100就结束等待
                                {
                                    return Json(new { error = true });
                                }
                            }
                            catch (Exception)
                            {

                            }
                        }


                        int buffersize = Convert.ToInt32(fileTemp.Length);

                        fileTemp.Read(buffer, 0, buffersize);
                        fa.Write(buffer, 0, buffersize);

                        //fileTemp.Read(buffer, 0, 10*1024*1024);
                        //fa.Write(buffer, 0, 10 * 1024 * 1024);


                        fileTemp.Flush();
                        //fileTemp.Close();
                        fileTemp.Dispose();







                        if (i == count - 1)
                        {
                            TempFolder.Delete(true);//删除temp目录 
                        }
                       
                    }




                    //将文件信息写入数据库
                    FileModel model = new FileModel();
                    FileUpload.Models.FileInfo upfile = new FileUpload.Models.FileInfo();
                    upfile.FileMD5 = md5value;
                    upfile.FileName = filefullname;
                    model.FileInfoSet.Add(upfile);
                    model.SaveChanges();

                    fa.Flush();
                    //fa.Close();//关闭流
                    fa.Dispose();
                }
            }
            return Json(new
            {
                jsonrpc = "2.0",
                id = id,
                filePath = "/Upload/" + fileFullName
            });

        }


        public ActionResult IsMD5Exist(string fileMd5, string fileName, string fileID)
        {

            FileModel model = new FileModel();
            FileUpload.Models.FileInfo file = new FileUpload.Models.FileInfo();
            file = (from c in model.FileInfoSet
                    where c.FileMD5 == fileMd5
                    select c).FirstOrDefault();
            if (file == null)
            {
                return HttpNotFound();//代表服务器上没有这个文件
            }
            
            return Json(new//存在MD5相同的文件
            {
                Eixst = true
            });
        }



    }
}