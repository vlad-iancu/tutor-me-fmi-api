namespace TutorMeFMI.App.Auth.Model
{
    public class User
    {
        public string Email { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string ProfilePath { get; set; }
        
        public static implicit operator User(DbUser dbUser)
        {
            return new()
            {
                Email = dbUser.Email, Id = dbUser.Id, Name = dbUser.Name, ProfilePath = dbUser.ProfilePath
            };
        }
    }
    
}