using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Interfaces
{
    public interface ILoginService
    {
        public Task LoginAsync(string username, string password,string ip);    
    }
}
