using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using RestaurantServer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class MenuItemManager : IMenuItemService
    {
        private readonly IMenuItemDal _menuItemDal;
        private readonly IMenuCategoryDal _menuCategoryDal;
        private readonly IMenuCategoryMenuItemDal _menuCategoryMenuItemDal;

        public MenuItemManager(IMenuItemDal menuItemDal, IMenuCategoryDal menuCategoryDal, IMenuCategoryMenuItemDal menuCategoryMenuItemDal)
        {
            _menuItemDal = menuItemDal;
            _menuCategoryDal = menuCategoryDal;
            _menuCategoryMenuItemDal = menuCategoryMenuItemDal;
        }

        public async Task CreateMenuItemAsync(CreateMenuItemDto createMenuItemDto)
        {
            var menuCategory = await _menuCategoryDal.GetAsync(x => x.Id == createMenuItemDto.MenuCategoryId);
            if (menuCategory == null)
            {
                throw new Exception("Menu category not found.");
            }

            var subItems = new List<MenuItem>();
            foreach (var menuItemId in createMenuItemDto.SubItems)
            {
                var subItem = await _menuItemDal.GetAsync(x => x.Id == menuItemId);
                if (subItem == null)
                {
                    throw new Exception($"SubItem with Id {menuItemId} not found.");
                }
                subItems.Add(subItem);
            }

            MenuItem newMenuItem = new MenuItem
            {
                Name = createMenuItemDto.Name,
                Price = createMenuItemDto.Price,
                Portion = createMenuItemDto.Portion,
                SubItems = subItems
            };

            await _menuItemDal.InsertAsync(newMenuItem);

            var menuCategoryItem = new MenuCategoryMenuItem
            {
                MenuCategoryId = menuCategory.Id,
                MenuItemId = newMenuItem.Id
            };

            await _menuCategoryMenuItemDal.InsertAsync(menuCategoryItem);
        }
    }
}
