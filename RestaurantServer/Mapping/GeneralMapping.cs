using AutoMapper;
using EntityLayer.Concrete;
using EntityLayer.DTOs.CustomerDtos;
using EntityLayer.DTOs.IntegrationDtos;
using EntityLayer.DTOs.MenuCategoryDtos;
using EntityLayer.DTOs.MenuDtos;
using EntityLayer.DTOs.MenuItemDtos;
using EntityLayer.DTOs.MenuItemMenuItemSetDtoS;
using EntityLayer.DTOs.MenuItemSetDtos;
using EntityLayer.DTOs.MenuOptionDtos;
using EntityLayer.DTOs.OrderDtos;
using EntityLayer.DTOs.OrderItemDtos;

namespace RestaurantServer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Integration mappings
            CreateMap<IntegrationDto, Integration>().ReverseMap();

            // MenuCategory mappings
            CreateMap<MenuCategoryDto, MenuCategory>().ReverseMap();
            CreateMap<CreateMenuCategoryDto, MenuCategory>();
            CreateMap<UpdateMenuCategoryDto, MenuCategory>();

            // Menu mappings
            CreateMap<MenuDto, Menu>().ReverseMap();
            CreateMap<CreateMenuDto, Menu>();
            CreateMap<UpdateMenuDto, Menu>();

            // MenuItem mappings
            CreateMap<MenuItemDto, MenuItem>().ReverseMap();
            CreateMap<CreateMenuItemDto, MenuItem>();
            CreateMap<UpdateMenuItemDto, MenuItem>();


            // MenuItemSet mappings
            CreateMap<MenuItemSetDto, MenuItemSet>().ReverseMap();
            CreateMap<CreateMenuItemSetDto, MenuItemSet>();


            // MenuItemOption mappings
            CreateMap<MenuItemOptionDto, MenuItemOption>().ReverseMap();

            // Order mappings
            CreateMap<OrderDto, Order>().ReverseMap();
            CreateMap<CreateOrderDto, Order>();

            // OrderItem mappings
            CreateMap<OrderItemDto, OrderItem>().ReverseMap();
            CreateMap<CreateOrderItemDto, OrderItem>();


            // Mapping for the join entity
            CreateMap<MenuItemMenuItemSet, MenuItemMenuItemSetDto>()
                .ForMember(dest => dest.MenuItemId, opt => opt.MapFrom(src => src.MenuItemId))
                .ForMember(dest => dest.MenuItemSetId, opt => opt.MapFrom(src => src.MenuItemSetId));
            CreateMap<MenuItemMenuItemSetDto, MenuItemMenuItemSet>();

            // Customer mappings
            CreateMap<CustomerDto, Customer>().ReverseMap();
        }
    }
}
