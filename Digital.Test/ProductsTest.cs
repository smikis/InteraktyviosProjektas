using Digital.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Linq;

namespace Digital.Test
{
    [TestClass]
    public class ProductsTest
    {
        [TestMethod]
        public void ShouldInsertNewProduct()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                  .Options;
            var context = new ApplicationDbContext(options);
            var repo = new ProductRepository(context);
            repo.InsertProduct(new Models.Product {
                Name = "aaaa",
                Description = "BBBB",
                CreateDate = DateTime.Now
            });
            repo.Save();
            var list = repo.GetProducts();
            Assert.IsTrue(list.Any());
        }
    }
}
