using CourseWork.Lib.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Lib
{
    public class CWContext : DbContext
    {
        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<Area> Areas { get; set; }

        public virtual DbSet<Specialization> Specializations { get; set; }

        /// <summary>
        /// Настройка подключения к БД
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionStr = "Server=localhost;Database=cwdb;Trusted_Connection=False;MultipleActiveResultSets=true;User Id=SA;Password=VanyaNaly0t!;";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                connectionStr = "Server=localhost\\SQLEXPRESS;Database=cwdb;Trusted_Connection=True;";
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connectionStr);
        }

        /// <summary>
        /// Настройки при создании модели данных
        /// </summary>
        /// <param name="modelBuilder">строитель модели</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
