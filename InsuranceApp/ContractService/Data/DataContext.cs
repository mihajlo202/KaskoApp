using ContractService.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        public DbSet<Contract> Contracts { get; set; }
        public DbSet<CarCasco> CarCascos { get; set; }
    }
}
