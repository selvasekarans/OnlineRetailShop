using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using OnlineShopApi.Controllers;
using OnlineShopApi.Models;
using OnlineShopApi.Services;
using System;
using System.Collections.Generic;
using System.Net;
using static OnlineShopApi.Models.OnlineShopDbSettings;

namespace OnlineShopUnitTest
{
    class ProductsControllerTest
    {
        private readonly Mock<IProductService> _processor;
        private readonly ProductsController _controller;
        private Product _product;
        private List<Product> _products;

        public ProductsControllerTest()
        {
            _processor = new Mock<IProductService>();
            _controller = new ProductsController(_processor.Object);
        }

        [SetUp]
        public void Setup()
        {
            _products = new List<Product>();
            _product = new Product()
            {
                Id = "61795c2ab1be2f062f413c9b",
                ProductId = 1,
                ProductName = "",
                Description = "",
                Quantity = 10,
                Price = 22300
            };

            _products.Add(_product);
        }

        [Test]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            _processor.Setup(x => x.Get()).Returns(_products);
            var result = _controller.Get() as ObjectResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode.Value);
        }

        [Test]
        public void Get_WhenCalled_ReturnsError()
        {
            _processor.Setup(x => x.Get()).Throws(new Exception());
            var result = _controller.Get() as ObjectResult;
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode.Value);
        }

        [TestCase("61795c2ab1be2f062f413c9b")]
        public void GetById_WhenCalled_ReturnsOkResult(string id)
        {
            _processor.Setup(x => x.Get(id)).Returns(_product);
            var result = _controller.Get(id) as ObjectResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode.Value);
        }

        [Test]
        public void Create_WhenCalled_ReturnsOkResult()
        {
            var product = new Product()
            {
                Id = "",
                ProductId = 3,
                ProductName = "New Product",
                Description = "New Product Desc",
                Quantity = 15,
                Price = 1100
            };

            _processor.Setup(x => x.Create(product)).Returns(_product);
            var result = _controller.Create(product) as ObjectResult;
            Assert.AreEqual((int)HttpStatusCode.Created, result.StatusCode.Value);
        }

        [TestCase("61795c2ab1be2f062f413c9b")]
        public void Update_WhenCalled_ReturnsOkResult(string id)
        {
            var product = new Product()
            {
                Id = "",
                ProductId = 3,
                ProductName = "Update Product",
                Description = "Update Product Desc",
                Quantity = 15,
                Price = 1100
            };

            _processor.Setup(x => x.Update(id, product)).Verifiable();
            _processor.Object.Update(id, product);
            _processor.VerifyAll();
        }

        [TestCase("61795c2ab1be2f062f413c9b")]
        public void Delete_WhenCalled_ReturnsOkResult(string id)
        {
            _processor.Setup(x => x.Remove(id)).Verifiable();
            _processor.Object.Remove(id);
            _processor.VerifyAll();
        }
    }
}