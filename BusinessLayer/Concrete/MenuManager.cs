using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs;
using RestaurantServer.DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class MenuManager : IMenuService
    {
        readonly IMenuDal _menuDal;
        readonly IMenuCategoryDal _menuCategoryDal;

        public MenuManager(IMenuDal menuDal,IMenuCategoryDal menuCategoryDal)
        {
            _menuDal = menuDal;
            _menuCategoryDal = menuCategoryDal;
        }

        public async Task<MenuDto> GetMenuByDayAsync(string dayOfWeek)
        {
            if (Enum.TryParse<DayOfWeekEnum>(dayOfWeek, true, out var dayEnum))
            {
                Menu menu = await _menuDal.GetWithCategoriesAsync(menu => menu.DayOfWeek == dayEnum);

                if (menu == null)
                {
                    throw new Exception($"No menu found for the day: {dayOfWeek}");
                }

                var menuDto = new MenuDto
                {
                    DayOfWeek = menu.DayOfWeek.ToString(),
                    MenuCategories = menu.MenuCategories.Select(mc => new MenuCategoryDto
                    {
                        Id = mc.Id,
                        CategoryName = mc.CategoryName,
                        MenuItems = mc.MenuCategoryItems.Select(mci => new MenuItemDto
                        {
                            Id = mci.MenuItem.Id,
                            Name = mci.MenuItem.Name,
                            Price = mci.MenuItem.Price,
                            Portion = mci.MenuItem.Portion,
                            Options = mci.MenuItem.Options.Select(o => new MenuItemOptionDto
                            {
                                Id = o.Id,
                                Name = o.Name,
                                Color = o.Color,
                            }).ToList()
                        }).ToList()
                    }).ToList()
                };

                return menuDto;
            }
            else
            {
                throw new ArgumentException($"Invalid day of the week: {dayOfWeek}");
            }
        }


        public async Task<MenuDto> CreateNewMenuDayWithCategoriesAsync(CreateMenuDto createMenuDto)
        {
            Menu menu = null;

            if (Enum.TryParse<DayOfWeekEnum>(createMenuDto.DayOfWeek, true, out var dayEnum))
            {
                try { 
                menu = new Menu
                {
                    DayOfWeek = dayEnum
                };
                }
                catch (Exception ex)
                {
                   throw new Exception($"There is already a menu for: {dayEnum}");
                }
            }


            menu.MenuCategories = createMenuDto.MenuCategories.Select(mc => _menuCategoryDal.Get(x => x.Id == mc.Id)).ToList();

            await _menuDal.InsertAsync(menu);

            var menuDto = new MenuDto
            {
                DayOfWeek = menu.DayOfWeek.ToString(),
                MenuCategories = menu.MenuCategories.Select(mc => new MenuCategoryDto
                {
                    Id = mc.Id,
                    CategoryName = mc.CategoryName,
                    MenuItems = mc.MenuCategoryItems.Select(mci => new MenuItemDto
                    {
                        Id = mci.MenuItem.Id,
                        Name = mci.MenuItem.Name,
                        Price = mci.MenuItem.Price,
                        Portion = mci.MenuItem.Portion,
                        Options = mci.MenuItem.Options.Select(o => new MenuItemOptionDto
                        {
                            Id = o.Id,
                            Name = o.Name,
                            Color = o.Color,
                        }).ToList()
                    }).ToList()
                }).ToList()
            };

            return menuDto;
        }

        public async Task<MenuDto> UpdateNewMenuDayWithCategoriesAsync(UpdateMenuDto updateMenuDto)
        {

            if (Enum.TryParse<DayOfWeekEnum>(updateMenuDto.DayOfWeek, true, out var dayEnum))
            {
                Menu menu = await _menuDal.GetWithCategoriesAsync(menu => menu.DayOfWeek == dayEnum);


                if (menu == null)
                {
                    throw new Exception($"No menu found for the day: {dayEnum}");
                }


                menu.MenuCategories = updateMenuDto.MenuCategories.Select(mc => _menuCategoryDal.Get(x => x.Id == mc.Id)).ToList();

                await _menuDal.InsertAsync(menu);

                var menuDto = new MenuDto
                {
                    DayOfWeek = menu.DayOfWeek.ToString(),
                    MenuCategories = menu.MenuCategories.Select(mc => new MenuCategoryDto
                    {
                        Id = mc.Id,
                        CategoryName = mc.CategoryName,
                        MenuItems = mc.MenuCategoryItems.Select(mci => new MenuItemDto
                        {
                            Id = mci.MenuItem.Id,
                            Name = mci.MenuItem.Name,
                            Price = mci.MenuItem.Price,
                            Portion = mci.MenuItem.Portion,
                            Options = mci.MenuItem.Options.Select(o => new MenuItemOptionDto
                            {
                                Id = o.Id,
                                Name = o.Name,
                                Color = o.Color,
                            }).ToList()
                        }).ToList()
                    }).ToList()
                };

                return menuDto;

            }
            else
            {
                throw new ArgumentException($"Invalid day of the week: {updateMenuDto.DayOfWeek}");
            }
        }

        public async Task<bool> DeleteMenuAsync(int menuId)
        {
            var menu = await _menuDal.GetAsync(m => m.DayOfWeek == (DayOfWeekEnum)menuId);
            if (menu == null)
            {
                return false;
            }

            await _menuDal.DeleteAsync(menu);
            return true;
        }
    }
}
