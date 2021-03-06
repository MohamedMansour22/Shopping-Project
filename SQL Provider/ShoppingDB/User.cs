using SQL_Provider.Enums;

namespace SQL_Provider.ShoppingDB
{

    public class User
    {
        public Guid ID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime Birthdate { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
