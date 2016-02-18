﻿using AutoMapper;
using DataModel.Model;
using RMS.Client.Models.View;
using RMS.Client.Models.View.MenuModels;

namespace RMS.Client.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMapping()
        {
            Mapper.CreateMap<RegisterModel, UserInfo>();
            Mapper.CreateMap<Restaurant, RestaurantModel>();
            Mapper.CreateMap<UserInfo, ProfileModel>();
            Mapper.CreateMap<ProfileModel, UserInfo>();
            Mapper.CreateMap<DishModel, Dish>();
            Mapper.CreateMap<Dish, DishModel>();
            Mapper.CreateMap<Category, CategoryModel>()
                  .ForMember(m => m.DishModels, o => o.MapFrom(c => c.Dishes));
            Mapper.CreateMap<CategoryModel, Category>();
        }
    }
}