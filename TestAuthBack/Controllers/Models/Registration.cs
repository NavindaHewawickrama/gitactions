namespace TestAuthBack.Controllers.Models
{
    public class Registration
    {

        public int ID { get; set; }

        required
        public string Username { get; set; }
        public string Passwords { get; set; }
        public string Email { get; set; }
    }
}
