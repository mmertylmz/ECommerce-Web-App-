using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.ProductImageFiles.UploadProductImage
{
    public class UploadProductImage
    {
        public class UploadProductImageCommandRequest : IRequest<UploadProductImageCommandResponse>
        {
            public string Id { get; set; }
            public IFormFileCollection? Files { get; set; }
        }


        public class UploadProductImageCommandResponse 
        { 
        
        }


        public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
        {
            readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
            readonly IProductReadRepository _productReadRepository;
            readonly IStorageService _storageService;

            public UploadProductImageCommandHandler(IProductImageFileWriteRepository productImageFileWriteRepository, IProductReadRepository productReadRepository, IStorageService storageService)
            {
                _productImageFileWriteRepository = productImageFileWriteRepository;
                _productReadRepository = productReadRepository;
                _storageService = storageService;
            }

            public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
            {
                List<(string fileName, string pathOrContainerName)> datas = await _storageService.UploadAsync("productimagefile", request.Files);

                Product product = await _productReadRepository.GetByIdAsync(request.Id);

                await _productImageFileWriteRepository.AddRangeAsync(datas.Select(x => new ProductImageFile
                {
                    FileName = x.fileName,
                    Path = x.pathOrContainerName,
                    Storage = _storageService.StorageName,
                    Products = new List<Product> { product }
                }).ToList());

                await _productImageFileWriteRepository.SaveAsync();

                return new();
            }
        }
    }
}
