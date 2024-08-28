using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TestAuthBack.Controllers.Models;

namespace TestAuthBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public RegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;   
        }

        [HttpPost]
        [Route("registration")]

        public string registration(Registration registration)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DataConnection").ToString());
            SqlCommand cmd = new SqlCommand("INSERT INTO Registration(Username,Passwords,Email) VALUES('"+registration.Username+"','"+registration.Passwords+"','"+registration.Email+"')",con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return "data inserted";
            }
            else
            {
                return "error data not inserted";
            }
            
        }


        [HttpPost]
        [Route("login")]

        public string login(Login login)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DataConnection").ToString());

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Registration WHERE Email = '" + login.Email+"' AND Passwords = '"+login.Passwords+"' ",con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return "Valid User";
            }
            else
            {
                return "Invalid User";
            }
            
            
        }
    }
}
