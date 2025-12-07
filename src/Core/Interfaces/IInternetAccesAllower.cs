using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IInternetAccesController
    {
        public Task AllowTraffic(string ip);
        public Task BlockTraffic(string ip);
    }
}
