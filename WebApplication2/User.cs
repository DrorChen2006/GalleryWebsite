namespace WebApplication2
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }

        public User(int userId, string username, string email, string hashedPassword)
        {
            UserId = userId;
            Username = username;
            Email = email;
            HashedPassword = hashedPassword;
        }
    }


}
