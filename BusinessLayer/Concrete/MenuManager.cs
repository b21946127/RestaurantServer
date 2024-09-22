using AutoMapper;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs.MenuDtos;
using EntityLayer.DTOs.MenuItemDtos;
using EntityLayer.DTOs.MenuItemSetDtos;

public class MenuManager : IMenuService
{
    readonly IMenuDal _menuDal;
    readonly IMenuCategoryDal _menuCategoryDal;
    readonly IMenuItemDal _menuItemDal;
    readonly IMenuItemSetDal _menuItemSetDal;
    readonly IIntegrationDal _integrationDal;
    readonly IMenuItemOptionDal _optionDal;
    readonly IMenuItemMenuItemSetDal _menuItemMenuItemSetDal;
    readonly IMapper _mapper;

    public MenuManager(IMenuDal menuDal, IMenuCategoryDal menuCategoryDal, IMenuItemDal menuItemDal, IMenuItemSetDal menuItemSetDal, IIntegrationDal integrationDal, IMenuItemOptionDal optionDal, IMenuItemMenuItemSetDal menuItemMenuItemSetDal, IMapper mapper)
    {
        _menuDal = menuDal;
        _menuCategoryDal = menuCategoryDal;
        _menuItemDal = menuItemDal;
        _menuItemSetDal = menuItemSetDal;
        _integrationDal = integrationDal;
        _optionDal = optionDal;
        _menuItemMenuItemSetDal = menuItemMenuItemSetDal;
        _mapper = mapper;

    }

    public async Task<MenuDto> GetMenuByDayAsync(string dayOfWeek)
    {
        if (Enum.TryParse<DayOfWeekEnum>(dayOfWeek, true, out var dayEnum))
        {
            var menu = await _menuDal.GetByAll(m => m.DayOfWeek == dayEnum);
            if (menu == null)
            {
                throw new Exception($"No menu found for the day: {dayOfWeek}");
            }
            return _mapper.Map<MenuDto>(menu);
        }
        throw new ArgumentException($"Invalid day of the week: {dayOfWeek}");
    }

    public async Task<MenuDto> AddNewMenuAsync(CreateMenuDto createMenuDto)
    {
        if (!Enum.TryParse<DayOfWeekEnum>(createMenuDto.DayOfWeek, true, out var dayEnum))
        {
            throw new ArgumentException($"Invalid day of the week: {createMenuDto.DayOfWeek}");
        }

        var menu = await _menuDal.GetByAll(m => m.DayOfWeek == dayEnum);
        if (menu == null)
        {
            throw new Exception($"No menu found for the day: {createMenuDto.DayOfWeek}");
        }

        menu.MenuCategories = new List<MenuCategory>();

        foreach (var categoryDto in createMenuDto.MenuCategories)
        {
            var category = new MenuCategory
            {
                CategoryName = categoryDto.CategoryName,
                MenuItems = new List<MenuItem>(),
                MenuItemSets = new List<MenuItemSet>()
            };

            foreach (var itemDto in categoryDto.MenuItems)
            {
                var menuItem = new MenuItem
                {
                    Name = itemDto.Name,
                    Portion = itemDto.Portion,
                    Price = itemDto.Price,
                    Integrations = itemDto.Integrations.Select(i => new Integration
                    {
                        Name = i.Name,
                        IsAllergen = i.IsAllergen
                    }).ToList(),
                    Options = itemDto.Options.Select(o => new MenuItemOption
                    {
                        Name = o.Name,
                        Color = o.Color
                    }).ToList(),
                    MenuItemMenuItemSets = new List<MenuItemMenuItemSet>()
                };

                category.MenuItems.Add(menuItem);
            }

            menu.MenuCategories.Add(category);
        }

        await _menuDal.UpdateAsync(menu);
        return _mapper.Map<MenuDto>(menu);
    }


