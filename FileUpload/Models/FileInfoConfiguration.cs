using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace FileUpload.Models
{
    public class FileInfoConfiguration:EntityTypeConfiguration<FileInfo>
    {
        public FileInfoConfiguration()
        {
            HasKey(a => a.FileMD5);//主键
            Property(a => a.FileMD5).IsRequired();
            Property(a => a.FileName).IsRequired();
        }
    }
}