using System;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class AuthRepo : IAuthRepo
    {
        private readonly DataContext context;
        public AuthRepo(DataContext context)
        {
            this.context = context;
        }
        public async Task<User> Login(string username, string password)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null) return null;
            if (!VerifyPHash(password, user.PasswordHash, user.PasswordSalt)) return null;
            return user;
        }

        private bool VerifyPHash(string password, byte[] pHash, byte[] pSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(pSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i< computedHash.Length; i++)
                {if (computedHash[i] != pHash[i]) return false;}

            }
            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] pHash, pSalt;
            CreatePasswordHash(password, out pHash, out pSalt );
            
            user.PasswordHash = pHash;
            user.PasswordSalt = pSalt;

            await this.context.Users.AddAsync(user);
            await this.context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] pHash, out byte[] pSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                pSalt = hmac.Key;
                pHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }

        public async Task<bool> UserExists(string username)
        {
            if (await this.context.Users.AnyAsync( x => x.Username == username)) return true;
            return false;
            
        }
    }
}