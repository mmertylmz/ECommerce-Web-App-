using ECommerceAPI.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Products.GetByIdProduct
{
    public class GetByIdProduct
    {
        public class GetByIdProductQueryRequest : IRequest<GetByIdProductQueryResponse>
        {
            public string Id { get; set; }
        }


        public class GetByIdProductQueryResponse
        {
            public string Name { get; set; }
            public int Stock { get; set; }
            public float Price { get; set; }
        }


        public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
        {
            readonly IProductReadRepository _productReadRepository;

            public GetByIdProductQueryHandler(IProductReadRepository productReadRepository)
            {
                _productReadRepository = productReadRepository;
            }

            public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
            {
                ECommerceAPI.Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id, false);
                return new()
                {
                    Name = product.Name,
                    Price = product.Price,
                    Stock = product.Stock,
                };
            }
        }
    }
}
