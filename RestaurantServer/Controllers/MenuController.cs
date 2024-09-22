using BusinessLayer.Abstract;
using EntityLayer.DTOs;
using Microsoft.AspNetCore.Mvc;
using RestaurantServer.DTOs;
using System.Threading.Tasks;

namespace RestaurantServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly IMenuCategoryService _menuCategoryService;
        private readonly IMenuItemService _menuItemService;

        public MenuController(IMenuService menuService, IMenuCategoryService menuCategoryService, IMenuItemService menuItemService)
        {
            _menuService = menuService;
            _menuCategoryService = menuCategoryService;
            _menuItemService = menuItemService;
        }

        [HttpGet("{dayOfWeek}", Name = "GetMenuByDay")]
        public async Task<IActionResult> GetMenuByDay(string dayOfWeek)
        {
            var menu = await _menuService.GetMenuByDayAsync(dayOfWeek);

            if (menu == null)
            {
                return NotFound();
            }

            return Ok(menu);
        }

        [HttpPost("CreateMenuCategory", Name = "CreateMenuCategory")]
        public async Task<IActionResult> CreateMenuCategory([FromBody] CreateMenuCategoryDto createMenuCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _menuCategoryService.MenuCategoryAddAsync(createMenuCategoryDto);

            return CreatedAtRoute("CreateMenuCategory", new { id = createMenuCategoryDto.CategoryName }, "Menu category created successfully.");
        }

        [HttpPost("CreateMenuItem", Name = "CreateMenuItem")]
        public async Task<IActionResult> CreateMenuItem([FromBody] CreateMenuItemDto createMenuItemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _menuItemService.CreateMenuItemAsync(createMenuItemDto);

            return CreatedAtRoute("CreateMenuItem", new { id = createMenuItemDto.Name }, "Menu item created successfully.");
        }

        [HttpPost("CreateMenu", Name = "CreateMenu")]
        public async Task<IActionResult> CreateMenu([FromBody] CreateMenuDto createMenuDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var menuDto = await _menuService.CreateNewMenuDayWithCategoriesAsync(createMenuDto);

            return CreatedAtRoute("GetMenuByDay", new { dayOfWeek = createMenuDto.DayOfWeek }, $"Menu for {createMenuDto.DayOfWeek} created successfully.");
        }

        [HttpPut("UpdateMenu", Name = "UpdateMenu")]
        public async Task<IActionResult> UpdateMenu([FromBody] UpdateMenuDto updateMenuDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _menuService.UpdateNewMenuDayWithCategoriesAsync(updateMenuDto);

            if (updated == null)
            {
                return NotFound($"Menu for {updateMenuDto.DayOfWeek} not found.");
            }

            return Ok($"Menu for {updateMenuDto.DayOfWeek} updated successfully.");
        }
    }
}
