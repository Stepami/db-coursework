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

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Trajectory> Trajectories { get; set; }

        public virtual DbSet<TrajectoryElement> TrajectoryElements { get; set; }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns>Новый пользователь</returns>
        public async Task<User> RegisterAsync(string login, string password)
        {
            User user = new() { Login = login, Password = password };
            Users.Add(user);
            await SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns>Пользователь либо <see langword="null" />, если логин или пароль неверны</returns>
        public async Task<User> AuthAsync(string login, string password) => await Users.FirstOrDefaultAsync(u => u.Login == login && u.Password == password);

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
            modelBuilder.Entity<Trajectory>()
                .HasOne(t => t.Specialization)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TrajectoryElement>()
                .HasOne(te => te.Course)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
