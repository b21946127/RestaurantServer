using EntityLayer.DTOs;
using RestaurantServer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IMenuCategoryService
    {
        Task MenuCategoryAddAsync(CreateMenuCategoryDto menuCategory);
        Task MenuCategoryDeleteAsync(int id);
        Task MenuCategoryUpdateAsync(UpdateMenuCategoryDto menuCategory);
        Task MenuCategoryAddNewMenuItemAsync(int menuCategoryId, int menuItemId);
    }
}
