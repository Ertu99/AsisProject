using AsisProject.UrunModels;
using AsisProject.UserModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AsisProject.Data
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Firma> Firmalar { get; set; }
        public DbSet<Irsaliye> IrsaliyeNumaralari { get; set; }
        public DbSet<UrunModel> Urunler { get; set; }
        public Context (DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
