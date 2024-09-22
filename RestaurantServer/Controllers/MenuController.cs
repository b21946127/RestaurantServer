using BusinessLayer.Abstract;
using EntityLayer.DTOs.MenuDtos;
using EntityLayer.DTOs.MenuItemSetDtos;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("AddNewMenu", Name = "AddNewMenu")]
        public async Task<IActionResult> AddNewMenu([FromBody] CreateMenuDto createMenuDto)
        {
            if (createMenuDto == null)
            {
                return BadRequest("Menu data is required.");
            }



            var menu = await _menuService.AddNewMenuAsync(createMenuDto);
            return Ok(menu);
        }

        [HttpPost("AddMenuItemSet", Name = "AddMenuItemSet")]
        public async Task<IActionResult> AddMenuItemSet([FromBody] CreateMenuItemSetDto createMenuItemSetDto)
        {
            if (createMenuItemSetDto == null)
            {
                return BadRequest("Menu Item Set Data is required.");
            }

            MenuDto menu = await _menuService.AddMenuItemSetsAsync(createMenuItemSetDto);
            return Ok(menu);
        }

        [HttpPut("UpdateMenu", Name = "UpdateMenu")]
        public async Task<IActionResult> UpdateMenu([FromBody] UpdateMenuDto updateMenuDto)
        {
            if (updateMenuDto == null)
            {
                return BadRequest("Invalid menu data.");
            }

            var updatedMenu = await _menuService.UpdateMenuAsync(updateMenuDto);
            return Ok(updatedMenu);
        }

        [HttpPut("UpdateMenuItemSet", Name = "UpdateMenuItemSet")]
        public async Task<IActionResult> UpdateMenuItemSet([FromBody] UpdateMenuItemSetDto updateMenuItemSet)
        {
            if (updateMenuItemSet == null)
            {
                return BadRequest("Menu data is required.");
            }



            var menu = await _menuService.UpdateMenuItemSetsAsync(updateMenuItemSet);
            return Ok(menu);
        }

        [HttpDelete("DeleteMenu/{id}", Name = "DeleteMenu")]
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
