using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.RequestParameters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Products.GetAllProducts
{
    public class GetAllProducts
    {
        public class GetAllProductsQueryRequest : IRequest<GetAllProductsQueryResponse>
        {
            //public Pagination Pagination { get; set; }
            public int Page { get; set; } = 0;
            public int Size { get; set; } = 5;
        }


        public class GetAllProductsQueryResponse
        {
            public int TotalCount { get; set; }
            public object Products { get; set; }
        }


        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, GetAllProductsQueryResponse>
        {
            readonly IProductReadRepository _productReadRepository;

            public GetAllProductsQueryHandler(IProductReadRepository productReadRepository)
            {
                _productReadRepository = productReadRepository;
            }

            public async Task<GetAllProductsQueryResponse> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
            {
                var totalCount = _productReadRepository.GetAll(false).Count();

                var products = _productReadRepository.GetAll(false)
                    .Skip(request.Page * request.Size)
                    .Take(request.Size)
                    .Select(x => new
                    {
                        x.Id,
                        x.Name,
                        x.Stock,
                        x.Price,
                        x.CreatedDate,
                        x.UpdatedDate
                    });

                return new()
                {
                    Products = products,
                    TotalCount = totalCount
                };

            }
        }
    }
}
