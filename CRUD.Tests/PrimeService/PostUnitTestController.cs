using CRUD.Controllers;
using CRUD.Models;
using CRUD.Repository;
using CRUD.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace CRUD.Tests.PrimeService
{
    [TestFixture]
    public class PostUnitTestController
    {
        private PostRepository repository;
        public static DbContextOptions<BlogDBContext> dbContextOptions { get; }
        public static string connectionString = "Server=LAPTOP-NL75CAU3;Database=BlogDB;UID=sa;PWD=Nations@123;";

        static PostUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<BlogDBContext>()
                .UseSqlServer(connectionString)
                .Options;
        }


        [Test]
        public async void Task_GetPostById_Return_OkResult()
        {
            //Arrange  
            var controller = new PostController(repository);
            var postId = 2;

            //Act  
            var data = await controller.GetPost(postId);

            //Assert  
            Assert.IsInstanceOf<OkObjectResult>(data);
        }

        [Test]
        public async void Task_GetPostById_Return_NotFoundResult()
        {
            //Arrange  
            var controller = new PostController(repository);
            var postId = 3;

            //Act  
            var data = await controller.GetPost(postId);

            //Assert  
            Assert.IsInstanceOf<NotFoundResult>(data);
        }

        [Test]
        public async void Task_GetPostById_Return_BadRequestResult()
        {
            //Arrange  
            var controller = new PostController(repository);
            int? postId = null;

            //Act  
            var data = await controller.GetPost(postId);

            //Assert  
            Assert.IsInstanceOf<BadRequestResult>(data);
        }
    }
}
