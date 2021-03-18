using System.Collections.Generic;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using TutorMeFMI.App.Auth.Model;

namespace TutorMeFMI.App.Auth.Dal
{
    public class JwtUtils
    {
        public static string GenerateUserToken(DbUser user)
        {
            var payload = new Dictionary<string, object>
            {
                {"email", user.Email},
                {"password", user.Password}
            };
            var algorithm = new HMACSHA256Algorithm();
            var serializer = new JsonNetSerializer();
            var urlEncoder = new JwtBase64UrlEncoder();
            var encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            var token = encoder.Encode(payload, Secrets.JwtSecret);
            
            return token;
        }

        public static User GetUserFromToken(string tokem)
        {
            var algorithm = new HMACSHA256Algorithm();
            var serializer = new JsonNetSerializer();
            var provider = new UtcDateTimeProvider();
            var urlEncoder = new JwtBase64UrlEncoder();
            var validator = new JwtValidator(serializer, provider);
            var decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
            var user = decoder.DecodeToObject(tokem, Secrets.JwtSecret, verify: true);
            return new User() {Email = user["email"].ToString(), Password = user["password"].ToString()};
        }
    }
}