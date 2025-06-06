﻿using Shared.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IOrderSrvice
    {
         Task<OrderResultDto> GetOrderByIdAsync(Guid id);
         Task<IEnumerable<OrderResultDto>> GetOrdersByUserEmailAsync(string userEmail);

        Task<OrderResultDto> CreateOrderAsync(OrderRequestDto orderRequest ,string userEmail);

        Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethods();


    }
}
