using Microsoft.EntityFrameworkCore;

namespace WebApp1_T1.Data
{
    public class MyAppContext : DbContext
    {
        public DbSet<Workshop> Workshop { get; set; }

        public MyAppContext()
        {
            // ЭТА СТРОКА: автоматически создаст файл базы и правильные таблицы, если их нет
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=Workshop.db");
            }
        }
    }
}
