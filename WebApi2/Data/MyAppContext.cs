using Microsoft.EntityFrameworkCore;

namespace WorkshopWebApi.Data
{
    public class MyAppContext : DbContext
    {
        
        public DbSet<Workshop> Workshop { get; set; }

        public DbSet<Equipment> Equipment { get; set; }

        public MyAppContext()
        {
            // ЭТА СТРОКА: автоматически создаст файл базы и правильные таблицы, если их нет
            Database.Migrate();
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
