using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommerceAPI.Application.Features.Queries.ProductImageFiles.GetProductImages
{
    public class GetProductImages
    {
        public class GetProductImagesQueryRequest : IRequest<List<GetProductImagesQueryResponse>>
        {
            public string Id { get; set; }
        }

        public class GetProductImagesQueryResponse 
        {
            public Guid Id { get; set; }
            public string? FileName { get; set; }
            public string? Path { get; set; }
        }

        public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQueryRequest, List<GetProductImagesQueryResponse>>
        {
            IProductReadRepository _productReadRepository;
            IConfiguration _configuration;

            public GetProductImagesQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration)
            {
                _productReadRepository = productReadRepository;
                _configuration = configuration;
            }

            public async Task<List<GetProductImagesQueryResponse>?> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
            {
                Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.Id));

                return product?.ProductImageFiles.Select(x => new GetProductImagesQueryResponse
                {
                    Path = $"{_configuration["BaseStorageUrl"]}/{x.Path}",
                    FileName = x.FileName,
                    Id = x.Id
                }).ToList();
            }
        }
    }
}
