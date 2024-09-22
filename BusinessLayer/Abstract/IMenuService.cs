using EntityLayer.Concrete;
using EntityLayer.DTOs;
using RestaurantServer.DTOs;
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
        Task<MenuDto> CreateNewMenuDayWithCategoriesAsync(CreateMenuDto createMenuDto);
        Task<MenuDto> UpdateNewMenuDayWithCategoriesAsync(UpdateMenuDto updateMenuDto);
        Task<bool> DeleteMenuAsync(int menuId);
    }
}
