namespace MyBlockForumServer.Auth
{
    public class LoginRequestDetails
    {
        public required string Login { get; set; }

        public required string Password { get; set; }
    }
}
