﻿using Mapster;
using System;
using DAL = VidlyDB;
using Models = VidlyModels.Models;

namespace VidlyBL.BusinessLogic
{
    struct ObjectAdapterConfigurations
    {
        static ObjectAdapterConfigurations()
        {
            CustomerModelConfigurations();
            MovieModelConfigurations();
        }
        public static void RegisterConfigurations()
        {
        }

        private static void CustomerModelConfigurations()
        {
            // Create Adapter configurations between transformations
            // for Models.Customer and DAL.Customer
            TypeAdapterConfig<DAL.Customer, Models.Customer>.ForType().
                Map(dest => dest.Id, source => source.CustomerId);

            TypeAdapterConfig<Models.Customer, DAL.Customer>.ForType().
                Map(dest => dest.CustomerId, source => source.Id);
        }

        private static void MovieModelConfigurations()
        {
            TypeAdapterConfig<DAL.Movie, Models.Movie>.ForType()
                   .Map(dest => dest.Id, source => source.MovieId)
                   .Map(dest => dest.Name, source => source.MovieName)
                   .Map(dest => dest.CategoryDetails, source => source.MovieCategory)
                   //.Map(dest => dest.CategoryDetails.CategoryId, source => source.CategoryId)
                   //.Map(dest => dest.CustomerDetails.Id, source => source.CustomerId)
                   .Map(dest => dest.CustomerDetails, source => source.Customer);

            TypeAdapterConfig<Models.Movie, DAL.Movie>.ForType()
                .Map(dest => dest.MovieId, source => source.Id)
                .Map(dest => dest.MovieName, source => source.Name)
                .Map(dest => dest.MovieCategory, source => source.CategoryDetails)
                //.Map(dest => dest.CategoryId, source => source.CategoryDetails.CategoryId)
                //.Map(dest => dest.CustomerId, source => source.CustomerDetails.Id)
                .Map(dest => dest.Customer, source => source.CustomerDetails);
        }
    }
}
