namespace MyBlockForumServer.Tools
{
    public class RequestValidator
    {
        public static bool IdValidate(int id)
        {
            return id >= 0;
        }

        public static bool ObjectValidate(object obj)
        {
            return obj is not null;
        }
    }
}
