using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ZipeCodeConsole.Repository
{
    public class Context : DbContext
    {
        public Context() : base("Server=localhost;Database=Services;User Id=sa; Password=@TRUNKS;")
        {

        }

        public virtual DbSet<ZipeCode> ZipeCode { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
