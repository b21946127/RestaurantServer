using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs.MenuDtos;
using EntityLayer.DTOs.MenuCategoryDtos;
using System.Linq.Expressions;

namespace BusinessLayer.Tests.Concrete
{
    [TestClass]
    public class MenuManagerTests
    {
        private Mock<IMenuDal> _menuDalMock;
        private Mock<IMenuCategoryDal> _menuCategoryDalMock;
        private Mock<IMenuItemDal> _menuItemDalMock;
        private Mock<IMenuItemSetDal> _menuItemSetDalMock;
        private Mock<IIntegrationDal> _integrationDalMock;
        private Mock<IMenuItemOptionDal> _optionDalMock;
        private Mock<IMenuItemMenuItemSetDal> _menuItemMenuItemSetDalMock;
        private Mock<IMapper> _mapperMock;
        private MenuManager _menuManager;

        [TestInitialize]
        public void SetUp()
        {
            _menuDalMock = new Mock<IMenuDal>();
            _menuCategoryDalMock = new Mock<IMenuCategoryDal>();
            _menuItemDalMock = new Mock<IMenuItemDal>();
            _menuItemSetDalMock = new Mock<IMenuItemSetDal>();
            _integrationDalMock = new Mock<IIntegrationDal>();
            _optionDalMock = new Mock<IMenuItemOptionDal>();
            _menuItemMenuItemSetDalMock = new Mock<IMenuItemMenuItemSetDal>();
            _mapperMock = new Mock<IMapper>();

            _menuManager = new MenuManager(
                _menuDalMock.Object,
                _menuCategoryDalMock.Object,
                _menuItemDalMock.Object,
                _menuItemSetDalMock.Object,
                _integrationDalMock.Object,
                _optionDalMock.Object,
                _menuItemMenuItemSetDalMock.Object,
                _mapperMock.Object
            );
        }

        [TestMethod]
        public async Task GetMenuByDayAsync_ShouldReturnMenuDto_WhenMenuExists()
        {
            // Arrange
            var dayOfWeek = "Monday";
            var menu = new Menu { DayOfWeek = DayOfWeekEnum.Monday };
            var menuDto = new MenuDto { DayOfWeek = "Monday" };

            _menuDalMock.Setup(m => m.GetByAll(It.IsAny<Expression<Func<Menu, bool>>>()))
                .ReturnsAsync(menu);
            _mapperMock.Setup(m => m.Map<MenuDto>(menu))
                .Returns(menuDto);

            // Act
            var result = await _menuManager.GetMenuByDayAsync(dayOfWeek);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Monday", result.DayOfWeek);
        }


        [TestMethod]
        public async Task GetMenuByDayAsync_ShouldThrowException_WhenDayOfWeekIsInvalid()
        {
            // Arrange
            var invalidDayOfWeek = "InvalidDay";

            // Act & Assert
            var ex = await Assert.ThrowsExceptionAsync<ArgumentException>(
                async () => await _menuManager.GetMenuByDayAsync(invalidDayOfWeek));
            Assert.AreEqual("Invalid day of the week: InvalidDay", ex.Message);
        }

        [TestMethod]
        public async Task AddNewMenuAsync_ShouldReturnMenuDto_WhenValidDataProvided()
        {
            // Arrange
            var createMenuDto = new CreateMenuDto
            {
                DayOfWeek = "Monday",
                MenuCategories = new List<CreateMenuCategoryDto>()
            };
            var menu = new Menu { DayOfWeek = DayOfWeekEnum.Monday };
            var menuDto = new MenuDto { DayOfWeek = "Monday" };

            _menuDalMock.Setup(m => m.GetByAll(It.IsAny<Expression<Func<Menu, bool>>>()))
                  .ReturnsAsync(menu);
            _mapperMock.Setup(m => m.Map<MenuDto>(menu))
                .Returns(menuDto);

            // Act
            var result = await _menuManager.AddNewMenuAsync(createMenuDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Monday", result.DayOfWeek);
        }

        [TestMethod]
        public async Task DeleteMenuAsync_ShouldReturnTrue_WhenMenuExists()
        {
            // Arrange
            var menuId = 1;
            var menu = new Menu { Id = menuId, MenuCategories = new List<MenuCategory>() };

            _menuDalMock.Setup(m => m.GetByAll(It.IsAny<Expression<Func<Menu, bool>>>()))
                           .ReturnsAsync(menu);
            _menuDalMock.Setup(m => m.DeleteAsync(It.IsAny<Menu>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _menuManager.DeleteMenuAsync(menuId);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task DeleteMenuAsync_ShouldThrowException_WhenMenuDoesNotExist()
        {
            // Arrange
            var menuId = 1;

            _menuDalMock.Setup(m => m.GetByAll(It.IsAny<Expression<Func<Menu, bool>>>()))
                .ReturnsAsync((Menu)null);

            // Act & Assert
            var ex = await Assert.ThrowsExceptionAsync<Exception>(
                async () => await _menuManager.DeleteMenuAsync(menuId));
            Assert.AreEqual($"Menu with ID {menuId} does not exist.", ex.Message);
        }
    }
}
