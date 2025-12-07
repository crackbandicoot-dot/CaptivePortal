using Core.Interfaces;
using Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Implementations
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository userRepository;
        private readonly IInternetAccesController internetAllower;

        public LoginService(IUserRepository userRepository,IInternetAccesController internetAllower)
        {
            this.userRepository = userRepository;
            this.internetAllower = internetAllower;
        }
        public async Task LoginAsync(string username, string password,string ip)
        {
            var user = await userRepository.GetByNameAsync(username);
            if (user.UserName==username && password==user.Password)
            {
               await internetAllower.AllowTraffic(ip);
            }
            else
            {
                throw new Exception("Invalid username or password");
            }
        }
        
    }
}
