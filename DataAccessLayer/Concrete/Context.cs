using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Concrete
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Integration> Integrations { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<MenuCategoryMenuItem> MenuCategoryMenuItems { get; set; }
        public DbSet<MenuItemOption> MenuItemOptions { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuCategoryMenuItem>()
                .HasKey(mci => new { mci.MenuCategoryId, mci.MenuItemId });

            modelBuilder.Entity<MenuCategoryMenuItem>()
                .HasOne(mci => mci.MenuCategory)
                .WithMany(mc => mc.MenuCategoryItems)
                .HasForeignKey(mci => mci.MenuCategoryId);

            modelBuilder.Entity<MenuCategoryMenuItem>()
                .HasOne(mci => mci.MenuItem)
                .WithMany(mi => mi.MenuCategoryItems)
                .HasForeignKey(mci => mci.MenuItemId);



            base.OnModelCreating(modelBuilder);
        }
    }
}
