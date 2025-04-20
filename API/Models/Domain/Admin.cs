namespace API.Models.Domain
{
    public class Admin
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; } = "Admin";
        public DateTime CreatedDate { get; set; }
    }
}