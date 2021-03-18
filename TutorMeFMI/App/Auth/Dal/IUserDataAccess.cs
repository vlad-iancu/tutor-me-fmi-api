using TutorMeFMI.App.Auth.Model;

namespace TutorMeFMI.App.Auth.Dal
{
    public interface IUserDataAccess
    {
        public DbUser GetUser(int id);
        public DbUser GetUser(string email, string password);
        public int AddUser(DbUser user);   
        public void DeleteUser(DbUser user);
    }
}