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
    public interface IMenuItemService
    {
        Task CreateMenuItemAsync(CreateMenuItemDto createMenuItemDto);
    }
}