    public async Task<MenuDto> AddMenuItemSetsAsync(CreateMenuItemSetDto setDto)
    {
        var menuCategory = await _menuCategoryDal.GetByAllAsync(mc => mc.Id == setDto.MenuCategoryId);
        if (menuCategory == null)
        {
            throw new Exception($"MenuCategory with ID {setDto.MenuCategoryId} does not exist.");
        }


        MenuItemSet menuItemSet = new MenuItemSet
        {
            Name = setDto.Name,
            MenuCategory = menuCategory,
            MenuItemMenuItemSets = new List<MenuItemMenuItemSet>()
        };

        await _menuItemSetDal.InsertAsync(menuItemSet);

        foreach (int itemId in setDto.MenuItemIds)
        {
            MenuItem existingMenuItem = await _menuItemDal.GetByAll(mi => mi.Id == itemId);
            if (existingMenuItem != null)
            {
                MenuItemMenuItemSet menuItemMenuItemSet = new MenuItemMenuItemSet
                {
                    MenuItem = existingMenuItem,
                    MenuItemId = existingMenuItem.Id,
                    MenuItemSet = menuItemSet,
                    MenuItemSetId = menuItemSet.Id
                };
                menuItemSet.MenuItemMenuItemSets.Add(menuItemMenuItemSet);

                if (existingMenuItem.MenuItemMenuItemSets == null)
                {
                    existingMenuItem.MenuItemMenuItemSets = new List<MenuItemMenuItemSet>();
                }
                existingMenuItem.MenuItemMenuItemSets.Add(menuItemMenuItemSet);

                await _menuItemMenuItemSetDal.InsertAsync(menuItemMenuItemSet);
            }
            else
            {
                throw new Exception($"MenuItem with ID {itemId} does not exist.");
            }
        }
        if (menuCategory.MenuItemSets == null)
        {
            menuCategory.MenuItemSets = new List<MenuItemSet>();
        }

        menuCategory.MenuItemSets.Add(menuItemSet);
        await _menuCategoryDal.UpdateAsync(menuCategory);

        if (menuCategory.Menu == null)
        {
            throw new Exception($"Menu for MenuCategory with ID {setDto.MenuCategoryId} does not exist.");
        }

        if (Enum.TryParse<DayOfWeekEnum>(menuCategory.Menu.DayOfWeek.ToString(), true, out var dayEnum))
        {
            var menu = await _menuDal.GetByAll(m => m.DayOfWeek == dayEnum);
            if (menu == null)
            {
                throw new Exception($"No menu found for the day: {menuCategory.Menu.DayOfWeek}");
            }

            return _mapper.Map<MenuDto>(menu);
        }
        else
        {
            throw new ArgumentException($"Invalid day of the week: {menuCategory.Menu.DayOfWeek}");
        }
    }

    public async Task<MenuDto> UpdateMenuItemSetsAsync(UpdateMenuItemSetDto setDto)
    {
        var menuCategory = await _menuCategoryDal.GetByAllAsync(mc => mc.Id == setDto.MenuCategoryId);
        if (menuCategory == null)
        {
            throw new Exception($"MenuCategory with ID {setDto.MenuCategoryId} does not exist.");
        }

        MenuItemSet existingMenuItemSet = await _menuItemSetDal.GetByAllAsync(ms => ms.Id == setDto.Id);
        MenuItemSet menuItemSet;

        existingMenuItemSet.Name = setDto.Name;
        menuItemSet = existingMenuItemSet;

        menuItemSet.MenuItemMenuItemSets.Clear();

        foreach (int itemId in setDto.MenuItemIds)
        {
            MenuItem existingMenuItem = await _menuItemDal.GetByAll(mi => mi.Id == itemId);
            if (existingMenuItem != null)
            {
                MenuItemMenuItemSet menuItemMenuItemSet = new MenuItemMenuItemSet
                {
                    MenuItem = existingMenuItem,
                    MenuItemId = existingMenuItem.Id,
                    MenuItemSet = menuItemSet,
                    MenuItemSetId = menuItemSet.Id
                };
                menuItemSet.MenuItemMenuItemSets.Add(menuItemMenuItemSet);

                if (existingMenuItem.MenuItemMenuItemSets == null)
                {
                    existingMenuItem.MenuItemMenuItemSets = new List<MenuItemMenuItemSet>();
                }
                existingMenuItem.MenuItemMenuItemSets.Add(menuItemMenuItemSet);

                await _menuItemMenuItemSetDal.InsertAsync(menuItemMenuItemSet);
            }
            else
            {
                throw new Exception($"MenuItem with ID {itemId} does not exist.");
            }
        }

        if (menuCategory.MenuItemSets == null)
        {
            menuCategory.MenuItemSets = new List<MenuItemSet>();
        }

        menuCategory.MenuItemSets.Add(menuItemSet);
        await _menuCategoryDal.UpdateAsync(menuCategory);

        if (Enum.TryParse<DayOfWeekEnum>(menuCategory.Menu.DayOfWeek.ToString(), true, out var dayEnum))
        {
            var menu = await _menuDal.GetByAll(m => m.DayOfWeek == dayEnum);
            if (menu == null)
            {
                throw new Exception($"No menu found for the day: {menuCategory.Menu.DayOfWeek}");
            }

            return _mapper.Map<MenuDto>(menu);
        }
        else
        {
            throw new ArgumentException($"Invalid day of the week: {menuCategory.Menu.DayOfWeek}");
        }
    }



