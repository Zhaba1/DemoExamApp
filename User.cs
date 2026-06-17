namespace DemoExamApp
{
    /// <summary>
    /// Модель авторизованного пользователя.
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBlocked { get; set; }

        public string RoleText => IsAdmin ? "admin" : "user";
    }
}
