using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Products.UpdateProduct
{
    public class UpdateProduct
    {
        public class UpdateProductCommandRequest : IRequest<UpdateProductCommandResponse> 
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public float Price { get; set; }
            public int Stock { get; set; }
        }


        public class UpdateProductCommandResponse 
        { 

        }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
        {
            readonly IProductReadRepository _productReadRepository;
            readonly IProductWriteRepository _productWriteRepository;

            public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
            {
                _productReadRepository = productReadRepository;
                _productWriteRepository = productWriteRepository;
            }

            public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
            {
                ECommerceAPI.Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);
                product.Stock = request.Stock;
                product.Name = request.Name;
                product.Price = request.Price;
                await _productWriteRepository.SaveAsync();

                return new();
            }
        }
    }
}
