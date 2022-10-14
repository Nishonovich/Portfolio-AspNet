namespace Portfolio.WebApi.Security
{
    public class PasswordHasher
    {
        private static string _key = "80b3670b-47d7-459a-a0f1-b7e95d21305b";

        public static (string Hash, string Salt) Hash(string password)
        {
            string salt = Guid.NewGuid().ToString();
            string hash = BCrypt.Net.BCrypt.HashPassword(salt + password + _key);
            return (hash, salt);
        }
        public static bool Verify(string password, string salt, string hash)
            => BCrypt.Net.BCrypt.Verify(salt + password + _key, hash);
    }
}
