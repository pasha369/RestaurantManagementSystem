using AutoMapper;
using DataModel.Model;
using RMS.Client.Models.View;
using RMS.Client.Models.View.MenuModels;
using RMS.Client.Models.View.OrderModels;

namespace RMS.Client.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMapping()
        {
            Mapper
                .CreateMap<RegisterModel, UserInfo>();
            Mapper
                .CreateMap<Restaurant, RestaurantModel>();
            Mapper
                .CreateMap<RestaurantModel, Restaurant>();
            Mapper
                .CreateMap<UserInfo, ProfileModel>();
            Mapper
                .CreateMap<ProfileModel, UserInfo>();
            Mapper
                .CreateMap<DishModel, Dish>();
            Mapper
                .CreateMap<Dish, DishModel>();
            Mapper
                .CreateMap<Dish, DishItemModel>();
            Mapper
                .CreateMap<Category, CategoryModel>()
                .ForMember(m => m.DishModels, o => o.MapFrom(c => c.Dishes));
            Mapper
                .CreateMap<CategoryModel, Category>();
            Mapper
                .CreateMap<DinnerTable, TableViewModel>()
                .ForMember(m => m.TableId, o => o.MapFrom(c => c.Id))
                .ForMember(m => m.Restaurant, o => o.MapFrom(c => c.Restaurant));
            Mapper
                .CreateMap<Reservation, ReservationViewModel>()
                .ForMember(m => m.Status, o => o.MapFrom(c => c.Status.ToString()));
        }
    }
}