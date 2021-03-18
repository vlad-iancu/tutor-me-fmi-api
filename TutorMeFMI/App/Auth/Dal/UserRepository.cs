using System.Linq;
using SqlKata.Execution;
using TutorMeFMI.App.Auth.Model;
using TutorMeFMI.Data;

namespace TutorMeFMI.App.Auth.Dal
{
    public class UserRepository : IUserDataAccess
    {
        public DbUser GetUser(int id)
        {
            throw new System.NotImplementedException();
        }

        public DbUser GetUser(string email, string password)
        {
            using var database = new Database().GetQueryFactory();
            var user = database.Query("user").Where("email", "=", email).Get<DbUser>().FirstOrDefault();
            var hash = user?.Password;
            if (user != null) user.Password = password;
            else return null;
            return BCrypt.Net.BCrypt.Verify(password, hash) ? user : null;
        }

        public int AddUser(DbUser user)
        {
            using var database = new Database().GetQueryFactory();
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            return database.Query("user").InsertGetId<int>(new
            {
                name = user.Name, email = user.Email, password = passwordHash
            });
        }

        public void DeleteUser(DbUser user)
        {
            throw new System.NotImplementedException();
        }
    }
}