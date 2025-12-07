using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetByNameAsync(string username);
        public IAsyncEnumerable<User> GetAllAsync();
        public Task DeleteAsync(string username);
        public Task AddAsync(User user);
        public Task UpdateAsync(string username,User user);
    }
}
