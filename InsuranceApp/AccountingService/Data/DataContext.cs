using AccountingService.Models;

namespace AccountingService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        public DbSet<BankStatement> BankStatement { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
    }
}