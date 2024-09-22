using BusinessLayer.Abstract;
using EntityLayer.DTOs.MenuCategoryDtos;
using EntityLayer.DTOs.MenuDtos;
using EntityLayer.DTOs.MenuItemSetDtos;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestaurantServer.Controllers;

namespace RestaurantServer.Tests.Controllers
{
    [TestClass]
    public class MenuControllerTests
    {
        private Mock<IMenuService> _menuServiceMock;
        private MenuController _menuController;

        [TestInitialize]
        public void SetUp()
        {
            _menuServiceMock = new Mock<IMenuService>();
            _menuController = new MenuController(_menuServiceMock.Object);
        }

        [TestMethod]
        public async Task GetMenuByDay_ReturnsOk_WhenMenuExists()
        {
            // Arrange
            var dayOfWeek = "Monday";
            var menuDto = new MenuDto { DayOfWeek = dayOfWeek };
            _menuServiceMock.Setup(m => m.GetMenuByDayAsync(dayOfWeek)).ReturnsAsync(menuDto);

            // Act
            var result = await _menuController.GetMenuByDay(dayOfWeek);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(menuDto, okResult.Value);
        }

        [TestMethod]
        public async Task GetMenuByDay_ReturnsNotFound_WhenMenuDoesNotExist()
        {
            // Arrange
            var dayOfWeek = "Monday";
            _menuServiceMock.Setup(m => m.GetMenuByDayAsync(dayOfWeek)).ReturnsAsync((MenuDto)null);

            // Act
            var result = await _menuController.GetMenuByDay(dayOfWeek);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task AddNewMenu_ReturnsOk_WhenMenuIsAdded()
        {
            // Arrange
            var createMenuDto = new CreateMenuDto { DayOfWeek = "Monday" };
            var menuDto = new MenuDto { DayOfWeek = "Monday" };
            _menuServiceMock.Setup(m => m.AddNewMenuAsync(createMenuDto)).ReturnsAsync(menuDto);

            // Act
            var result = await _menuController.AddNewMenu(createMenuDto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(menuDto, okResult.Value);
        }

        [TestMethod]
        public async Task AddNewMenu_ReturnsBadRequest_WhenDtoIsNull()
        {
            // Arrange
            CreateMenuDto createMenuDto = null;

            // Act
            var result = await _menuController.AddNewMenu(createMenuDto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task AddMenuItemSet_ReturnsOk_WhenItemSetIsAdded()
        {
            // Arrange
            var createMenuItemSetDto = new CreateMenuItemSetDto();
            var menuDto = new MenuDto();
            _menuServiceMock.Setup(m => m.AddMenuItemSetsAsync(createMenuItemSetDto)).ReturnsAsync(menuDto);

            // Act
            var result = await _menuController.AddMenuItemSet(createMenuItemSetDto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(menuDto, okResult.Value);
        }

        [TestMethod]
        public async Task UpdateMenuItemSet_ReturnsOk_WhenItemSetIsUpdated()
        {
            // Arrange
            var updateMenuItemSetDto = new UpdateMenuItemSetDto
            {
                Id = 1,
                MenuCategoryId = 1,
                Name = "Test",
            };

            var updatedMenuDto = new MenuDto
            {
                 Id = 1,
                DayOfWeek = "Monday",
                MenuCategories = new List<MenuCategoryDto>
                {
                    new MenuCategoryDto
                    {
                        Id = 1,
                        CategoryName = "Test",
                        MenuItemSets = new List<MenuItemSetDto>
                        {
                            new MenuItemSetDto
                            {
                                Id = 1,
                                Name = "Test",
                            }
                        }
                    }
                }

            };

            _menuServiceMock.Setup(m => m.UpdateMenuItemSetsAsync(updateMenuItemSetDto)).ReturnsAsync(updatedMenuDto);

            // Act
            var result = await _menuController.UpdateMenuItemSet(updateMenuItemSetDto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult), "Expected OkObjectResult.");
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult, "Result should not be null.");
            Assert.AreEqual(updatedMenuDto, okResult.Value, "Returned menuDto does not match the expected value.");
        }

        [TestMethod]
        public async Task UpdateMenuItemSet_ReturnsBadRequest_WhenDtoIsNull()
        {
            // Act
            var result = await _menuController.UpdateMenuItemSet(null);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult, "Expected BadRequestObjectResult.");
            Assert.AreEqual("Menu data is required.", badRequestResult.Value, "Error message does not match.");
        }


        [TestMethod]
        public async Task AddMenuItemSet_ReturnsBadRequest_WhenDtoIsNull()
        {
            // Arrange
            CreateMenuItemSetDto createMenuItemSetDto = null;

            // Act
            var result = await _menuController.AddMenuItemSet(createMenuItemSetDto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task UpdateMenu_ReturnsOk_WhenMenuIsUpdated()
        {
            // Arrange
            var updateMenuDto = new UpdateMenuDto();
            var updatedMenuDto = new MenuDto();
            _menuServiceMock.Setup(m => m.UpdateMenuAsync(updateMenuDto)).ReturnsAsync(updatedMenuDto);

            // Act
            var result = await _menuController.UpdateMenu(updateMenuDto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(updatedMenuDto, okResult.Value);
        }

        [TestMethod]
        public async Task UpdateMenu_ReturnsBadRequest_WhenDtoIsNull()
        {
            // Arrange
            UpdateMenuDto updateMenuDto = null;

            // Act
            var result = await _menuController.UpdateMenu(updateMenuDto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task DeleteMenu_ReturnsNoContent_WhenMenuIsDeleted()
        {
            // Arrange
            var menuId = 1;
            _menuServiceMock.Setup(m => m.DeleteMenuAsync(menuId)).ReturnsAsync(true);

            // Act
            var result = await _menuController.DeleteMenu(menuId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DeleteMenu_ReturnsNotFound_WhenMenuDoesNotExist()
        {
            // Arrange
            var menuId = 1;
            _menuServiceMock.Setup(m => m.DeleteMenuAsync(menuId)).ReturnsAsync(false);

            // Act
            var result = await _menuController.DeleteMenu(menuId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }
    }
}
