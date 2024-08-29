//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Data;
//using System.Data.SqlClient;
//using TestAuthBack.Controllers.Models;

//namespace TestAuthBack.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class RegistrationController : ControllerBase
//    {
//        private readonly IConfiguration _configuration;

//        public RegistrationController(IConfiguration configuration)
//        {
//            _configuration = configuration;   
//        }

//        [HttpPost]
//        [Route("registration")]

//        public string registration(Registration registration)
//        {
//            if(_configuration != null)
//            {
//                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DataConnection").ToString());
//                SqlCommand cmd = new SqlCommand("INSERT INTO Registration(Username,Passwords,Email) VALUES('" + registration.Username + "','" + registration.Passwords + "','" + registration.Email + "')", con);
//                con.Open();
//                int i = cmd.ExecuteNonQuery();
//                con.Close();
//                if (i > 0)
//                {
//                    return "data inserted";
//                }
//                else
//                {
//                    return "error data not inserted";
//                }
//            }
//            else
//            {
//                return "configuration is null";
//            }


//        }


//        [HttpPost]
//        [Route("login")]

//        public string login(Login login)
//        {
//            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DataConnection").ToString());

//            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Registration WHERE Email = '" + login.Email+"' AND Passwords = '"+login.Passwords+"' ",con);
//            DataTable dt = new DataTable();
//            da.Fill(dt);
//            if (dt.Rows.Count > 0)
//            {
//                return "Valid User";
//            }
//            else
//            {
//                return "Invalid User";
//            }


//        }
//    }
//}

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
        public IActionResult Registration([FromBody] Registration registration)
        {
            if (registration == null)
            {
                return BadRequest("Registration data is null");
            }

            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DataConnection")))
                {
                    string query = "INSERT INTO Registration (Username, Passwords, Email) VALUES (@Username, @Passwords, @Email)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Username", registration.Username ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Passwords", registration.Passwords ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", registration.Email ?? (object)DBNull.Value);

                        con.Open();
                        int result = cmd.ExecuteNonQuery();
                        con.Close();

                        if (result > 0)
                        {
                            return Ok("Data inserted");
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError, "Error inserting data");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] Login login)
        {
            if (login == null)
            {
                return BadRequest("Login data is null");
            }

            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DataConnection")))
                {
                    string query = "SELECT * FROM Registration WHERE Email = @Email AND Passwords = @Passwords";
                    using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@Email", login.Email ?? (object)DBNull.Value);
                        da.SelectCommand.Parameters.AddWithValue("@Passwords", login.Passwords ?? (object)DBNull.Value);

                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            return Ok("Valid User");
                        }
                        else
                        {
                            return Unauthorized("Invalid User");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}

