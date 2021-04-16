namespace TutorMeFMI.App.Auth.Model
{
    public class DbUser
    {
        [SqlKata.Column("id")]
        public int Id { get; set; }

        [SqlKata.Column("name")]
        public string Name { get; set; }

        [SqlKata.Column("email")]
        public string Email { get; set; }

        [SqlKata.Column("password")]
        public string Password { get; set; }
        [SqlKata.Column("profilePath")]
        public string ProfilePath { get; set; }

        public static implicit operator DbUser(RegisterRequest request)
        {
            return new DbUser {Id = 0, Email = request.email, Name = request.name, Password = request.password};
        }
    }
}