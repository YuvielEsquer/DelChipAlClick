using Microsoft.EntityFrameworkCore;

namespace ApiTallerDelChipAlClick.Models
{
    public class TallerContext : DbContext
    {
        public TallerContext(DbContextOptions<TallerContext> options)
            : base(options)
        { }

        public DbSet<LedsModel> Leds { get; set; }
        public DbSet<CommonModulesModel> CommonModules { get; set; }
        public DbSet<UsersModel> Users { get; set; }
    }
}
