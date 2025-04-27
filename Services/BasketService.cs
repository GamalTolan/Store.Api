using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Services.Abstractions;
using Shared.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BasketService(IBasketRepository basketRepository, IMapper mapper) :IBasketServices
    {
        public async Task<bool> DeleteBasketAsync(string id)
           => await basketRepository.DeleteBasketAsync(id);

        public async Task<BasketDto> GetBasketAsync(string id)
        {
            var baske = await basketRepository.GetBasketAsync(id);

            return basketRepository is null ? throw new BasketNotFoundException(id)
                                            : mapper.Map<BasketDto>(baske);
        }

        public async Task<BasketDto> UpdateBasketAsync(BasketDto basket)
        {
            var customerBasket = mapper.Map<CustomerBasket>(basket);

            var updateBasket = await basketRepository.UpdateBasketAsync(customerBasket);

            return updateBasket is null ? throw new Exception("cant update Basket now !")
                                        : mapper.Map<BasketDto>(updateBasket);
        }
    }
}
