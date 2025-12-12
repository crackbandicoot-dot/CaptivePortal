using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure
{
    public class UserMockingRepository : IUserRepository
    {
        private Dictionary<string,User> _users;
        public UserMockingRepository()
        {
            _users = new() { { "username", new User("username","password")} };
        }
        public async Task AddAsync(User user)
        {
            _users.Add(user.UserName, user);
        }

        public async Task DeleteAsync(string username)
        {
            _users.Remove(username);
        }

        public IAsyncEnumerable<User> GetAllAsync()
        {
            return _users.Values.ToAsyncEnumerable();
        }

        public async Task<User> GetByNameAsync(string username)
        {
            if(_users.TryGetValue(username, out User user))
            {
                return user;
            }
            throw new Exception();
        }

        public async Task UpdateAsync(string username, User user)
        {
            _users.Remove(username);
            _users.Add(username, user);

        }
    }
}
