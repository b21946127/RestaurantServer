using EntityLayer.Concrete;
using EntityLayer.DTOs.MenuDtos;
using EntityLayer.DTOs.MenuItemSetDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IMenuService
    {
        Task<MenuDto> GetMenuByDayAsync(string dayOfWeek);
        Task<MenuDto> AddNewMenuAsync(CreateMenuDto createMenuDto);
        Task<MenuDto> AddMenuItemSetsAsync(CreateMenuItemSetDto setDto);
        Task<MenuDto> UpdateMenuItemSetsAsync(UpdateMenuItemSetDto setDto);
        Task<MenuDto> UpdateMenuAsync(UpdateMenuDto updateMenuDto);
        Task<bool> DeleteMenuAsync(int menuId);
    }
}
