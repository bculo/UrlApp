using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlUtility.API.Controllers;
using UrlUtility.API.Interfaces;
using UrlUtility.API.Repository.Sql;
using UrlUtility.API.Repository.Sql.Repositories;

namespace UrlUtility.API.Test
{
    public class UrlControllerTests
    {
        private DbContextOptions<SqlUrlDbContext> dbOptions;
        private IUrlRepository _urlRepo;
        private IHashids _hash;
        private ITime _time;

        [SetUp]
        public void Setup()
        {
            dbOptions = new DbContextOptionsBuilder<SqlUrlDbContext>()
                .UseInMemoryDatabase(databaseName: "ShortUrl")
                .Options;

            _urlRepo = new Mock<IUrlRepository>().Object;

            _hash = new Hashids("RandomTestSalt", 6);

            _time = new Mock<ITime>().Object;
        }

        [Test]
        public async Task Should_Return_Response_Success_When_Valid_Item_Submitted()
        {
            var controller = CreateUrlController();

            var response = await controller.ShortUrl(new Dtos.SaveUrlDto
            {
                PageUrl = "https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=vs"
            });

            if(response is OkObjectResult)
            {
                var successResponse = response as OkObjectResult;
                var responseValue = successResponse.Value as string;

                Assert.AreEqual(responseValue.Length, 6);
                return;
            }

            Assert.IsTrue(false);
        }

        [Test]
        public async Task Should_Return_Response_BadRequest_When_Invalid_Item_Submitted()
        {
            var controller = CreateUrlController();

            var response = await controller.ShortUrl(new Dtos.SaveUrlDto
            {
                PageUrl = null
            });

            if (response is BadRequestResult)
            {
                var successResponse = response as BadRequestResult;
                Assert.AreEqual(400, successResponse.StatusCode);
                return;
            }

            Assert.IsTrue(false);
        }

        private UrlController CreateUrlController()
        {
            var shorUrlRepo = CreateRepository();
            return new UrlController(_urlRepo, _time, shorUrlRepo, _hash);
        }

        private IShortUrlRepository CreateRepository()
        {
            var context = new SqlUrlDbContext(dbOptions);
            return new ShortUrlRepository(context);
        }
    }
}
