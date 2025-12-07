using Core;
using Core.Interfaces;
using Core.Services.Implementations;
using Core.Services.Interfaces;
using Infraestructure;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    [TestClass]
    public class CaptivePortalTest
    {
        [TestMethod]
        public async Task TestServer()
        {
            IUserRepository userRepository= new UserMockingRepository();
            IInternetAccesController internetController = new InternetAccesControllerMock();
            ILoginService loginService = new LoginService(userRepository,internetController);
            CaptivePortalServer server = new(new IPEndPoint(IPAddress.Loopback, 8000),loginService, @"C:\OSShared\CaptivePortal\src\Core\Pages");
            await server.StartAsync();
        }
    }

}
