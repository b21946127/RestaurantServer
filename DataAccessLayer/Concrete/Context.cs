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
        public DbSet<MenuItemOption> MenuItemOptions { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<MenuItemSet> MenuItemSets { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MenuItemMenuItemSet> MenuItemMenuItemSets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure unique index for Menu DayOfWeek
            modelBuilder.Entity<Menu>().HasIndex(x => new { x.DayOfWeek }).IsUnique();

            // Configure seed data for Menus
            modelBuilder.Entity<Menu>().HasData(
                new Menu { Id = 1, DayOfWeek = DayOfWeekEnum.Monday },
                new Menu { Id = 2, DayOfWeek = DayOfWeekEnum.Tuesday },
                new Menu { Id = 3, DayOfWeek = DayOfWeekEnum.Wednesday },
                new Menu { Id = 4, DayOfWeek = DayOfWeekEnum.Thursday },
                new Menu { Id = 5, DayOfWeek = DayOfWeekEnum.Friday },
                new Menu { Id = 6, DayOfWeek = DayOfWeekEnum.Saturday },
                new Menu { Id = 7, DayOfWeek = DayOfWeekEnum.Sunday }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    Name = "customer",
                    Surname = "customer",
                    Password = "customer",
                    Email = "customer@xx.com",
                }
                );



            // Configure many-to-many relationship between MenuItem and MenuItemSet
            modelBuilder.Entity<MenuItemMenuItemSet>()
                .HasKey(mis => new { mis.MenuItemId, mis.MenuItemSetId });

            modelBuilder.Entity<MenuItemMenuItemSet>()
                .HasOne(mis => mis.MenuItem)
                .WithMany(mi => mi.MenuItemMenuItemSets)
                .HasForeignKey(mis => mis.MenuItemId);

            modelBuilder.Entity<MenuItemMenuItemSet>()
                .HasOne(mis => mis.MenuItemSet)
                .WithMany(ms => ms.MenuItemMenuItemSets)
                .HasForeignKey(mis => mis.MenuItemSetId);
        }
    }
}
