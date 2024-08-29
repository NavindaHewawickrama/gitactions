namespace TestAuthBack.Controllers.Models
{
    public class Registration
    {

        public int ID { get; set; }

        required
        public string Username { get; set; }
        required
        public string Passwords { get; set; }
        required
        public string Email { get; set; }
    }
}
