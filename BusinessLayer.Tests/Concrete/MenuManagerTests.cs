using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoMapper;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs.MenuDtos;
using EntityLayer.DTOs.MenuItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityLayer.DTOs.MenuCategoryDtos;
using EntityLayer.DTOs.MenuItemSetDtos;
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
        private IMapper _mapper;
        private MenuManager _menuManager;

        [TestInitialize]
        public void Setup()
        {
            _menuDalMock = new Mock<IMenuDal>();
            _menuCategoryDalMock = new Mock<IMenuCategoryDal>();
            _menuItemDalMock = new Mock<IMenuItemDal>();
            _menuItemSetDalMock = new Mock<IMenuItemSetDal>();
            _integrationDalMock = new Mock<IIntegrationDal>();
            _optionDalMock = new Mock<IMenuItemOptionDal>();
            _menuItemMenuItemSetDalMock = new Mock<IMenuItemMenuItemSetDal>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Menu, MenuDto>();
                // Add other mappings here as needed
            });
            _mapper = config.CreateMapper();

            _menuManager = new MenuManager(
                _menuDalMock.Object,
                _menuCategoryDalMock.Object,
                _menuItemDalMock.Object,
                _menuItemSetDalMock.Object,
                _integrationDalMock.Object,
                _optionDalMock.Object,
                _menuItemMenuItemSetDalMock.Object,
                _mapper);
        }

        [TestMethod]
        public async Task GetMenuByDayAsync_ValidDay_ReturnsMenuDto()
        {
            var dayOfWeek = "Monday";
            var menu = new Menu { DayOfWeek = DayOfWeekEnum.Monday };

            _menuDalMock.Setup(m => m.GetByAll(It.IsAny<Expression<Func<Menu, bool>>>()))
                .ReturnsAsync(menu);

            var result = await _menuManager.GetMenuByDayAsync(dayOfWeek);

            Assert.IsNotNull(result);
            Assert.AreEqual(DayOfWeekEnum.Monday, Enum.Parse<DayOfWeekEnum>(result.DayOfWeek));

        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "No menu found for the day: Friday")]
        public async Task GetMenuByDayAsync_NoMenuFound_ThrowsException()
        {
            var dayOfWeek = "Friday";
            _menuDalMock.Setup(m => m.GetByAll(It.IsAny<Expression<Func<Menu, bool>>>()))
                .ReturnsAsync((Menu)null);

            await _menuManager.GetMenuByDayAsync(dayOfWeek);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid day of the week: InvalidDay")]
        public async Task GetMenuByDayAsync_InvalidDay_ThrowsArgumentException()
        {
            var dayOfWeek = "InvalidDay";
            await _menuManager.GetMenuByDayAsync(dayOfWeek);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid day of the week: InvalidDay")]
        public async Task AddNewMenuAsync_InvalidDay_ThrowsArgumentException()
        {
            var createMenuDto = new CreateMenuDto { DayOfWeek = "InvalidDay" };
            await _menuManager.AddNewMenuAsync(createMenuDto);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "No menu found for the day: Monday")]
        public async Task AddNewMenuAsync_NoMenuFound_ThrowsException()
        {
            var createMenuDto = new CreateMenuDto
            {
                DayOfWeek = "Monday",
                MenuCategories = new List<CreateMenuCategoryDto>()
            };

            _menuDalMock.Setup(m => m.GetByAll(It.IsAny<Expression<Func<Menu, bool>>>()))
                .ReturnsAsync((Menu)null);

            await _menuManager.AddNewMenuAsync(createMenuDto);
        }


        [TestMethod]
        [ExpectedException(typeof(Exception), "MenuCategory with ID 999 does not exist.")]
        public async Task AddMenuItemSetsAsync_MenuCategoryNotFound_ThrowsException()
        {
            var setDto = new CreateMenuItemSetDto { MenuCategoryId = 999 };

            _menuCategoryDalMock.Setup(m => m.GetByAllAsync(It.IsAny<Expression<Func<MenuCategory, bool>>>()))
                .ReturnsAsync((MenuCategory)null);

            await _menuManager.AddMenuItemSetsAsync(setDto);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "MenuItem with ID 999 does not exist.")]
        public async Task AddMenuItemSetsAsync_MenuItemNotFound_ThrowsException()
        {
            var setDto = new CreateMenuItemSetDto
            {
                MenuCategoryId = 1,
                MenuItemIds = new List<int> { 999 }
            };

            var menuCategory = new MenuCategory { Id = 1, Menu = new Menu { DayOfWeek = DayOfWeekEnum.Monday } };
            _menuCategoryDalMock.Setup(m => m.GetByAllAsync(mc => mc.Id == setDto.MenuCategoryId)).ReturnsAsync(menuCategory);
            _menuItemDalMock.Setup(m => m.GetByAll(It.IsAny<Expression<Func<MenuItem, bool>>>()))
                .ReturnsAsync((MenuItem)null);


            await _menuManager.AddMenuItemSetsAsync(setDto);
        }


        [TestMethod]
        [ExpectedException(typeof(Exception), "MenuCategory with ID 999 does not exist.")]
        public async Task UpdateMenuItemSetsAsync_MenuCategoryNotFound_ThrowsException()
        {
            var setDto = new UpdateMenuItemSetDto { MenuCategoryId = 999 };

            _menuCategoryDalMock.Setup(m => m.GetByAllAsync(It.IsAny<Expression<Func<MenuCategory, bool>>>()))
                .ReturnsAsync((MenuCategory)null);

            await _menuManager.UpdateMenuItemSetsAsync(setDto);
        }

        
    }
}