using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using Microsoft.Extensions.Configuration;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repository;

namespace DataAccessLayer.Tests.Concrete
{
    public class ContextTests
    {
        private DbContextOptions<Context> _options;
        private Context _context;

        [SetUp]
        public void SetUp()
        {
            var inMemorySettings = new Dictionary<string, string> {
        {"ConnectionStrings:WebApiDatabase", "InMemoryDatabase"}
    };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            _options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;

            _context = new Context(_options);
            _context.Database.EnsureDeleted(); 
            _context.Database.EnsureCreated(); 

            _context.MenuItems.AddRange(
                new MenuItem { Id = 1, Name = "Test1" },
                new MenuItem { Id = 2, Name = "Test2" }
            );
            _context.SaveChanges();

        }


        [Test]
        public void CanInsertMenuItemIntoDatabase()
        {
            // Arrange
            var menuItem = new MenuItem { Id = 3, Name = "TestItem3" };

            // Act
            _context.MenuItems.Add(menuItem);
            _context.SaveChanges();

            // Assert
            var result = _context.MenuItems.Find(3);
            Assert.IsNotNull(result);
            Assert.AreEqual("TestItem3", result.Name);
        }

        [Test]
        public void CanDeleteMenuItemFromDatabase()
        {
            // Arrange
            var menuItem = _context.MenuItems.Find(1);

            // Act
            _context.MenuItems.Remove(menuItem);
            _context.SaveChanges();

            // Assert
            var result = _context.MenuItems.Find(1);
            Assert.IsNull(result); 
        }
    }
}
