using System.Data.SqlClient;
namespace WebAppInsurancePolicy.Models
{
   
    using System.Data.SqlClient;
    using static WebAppInsurancePolicy.Models.SecurityService;



   
        public class DataService
        {
            IConfiguration config;
            SqlConnection con;
            SqlCommand? cmd;
            public DataService(IConfiguration _config)
            {
                this.config = _config;
                con = new SqlConnection();
                con.ConnectionString = config["ConnectionStrings:cstr"];
            }





            public string? FindUser(OperatorLoginModel model)
            {
                string? role = null;
                cmd = new SqlCommand();
                cmd.Connection = con;





                cmd.CommandText = "select role from Users where username=@user and password=@pwd";





                cmd.Parameters.AddWithValue("@user", model.username);
                cmd.Parameters.AddWithValue("@pwd", model.password);
                /*cmd.CommandText = "select Email from Users where Email=@email and Password=@pwd";





                cmd.Parameters.AddWithValue("@email", model.Email);
                cmd.Parameters.AddWithValue("@pwd", model.Password);*/
                con.Open();
                var result = cmd.ExecuteScalar();
                if (result != null) { role = result.ToString(); }
                con.Close();
                return role;
            }
        }
    }


