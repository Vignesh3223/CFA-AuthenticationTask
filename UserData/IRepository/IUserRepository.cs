using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserData.Models;

namespace UserData.IRepository
{
    public interface IUserRepository
    {
        public Task<List<User>> GetUsers();
        public Task<User> GetUserById(int Userid);
        public Task<Success> CreateUser([FromBody] User user);
        public Task<Success> UpdateUser(User user);
        public Task<Success> DeleteUser(int Userid);

    }
}
