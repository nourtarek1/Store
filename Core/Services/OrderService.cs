using AutoMapper;
using Domian.Contracts;
using Domian.Exceptions;
using Domian.Models;
using Domian.Models.OrderModels;
using Services.Abstractions;
using Services.Specifications;
using Shared.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService 
        (IMapper mapper,
         IBasketRepository basketRepository,
         IUnitOfWork unitOfWork
        )
        : IOrderSrvice
    {
        public async Task<OrderResultDto> CreateOrderAsync(OrderRequestDto orderRequest, string userEmail)
        {
            // 1 . Addres

            var address = mapper.Map<Address>(orderRequest.ShipToAddress);

            // 2. Order ITems =>Basket
            var basket = await basketRepository.GetBasketAsync(orderRequest.BasketId);
            if(basket is null) throw new BasketNotFoundException(orderRequest.BasketId);
            var orderItems = new List<OrderItem>();
            foreach(var item in basket.Items)
            {
                var product = await unitOfWork.GetRepository<Product, int>().GetAsync(item.Id);
                if (product is null) throw new ProudctNotFoundExceptions(item.Id);
                var orderItem = new OrderItem(new ProductInOrderItem(product.Id, product.Name, product.PictureUrl),item.Quantity,product.Price);
                orderItems.Add(orderItem);
            
            }
            // 3 . GetDelivery Method
            var deliveryMethod =  await unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(orderRequest.DeliveryMethodId);
            if (deliveryMethod is null) throw new DeliveryMethodNotFoundException(orderRequest.DeliveryMethodId);

            var subTotal = orderItems.Sum(i => i.Price * i.Quantity);
            
            
            var order = new Order(userEmail, address, orderItems, deliveryMethod, subTotal,"");
            await unitOfWork.GetRepository<Order, Guid>().AddAsync(order);
           var count =  await unitOfWork.SaveChasgesAsync();
            if (count == 0) throw new OrderBadRequestException();
             var result = mapper.Map<OrderResultDto>(order);
            return result;
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethods()
        {
            var deliveryMethods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();

            var result = mapper.Map<IEnumerable<DeliveryMethodDto>>(deliveryMethods);

            return result;
        }

        public async Task<OrderResultDto> GetOrderByIdAsync(Guid id)
        {
            var spec = new OrderSpecification(id);
          var order = await  unitOfWork.GetRepository<Order, Guid>().GetAsync(spec);
            if (order is null) throw new OrderNotFoundException(id);

            var result = mapper.Map<OrderResultDto>(order);
            return result;

        }

        public async Task<IEnumerable<OrderResultDto>> GetOrdersByUserEmailAsync(string userEmail)
        {
            var spec = new OrderSpecification(userEmail);
            var orders = await unitOfWork.GetRepository<Order, Guid>().GetAllAsync(spec);

            var result = mapper.Map<IEnumerable<OrderResultDto>>(orders);
            return result;
        }
    }
}