    public async Task<MenuDto> UpdateMenuAsync(UpdateMenuDto updateMenuDto)
    {
        if (!Enum.TryParse<DayOfWeekEnum>(updateMenuDto.DayOfWeek, true, out var dayOfWeek))
        {
            throw new Exception($"Invalid day of week: {updateMenuDto.DayOfWeek}");
        }

        Menu menu = await _menuDal.GetByAll(m => m.DayOfWeek == dayOfWeek);

        if (menu == null)
        {
            throw new Exception($"No menu found for the day: {updateMenuDto.DayOfWeek}");

        }

        foreach (var categoryDto in updateMenuDto.MenuCategories)
        {
            MenuCategory category = menu.MenuCategories.FirstOrDefault(c => c.Id == categoryDto.Id);

            if (category != null)
            {
                category.CategoryName = categoryDto.CategoryName;
            }
            else
            {
                category = new MenuCategory
                {
                    CategoryName = categoryDto.CategoryName,
                    MenuItems = new List<MenuItem>(),
                    MenuItemSets = new List<MenuItemSet>()
                };
                menu.MenuCategories.Add(category);
            }

            foreach (MenuItemDto itemDto in categoryDto.MenuItems)
            {
                MenuItem menuItem = category.MenuItems.FirstOrDefault(i => i.Id == itemDto.Id);

                if (menuItem != null)
                {
                    menuItem.Name = itemDto.Name;
                    menuItem.Portion = itemDto.Portion;
                    menuItem.Price = itemDto.Price;

                    menuItem.Options.Clear();

                    if (itemDto.Options != null)
                    {
                        foreach (var optionDto in itemDto.Options)
                        {
                            var existingOption = menuItem.Options.FirstOrDefault(o => o.Name == optionDto.Name);
                            if (existingOption != null)
                            {
                                existingOption.Color = optionDto.Color;
                            }
                            else
                            {
                                menuItem.Options.Add(new MenuItemOption
                                {
                                    Name = optionDto.Name,
                                    Color = optionDto.Color
                                });
                            }
                        }
                    }

                    menuItem.Integrations.Clear();
                    foreach (var integrationDto in itemDto.Integrations)
                    {
                        Integration integration = await _integrationDal.GetAsync(i => i.Id == integrationDto.Id);
                        if (integration != null)
                        {
                            menuItem.Integrations.Add(integration);
                        }
                        else
                        {
                            Integration integration1 = new Integration
                            {
                                Name = integration.Name,
                                IsAllergen = integration.IsAllergen
                            };

                            menuItem.Integrations.Add(integration1);
                        }
                    }
                }
                else
                {
                    menuItem = new MenuItem
                    {
                        Name = itemDto.Name,
                        Portion = itemDto.Portion,
                        Price = itemDto.Price,
                        Integrations = itemDto.Integrations.Select(i => new Integration
                        {
                            Name = i.Name,
                            IsAllergen = i.IsAllergen
                        }).ToList(),
                        Options = itemDto.Options.Select(o => new MenuItemOption
                        {
                            Name = o.Name,
                            Color = o.Color
                        }
                          ).ToList()
                    };

                    category.MenuItems.Add(menuItem);
                }
            }
        }

        await _menuDal.UpdateAsync(menu);
        return _mapper.Map<MenuDto>(menu);
    }



    public async Task<bool> DeleteMenuAsync(int menuId)
    {
        Menu menu = await _menuDal.GetByAll(m => m.Id == menuId);
        if (menu == null)
        {
            throw new Exception($"Menu with ID {menuId} does not exist.");
        }

        foreach (MenuCategory category in menu.MenuCategories)
        {
            foreach (MenuItem menuItem in category.MenuItems)
            {
                foreach (MenuItemOption option in menuItem.Options)
                {
                    await _optionDal.DeleteAsync(option);
                }
                foreach (Integration integration in menuItem.Integrations)
                {
                    await _integrationDal.DeleteAsync(integration);
                }
                await _menuItemDal.DeleteAsync(menuItem);
            }

            await _menuCategoryDal.DeleteAsync(category);
        }

        await _menuDal.DeleteAsync(menu);
        return true;
    }

}
