using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using DataAccessLayer.Concrete.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace DataAccessLayer.Tests
{
    public class GenericRepositoryTests
    {
        private DbContextOptions<Context> _options;
        private Context _context;
        private GenericRepository<MenuItem> _repository;

        [SetUp]
        public void SetUp()
        {
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

            _repository = new GenericRepository<MenuItem>(_context);
        }

        [Test]
        public async Task Insert_ShouldAddEntityToContext_WhenCalled()
        {
            // Arrange
            var entity = new MenuItem { Id = 3, Name = "Test3" };

            // Act
            await _repository.InsertAsync(entity);

            // Assert
            var result = await _context.MenuItems.FindAsync(3);
            Assert.IsNotNull(result);
            Assert.AreEqual("Test3", result.Name);
        }

        [Test]
        public async Task Delete_ShouldRemoveEntityFromContext_WhenCalled()
        {
            // Arrange
            var entity = await _context.MenuItems.FindAsync(1);

            // Act
            await _repository.DeleteAsync(entity);

            // Assert
            var result = await _context.MenuItems.FindAsync(1);
            Assert.IsNull(result);
        }

        [Test]
        public async Task Get_ShouldReturnSingleEntity_WhenEntityExists()
        {
            // Act
            var result = await _repository.GetAsync(e => e.Id == 1);

            // Assert
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Test1", result.Name);
        }

        [Test]
        public async Task List_ShouldReturnAllEntities_WhenCalled()
        {
            // Act
            var result = await _repository.ListAsync();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Test1", result[0].Name);
            Assert.AreEqual("Test2", result[1].Name);
        }

        [Test]
        public async Task Update_ShouldModifyEntityInContext_WhenCalled()
        {
            // Arrange
            var entity = await _context.MenuItems.FindAsync(1);
            entity.Name = "UpdatedTest";

            // Act
            await _repository.UpdateAsync(entity);

            // Assert
            var updatedEntity = await _context.MenuItems.FindAsync(1);
            Assert.AreEqual("UpdatedTest", updatedEntity.Name);
        }
    }
}
