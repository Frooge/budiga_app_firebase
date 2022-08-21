using budiga_app.MVVM.Model;

namespace budiga_app.Core
{
    public static class Sessions
    {
        public static UserModel session { get; set; }

        public static void Dispose()
        {
            session = new UserModel();
        }
    }
}
