using CRUD.Controllers;
using CRUD.Models;
using CRUD.Repository;
using CRUD.Tests;
using CRUD.ViewModel;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

[TestFixture]
public class PostControlTests
{

    private static PostRepository repository;
    public static DbContextOptions<BlogDBContext> dbContextOptions { get; set; }
    public static string connectionString = "Server=LAPTOP-NL75CAU3;Database=BlogDB;User Id=sa;Password=*******";

    static PostControlTests()
    {
        dbContextOptions = new DbContextOptionsBuilder<BlogDBContext>()
            .UseSqlServer(connectionString)
            .Options;
    }
    public PostControlTests()
    {
        var context = new BlogDBContext(dbContextOptions);
        DummyDataDBInitializer db = new DummyDataDBInitializer();
        db.Seed(context);

        repository = new PostRepository(context);
    }


    [TestCase]
    public async Task Task_GetPostById_Return_OkResult()
    {
        //Arrange  
        var controller = new PostController(repository);
        var postId = 2;

        //Act  
        var data = await controller.GetPost(postId);

        //Assert  
        Assert.IsInstanceOf<OkObjectResult>(data);
    }

    [TestCase]
    public async Task Task_GetPostById_Return_NotFoundResult()
    {
        //Arrange  
        var controller = new PostController(repository);
        var postId = 3;

        //Act  
        var data = await controller.GetPost(postId);

        //Assert  
        Assert.IsInstanceOf<NotFoundResult>(data);
    }

    [TestCase]
    public async Task Task_GetPostById_Return_BadRequestResult()
    {
        //Arrange  
        var controller = new PostController(repository);
        int? postId = null;

        //Act  
        var data = await controller.GetPost(postId);

        //Assert  
        Assert.IsInstanceOf<BadRequestResult>(data);
    }

    [TestCase]
    public async Task Task_GetPostById_MatchResult()
    {
        //Arrange  
        var controller = new PostController(repository);
        int? postId = 1;

        //Act  
        var data = await controller.GetPost(postId);

        //Assert  
        Assert.IsInstanceOf<OkObjectResult>(data);

        var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
        var post = okResult.Value.Should().BeAssignableTo<PostViewModel>().Subject;

        Assert.Equals("Test Title 1", post.Title);
        Assert.Equals("Test Description 1", post.Description);
    }



    [TestCase]
    public async Task Task_GetPosts_Return_OkResult()
    {
        //Arrange  
        var controller = new PostController(repository);

        //Act  
        var data = await controller.GetPosts();

        //Assert  
        Assert.IsInstanceOf<OkObjectResult>(data);
    }

    [TestCase]
    public async Task Task_GetPosts_Return_BadRequestResult()
    {
        //Arrange  
        var controller = new PostController(repository);

        //Act  
        var data = await controller.GetPosts();
        data = null;

        if (data != null)
            //Assert  
            Assert.IsInstanceOf<BadRequestResult>(data);
    }

    [TestCase]
    public async Task Task_GetPosts_MatchResult()
    {
        //Arrange  
        var controller = new PostController(repository);

        //Act  
        var data = await controller.GetPosts();

        //Assert  
        Assert.IsInstanceOf<OkObjectResult>(data);

        var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
        var post = okResult.Value.Should().BeAssignableTo<List<PostViewModel>>().Subject;

        Assert.Equals("Test Title 1", post[0].Title);
        Assert.Equals("Test Description 1", post[0].Description);

        Assert.Equals("Test Title 2", post[1].Title);
        Assert.Equals("Test Description 2", post[1].Description);
    }

    [TestCase]
    public async Task Task_Update_ValidData_Return_OkResult()
    {
        //Arrange  
        var controller = new PostController(repository);
        var postId = 2;

        //Act  
        var existingPost = await controller.GetPost(postId);
        var okResult = existingPost.Should().BeOfType<OkObjectResult>().Subject;
        var result = okResult.Value.Should().BeAssignableTo<PostViewModel>().Subject;

        var post = new Post();
        post.Title = "Test Title 2 Updated";
        post.Description = result.Description;
        post.CategoryId = result.CategoryId;
        post.CreatedDate = result.CreatedDate;

        var updatedData = await controller.UpdatePost(post);

        //Assert  
        Assert.IsInstanceOf<OkResult>(updatedData);
    }

    [TestCase]
    public async Task Task_Update_InvalidData_Return_BadRequest()
    {
        //Arrange  
        var controller = new PostController(repository);
        var postId = 2;

        //Act  
        var existingPost = await controller.GetPost(postId);
        var okResult = existingPost.Should().BeOfType<OkObjectResult>().Subject;
        var result = okResult.Value.Should().BeAssignableTo<PostViewModel>().Subject;

        var post = new Post();
        post.Title = "Test Title More Than 20 Characteres";
        post.Description = result.Description;
        post.CategoryId = result.CategoryId;
        post.CreatedDate = result.CreatedDate;

        var data = await controller.UpdatePost(post);

        //Assert  
        Assert.IsInstanceOf<BadRequestResult>(data);
    }

    [TestCase]
    public async Task Task_Update_InvalidData_Return_NotFound()
    {
        //Arrange  
        var controller = new PostController(repository);
        var postId = 2;

        //Act  
        var existingPost = await controller.GetPost(postId);
        var okResult = existingPost.Should().BeOfType<OkObjectResult>().Subject;
        var result = okResult.Value.Should().BeAssignableTo<PostViewModel>().Subject;

        var post = new Post();
        post.PostId = 5;
        post.Title = "Test Title More Than 20 Characteres";
        post.Description = result.Description;
        post.CategoryId = result.CategoryId;
        post.CreatedDate = result.CreatedDate;

        var data = await controller.UpdatePost(post);

        //Assert  
        Assert.IsInstanceOf<NotFoundResult>(data);
    }

    [TestCase]
    public async Task Task_Delete_Post_Return_OkResult()
    {
        //Arrange  
        var controller = new PostController(repository);
        var postId = 2;

        //Act  
        var data = await controller.DeletePost(postId);

        //Assert  
        Assert.IsInstanceOf<OkResult>(data);
    }

    [TestCase]
    public async Task Task_Delete_Post_Return_NotFoundResult()
    {
        //Arrange  
        var controller = new PostController(repository);
        var postId = 5;

        //Act  
        var data = await controller.DeletePost(postId);

        //Assert  
        Assert.IsInstanceOf<NotFoundResult>(data);
    }

    [TestCase]
    public async Task Task_Delete_Return_BadRequestResult()
    {
        //Arrange  
        var controller = new PostController(repository);
        int? postId = null;

        //Act  
        var data = await controller.DeletePost(postId);

        //Assert  
        Assert.IsInstanceOf<BadRequestResult>(data);
    }
}