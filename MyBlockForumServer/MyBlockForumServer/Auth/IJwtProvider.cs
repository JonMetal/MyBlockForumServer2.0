using MyBlockForumServer.Database.Entities;

namespace MyBlockForumServer.Auth
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}