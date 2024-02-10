using Microsoft.EntityFrameworkCore;
using TechExam3.Model;

namespace TechExam3.Repository
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        public DbSet<EmployeeModel> Employee { get; set; }
    }
}
