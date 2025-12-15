namespace Domain.Entities
{
    /// <summary>
    /// DoMain的职责是定义核心业务实体，以及接口
    /// </summary>

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public User(string Name, string Email, string Password)
        {
            this.Name = Name;
            this.Email = Email;
            this.Password = Password;
        }

        public int GetUserPasswordLength()
        {
            return Password.Length;
        }
    }
}
