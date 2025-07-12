using Microsoft.EntityFrameworkCore;

namespace NovaMed.Data
{
    public class NovaMedContext : DbContext
    {
        public NovaMedContext(DbContextOptions<NovaMedContext> options) : base(options) { }


        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
