using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserData.IRepository;
using UserData.Models;

namespace UserData.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;
        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetUserById(int Userid)
        {
            var user = await _context.Users.FindAsync(Userid);
            return user;
        }

        public async Task<Success> CreateUser([FromBody] User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return new Success
            {
                Status = "Success",
                Message = "User Created Successfully"
            };
        }

        public async Task<Success> UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new Success
            {
                Status = "Success",
                Message = "User Edited Successfully"
            };
        }

        public async Task<Success> DeleteUser(int Userid)
        {
            var user = await _context.Users.FindAsync(Userid);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return new Success
            {
                Status = "Success",
                Message = "User Deleted Successfully"
            };
        }
    }
}
