using Microsoft.EntityFrameworkCore;

namespace ZipeCodeConsoleCore.Repository
{
    public class Context : DbContext
    {
        public virtual DbSet<ZipeCode> ZipeCode { get; set; }

        private readonly string connectionString;

        public Context() : base()
        {
            this.connectionString = "Server=localhost;Database=Services;User Id=sa; Password=@TRUNKS;";
            this.OnConfiguring(new DbContextOptionsBuilder<DbContext>());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
