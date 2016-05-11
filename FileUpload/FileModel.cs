using FileUpload.Models;

namespace FileUpload
{
    using System;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Linq;

    public class FileModel : DbContext
    {
        //您的上下文已配置为从您的应用程序的配置文件(App.config 或 Web.config)
        //使用“FileModel”连接字符串。默认情况下，此连接字符串针对您的 LocalDb 实例上的
        //“FileUpload.FileModel”数据库。
        //
        //如果您想要针对其他数据库和/或数据库提供程序，请在应用程序配置文件中修改“FileModel”
        //连接字符串。
        public FileModel()
            : base("name=FileModel")
        {
        }
        public FileModel(DbConnection connection, bool contextOwnsConnection)
            : base(connection, contextOwnsConnection)//用于控制是否由当前的DbContext子类控制数据库连接实例。如果contextOwnsContext属性为true，那么当DbContext子类Dispose的时候，就会把共用的DbConnection实例也Dispose掉。如果这个值是false，则数据库连接实例就需要由程序员自己写程序释放。
        {
        }
        //为您要在模型中包含的每种实体类型都添加 DbSet。有关配置和使用 Code First  模型
        //的详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=390109。

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public DbSet<FileInfo> FileInfoSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FileInfoConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}