using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs.MenuCategoryDtos;
using EntityLayer.DTOs.MenuDtos;
using EntityLayer.DTOs.MenuItemDtos;
using EntityLayer.DTOs.MenuItemSetDtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RestaurantServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet("GetMenuByDay/{dayOfWeek}", Name = "GetMenuByDay")]
        public async Task<IActionResult> GetMenuByDay(string dayOfWeek)
        {
            var menu = await _menuService.GetMenuByDayAsync(dayOfWeek);

            if (menu == null)
            {
                return NotFound();
            }

            return Ok(menu);
        }

        [HttpPost("AddNewMenu",Name="AddNewMenu")]
        public async Task<IActionResult> AddNewMenu([FromBody] CreateMenuDto createMenuDto)
        {
            if (createMenuDto == null)
            {
                return BadRequest("Menu data is required.");
            }

        

            var menu = await _menuService.AddNewMenuAsync(createMenuDto);
            return Ok(menu);
        }

        [HttpPost("AddMenuItemSet",Name ="AddMenuItemSet")]
        public async Task<IActionResult> AddMenuItemSet([FromBody] CreateMenuItemSetDto createMenuItemSet)
        {
            if(createMenuItemSet == null)
            {
                return BadRequest("Menu Item Set Data is required.");
            }

            MenuDto menu = await _menuService.AddOrUpdateMenuItemSetsAsync(createMenuItemSet);
            return Ok(menu);
        }

        [HttpPut("UpdateMenu", Name = "UpdateMenu")]
        public async Task<IActionResult> UpdateMenu( [FromBody] UpdateMenuDto updateMenuDto)
        {
            if (updateMenuDto == null)
            {
                return BadRequest("Invalid menu data.");
            }

            var updatedMenu = await _menuService.UpdateMenuAsync(updateMenuDto);
            return Ok(updatedMenu);
        }

        [HttpDelete("DeleteMenu/{id}", Name ="DeleteMenu")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var result = await _menuService.DeleteMenuAsync(id);

            if (!result)
            {
                return NotFound($"Menu with ID {id} does not exist.");
            }

            return NoContent();
        }
    }
}
