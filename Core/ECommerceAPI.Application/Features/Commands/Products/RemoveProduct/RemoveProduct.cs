﻿using ECommerceAPI.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Products.RemoveProduct
{
    public class RemoveProduct
    {

        public class RemoveProductCommandRequest : IRequest<RemoveProductCommandResponse>
        {
            public string Id { get; set; }
        }


        public class RemoveProductCommandResponse
        {

        }


        public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommandRequest, RemoveProductCommandResponse>
        {
            readonly IProductWriteRepository _productWriteRepository;

            public RemoveProductCommandHandler(IProductWriteRepository productWriteRepository)
            {
                _productWriteRepository = productWriteRepository;
            }

            public async Task<RemoveProductCommandResponse> Handle(RemoveProductCommandRequest request, CancellationToken cancellationToken)
            {
                await _productWriteRepository.RemoveAsync(request.Id);
                await _productWriteRepository.SaveAsync();
                return new();
            }
        }
    }
}
