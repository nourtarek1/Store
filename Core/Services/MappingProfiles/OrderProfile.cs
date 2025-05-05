﻿using AutoMapper;
using Domian.Models.OrderModels;
using Shared.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile() {

            CreateMap<Address, AddressDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.productId, o => o.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Product.PicturUrl));

            CreateMap<Order, OrderResultDto>()
                .ForMember(d => d.PaymentStatus, o => o.MapFrom(s => s.PaymentStatus.ToString()))
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.Total, o => o.MapFrom(s => s.SubTotal + s.DeliveryMethod.Cost));

            CreateMap<DeliveryMethod, DeliveryMethodDto>();
        }
    }
}
