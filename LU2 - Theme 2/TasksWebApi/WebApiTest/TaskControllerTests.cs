using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TasksWebApi.Controllers;
using TasksWebApi.Data;
using TasksWebApi.DTOs;
using TasksWebApi.Entities;

namespace WebApiTest
{
    [TestClass]
    public class TaskControllerTests
    {
        private TaskController controller;
        private DataContext context;
        private Mock<ISession> sessionMock;
        private Mock<IResponseCookies> responseCookie;
        private Mock<IRequestCookieCollection> requestCookie;
        private const string AuthSessionKey = "UserSession";
        private const string AuthCookie = "UserCookie";


        [TestInitialize]
        public void Setup()
        {
            var dbOptions = new DbContextOptionsBuilder<DataContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            context = new DataContext(dbOptions);

            controller = new TaskController(context);
            /*All of this just for an in-memory database*/

            sessionMock = new Mock<ISession>();
            responseCookie = new Mock<IResponseCookies>();
            requestCookie = new Mock<IRequestCookieCollection>();

            //fake httpContext 
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(s => s.Session).Returns(sessionMock.Object);
            httpContext.Setup(rs => rs.Response.Cookies).Returns(responseCookie.Object);
            httpContext.Setup(rq => rq.Request.Cookies ).Returns(requestCookie.Object);

            //pass our fake http context to the controller

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext.Object
            };

        }
        //fake user -> helper method
        private void FakeUser(string username)
        {
            byte[] loggedInUsername = Encoding.UTF8.GetBytes(username);
            sessionMock.Setup(s=> s.TryGetValue(AuthSessionKey, out loggedInUsername)).Returns(true);
            requestCookie.Setup(c => c[AuthCookie]).Returns(username);
        }

        [TestMethod]
        public void AdminLoginValid()
        {
            var loginDto = new LoginDto { Username = "admin", Password = "Admin123" };

            var result = controller.Login(loginDto);

            var okResult = result as OkObjectResult;//200 OK
            Assert.IsNotNull(okResult);
            sessionMock.Verify(s => s.Set(AuthSessionKey, It.IsAny<byte[]>()), Times.Once);
            responseCookie.Verify(c => c.Append(AuthCookie, "admin", It.IsAny<CookieOptions>()), Times.Once);
        }

        [TestMethod]
        public void AdminLoginInvalid()
        {
            var loginDto = new LoginDto { Username = "Admin", Password = "pass" };

            var result = controller.Login(loginDto);

            var failedLogin = result as UnauthorizedObjectResult;
            Assert.IsNotNull(failedLogin);
            Assert.AreEqual("Invalid credentials", failedLogin.Value);
        }

        [TestMethod]
        public async Task GetAll_Tasks()
        {
            FakeUser("admin");

            context.Items.AddRange(
                new TaskItem
                {
                    Id = 1,
                    Title = "Submit cloud",
                    Description = "POE Part 1",
                    isComplete = false,
                    DueDate = DateTime.Now.AddDays(5)
                },
                new TaskItem
                {
                    Id = 2,
                    Title = "Do ICE Task 3",
                    Description = "CAs",
                    isComplete = true,
                    DueDate = DateTime.Now
                });

            await context.SaveChangesAsync();

            var result = await controller.GetTasks();

            var okResult = result.Result as OkObjectResult;

            Assert.IsNotNull(okResult);

            var tasks = okResult.Value as List<TaskItemDto>;

            Assert.IsNotNull(tasks);
        }
    }
}
