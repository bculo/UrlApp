using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrlUtility.API.Entities;
using UrlUtility.API.Interfaces;
using UrlUtility.API.Repository.Sql;
using UrlUtility.API.Repository.Sql.Repositories;

namespace UrlUtility.API.Test
{
    public class ShorUrlRepositoryTests
    {
        private DbContextOptions<SqlUrlDbContext> dbOptions;

        [SetUp]
        public void Setup()
        {
            dbOptions = new DbContextOptionsBuilder<SqlUrlDbContext>()
                .UseInMemoryDatabase(databaseName: "ShortUrl")
                .Options;        
        }

        [Test]
        public async Task Should_Return_Url_When_Given_Id_Exists()
        {
            var repository = await CreateRepository();

            var url = await repository.GetUrl(1);

            Assert.NotNull(url);
            Assert.AreEqual("https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=vs", url.PageUrl);
        }

        [Test]
        public async Task Should_Return_Null_When_Given_Id_Doesnt_Exists()
        {
            var repository = await CreateRepository();

            var url = await repository.GetUrl(-1);

            Assert.IsNull(url);
        }

        [Test]
        public async Task Should_Persiste_Url_When_Url_Added_To_Repository()
        {
            var repository = await CreateRepository();

            await repository.Add(new Entities.ShortUrl
            {
                PageUrl = "https://stackoverflow.com/questions/26882986/overwrite-json-property-name-in-c-sharp",
                Id = 4,
            });

            var addedUrl = await repository.GetUrl(4);

            Assert.IsNotNull(addedUrl);
        }

        [Test]
        public async Task Should_Throw_Exception_When_Adding_Url_With_Same_Id()
        {
            var repository = await CreateRepository();

            var newUrl = new Entities.ShortUrl
            {
                PageUrl = "https://github.com/moq/moq",
                Id = 1,
            };

            Assert.ThrowsAsync<InvalidOperationException>(async () => await repository.Add(newUrl));
        }

        [Test]
        public async Task Should_Return_List_When_Fetching_All_Urls()
        {
            var repository = await CreateRepository();

            var items = await repository.GetAll();

            Assert.NotNull(items);
            Assert.IsInstanceOf<List<ShortUrl>>(items);
        }

        private async Task<IShortUrlRepository> CreateRepository()
        {
            var context = new SqlUrlDbContext(dbOptions);
            await SeedData(context);
            return new ShortUrlRepository(context);
        }

        private async Task SeedData(SqlUrlDbContext context)
        {
            context.ShortUrls.Add(new Entities.ShortUrl
            {
                Id = 1,
                PageUrl = "https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=vs"
            });

            context.ShortUrls.Add(new Entities.ShortUrl
            {
                Id = 2,
                PageUrl = "https://stackoverflow.com/"
            });

            context.ShortUrls.Add(new Entities.ShortUrl
            {
                Id = 3,
                PageUrl = "https://docs.microsoft.com/en-us/ef/core/providers/cosmos/?tabs=dotnet-core-cli"
            });

            await context.SaveChangesAsync();
        }
    }
}