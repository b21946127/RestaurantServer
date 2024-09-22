using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs;
using RestaurantServer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class MenuCategoryManager : IMenuCategoryService
    {
        private readonly IMenuCategoryDal _menuCategoryDal;
        private readonly IMenuCategoryMenuItemDal _menuCategoryMenuItemDal;
        private readonly IMenuItemDal _menuItemDal;
        private readonly IMenuDal _menuDal;

        public MenuCategoryManager(IMenuCategoryDal menuCategoryDal, IMenuCategoryMenuItemDal menuCategoryMenuItemDal, IMenuItemDal menuItemDal, IMenuDal menuDal)
        {
            _menuCategoryDal = menuCategoryDal;
            _menuCategoryMenuItemDal = menuCategoryMenuItemDal;
            _menuItemDal = menuItemDal;
            _menuDal = menuDal;
        }

        public async Task MenuCategoryAddAsync(CreateMenuCategoryDto menuCategory)
        {
            if (!Enum.TryParse(menuCategory.MenuDay, out DayOfWeekEnum menuDayEnum))
            {
                throw new ArgumentException("Invalid day of week", nameof(menuCategory.MenuDay));
            }

            var menu = await _menuDal.GetAsync(x => x.DayOfWeek == menuDayEnum);

            if (menu == null)
            {
                throw new Exception($"Menu not found for day: {menuDayEnum}");
            }

            var newMenuCategory = new MenuCategory
            {
                CategoryName = menuCategory.CategoryName,
                Menu = menu
            };

            await _menuCategoryDal.InsertAsync(newMenuCategory);
            menu.MenuCategories.Add(newMenuCategory);
            await _menuDal.UpdateAsync(menu);
        }

        public async Task MenuCategoryDeleteAsync(int id)
        {
            var menuCategory = await _menuCategoryDal.GetAsync(x => x.Id == id);
            if (menuCategory == null)
            {
                throw new ArgumentException("MenuCategory not found.", nameof(id));
            }
            await _menuCategoryDal.DeleteAsync(menuCategory);
        }

        public async Task MenuCategoryUpdateAsync(UpdateMenuCategoryDto menuCategory)
        {
            var updatedMenuCategory = await _menuCategoryDal.GetAsync(x => x.Id == menuCategory.Id);
            if (updatedMenuCategory == null)
            {
                throw new ArgumentException("MenuCategory not found.", nameof(menuCategory.Id));
            }
            updatedMenuCategory.CategoryName = menuCategory.CategoryName;
            await _menuCategoryDal.UpdateAsync(updatedMenuCategory);
        }

        public async Task MenuCategoryAddNewMenuItemAsync(int menuCategoryId, int menuItemId)
        {
            var menuCategory = await _menuCategoryDal.GetAsync(x => x.Id == menuCategoryId);
            var menuItem = await _menuItemDal.GetAsync(x => x.Id == menuItemId);

            if (menuCategory == null || menuItem == null)
            {
                throw new Exception("MenuCategory or MenuItem not found.");
            }

            var menuCategoryMenuItem = new MenuCategoryMenuItem
            {
                MenuCategoryId = menuCategoryId,
                MenuItemId = menuItemId
            };

            await _menuCategoryMenuItemDal.InsertAsync(menuCategoryMenuItem);
        }
    }
}
