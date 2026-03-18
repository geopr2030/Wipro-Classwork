using SecureUserApp.Models;
using SecureUserApp.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureUserApp.Services
{
    public class UserService
    {
        private List<User> users = new List<User>();
        private EncryptionService encryption = new EncryptionService();

        public void Register(string username, string password, string email)
        {
            string hashed = PasswordHasher.HashPassword(password);
            string encryptedEmail = encryption.Encrypt(email);

            users.Add(new User
            {
                Username = username,
                HashedPassword = hashed,
                Email = encryptedEmail
            });
        }

        public bool Login(string username, string password)
        {
            string hashed = PasswordHasher.HashPassword(password);

            var user = users.FirstOrDefault(u => u.Username == username && u.HashedPassword == hashed);

            return user != null;
        }
    }
}
