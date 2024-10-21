﻿using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDTO>()
                .ForMember(d=>d.ProductType , O=>O.MapFrom(S=>S.ProductType.Name))
                .ForMember(d=>d.ProductBrand , O=>O.MapFrom(S=>S.ProductBrand.Name));
        }
    }
}
