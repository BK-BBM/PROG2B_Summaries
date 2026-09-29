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

namespace WebApixUnitTest
{
    public class TaskControllerxUnitTest
    {
        private readonly DbContextOptions<DataContext> _dbOptions;
        private const string AuthSessionKey = "UserSession";
        private const string AuthCookie = "UserCookie";

        /* In this example, to avoid using the 'tuple' config
         * We will add class fields.
         */

        private DataContext context;
        private Mock<ISession> _sessionMock;
        private Mock<IResponseCookies> _responseCookies;
        private Mock<IRequestCookieCollection> _requestCookies;

        public TaskControllerxUnitTest()
        {
            _dbOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        /*helper method -> to demo for people how you'd approach 
         * testing login if there was an AuthController 
         * (which there always SHOULD)
         */
        private TaskController CreateController(string sessionUser = null,
            string cookieUser = null)
        {
            context = new DataContext(_dbOptions);
            var controller = new TaskController(context);

            _sessionMock = new Mock<ISession>();
            byte[] sessionInfo = sessionUser != null ?
                System.Text.Encoding.UTF8.GetBytes(sessionUser) : null;

            /* to set our byte[] (sessionInfo)
             * 
             * we check if the sessionUser is not null. 
             * 
             * If it's not null, encode the username, otherwise leave it as null
             * (pay attention to the ternary operator usage) 
             
             */

            _sessionMock.Setup(s => s.TryGetValue(AuthSessionKey, out sessionInfo))
                .Returns(sessionUser != null);

            _responseCookies = new Mock<IResponseCookies>();
            _requestCookies = new Mock<IRequestCookieCollection>();

            _requestCookies.Setup(c => c[AuthCookie]).Returns(cookieUser);

            //fake httpcontext (cookies and sessions above, httpContext below)

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(s => s.Session).Returns(_sessionMock.Object);
            httpContext.Setup(s => s.Response.Cookies).Returns(_responseCookies.Object);
            httpContext.Setup(s => s.Request.Cookies).Returns(_requestCookies.Object);

            controller.ControllerContext = new ControllerContext { HttpContext = httpContext.Object };

            return controller;

        }

        [Fact]
        public void ValidLogin()
        {
            var controller = CreateController();
            var loginDto = new LoginDto { Username = "admin", Password = "Admin123" };

            var result = controller.Login(loginDto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);

            _sessionMock.Verify(s => s.Set(AuthSessionKey, It.IsAny<byte[]>()), Times.Once);
            _responseCookies.Verify(c => c.Append(AuthCookie, "admin", It.IsAny<CookieOptions>()), Times.Once);
        }
    }
}
