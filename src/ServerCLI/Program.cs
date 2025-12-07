using Core;
using Core.Interfaces;
using Core.Services.Implementations;
using Core.Services.Interfaces;
using Infraestructure;
using System.Net;
internal class Program
{
    private static async Task Main(string[] args)
    {
        
        //Instanciate services
        IUserRepository userRepository = new UserMockingRepository();
        IInternetAccesController internetAccesController = new InternetAccesControllerMock();
        ILoginService loginService = new LoginService(userRepository, internetAccesController);
        AsyncServerBase server = new CaptivePortalServer(new IPEndPoint(IPAddress.Parse("192.168.4.1"), 8000),loginService, "/media/chris/Windows/OSShared/CaptivePortal/src/Core/Pages");
        string action = args[0];
        try
        {
        if (action == "--start")
        {
            Console.WriteLine("Starting server");
            await server.StartAsync();
        }
        else if(action=="--stop")
        {
            Console.WriteLine("Stopping server");
            server.Stop();
        }
        }
        catch
        {
            throw new Exception("Invalid command");
        }
        
    }
}