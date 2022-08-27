﻿using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        [HttpGet]
        public async Task Get()
        {
            //await _productWriteRepository.AddRangeAsync(new()
            //{
            //    new(){Id=Guid.NewGuid(), Name="Product 1", CreatedDate=DateTime.UtcNow, Price=100, Stock=10},
            //    new(){Id=Guid.NewGuid(), Name="Product 2", CreatedDate=DateTime.UtcNow, Price=120, Stock=30},
            //    new(){Id=Guid.NewGuid(), Name="Product 3", CreatedDate=DateTime.UtcNow, Price=150, Stock=20}
            //});
            //var count = await _productWriteRepository.SaveAsync();

            //Product p = await _productReadRepository.GetByIdAsync("de66c59a-2db1-4afd-8113-b5755dc19691"); //default true
            Product p = await _productReadRepository.GetByIdAsync("de66c59a-2db1-4afd-8113-b5755dc19691",false); //change will not be affected
            p.Name = "Mert";
            await _productWriteRepository.SaveAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Product product = await _productReadRepository.GetByIdAsync(id);
            return Ok(product);
        }
    }
}
