using Microsoft.AspNetCore.Mvc;
using OnlineShopApi.Models;
using OnlineShopApi.Services;
using System;
using System.Collections.Generic;
using System.Net;

namespace OnlineShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _ProductService;

        public ProductsController(IProductService ProductService)
        {
            _ProductService = ProductService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_ProductService.Get());
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
            

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        public IActionResult Get(string id)
        {
            try
            {
                var Product = _ProductService.Get(id);

                if (Product == null)
                {
                    return NotFound();
                }

                return Ok(Product);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(Product Product)
        {
            try
            {
                _ProductService.Create(Product);

                return CreatedAtRoute("GetProduct", new { id = Product.Id.ToString() }, Product);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Product ProductIn)
        {
            try
            {
                Product Product = _ProductService.Get(id);

                if (Product == null)
                {
                    return NotFound();
                }

                _ProductService.Update(id, ProductIn);

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            try
            {
                Product Product = _ProductService.Get(id);

                if (Product == null)
                {
                    return NotFound();
                }

                _ProductService.Remove(Product.Id);

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
