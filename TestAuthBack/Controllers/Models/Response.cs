namespace TestAuthBack.Controllers.Models
{
    public class Response
    {

        public int statusCode {  get; set; }

        required
        public string statusMessage { get; set; }
    }
}
