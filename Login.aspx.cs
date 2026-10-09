using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;

namespace WebForm
{
    public partial class Login : System.Web.UI.Page
    {

        private string GenerateJwtToken(int userId, string username, string role)
        {
            string secretKey = "MyVeryStrongSecretKeyForJWT123456789";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub , userId.ToString()),
                new Claim("name",username),
                new Claim(ClaimTypes.Role , role)
            };

            var token =  new JwtSecurityToken(
                issuer: "StudentRegistrationApp",
                audience: "StudentAppUsers",
                claims : claims,
                expires:DateTime.UtcNow.AddMinutes(30),
                signingCredentials : credentials
                );


            return new JwtSecurityTokenHandler()
                .WriteToken(token);

        }
        protected void btnLogin_Click(object Sender, EventArgs args)
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text;
            string connectionstring = ConfigurationManager.ConnectionStrings["StudentDBConnection"].ConnectionString;
            string role = null;
            int userId = 0;

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                string query = @"SELECT Id, Role
                 FROM Users
                 WHERE UserName = @Username
                 AND Password = @Password";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();

                using(SqlDataReader reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        userId = Convert.ToInt32(reader["Id"]);

                        role = reader["role"].ToString();
                    }
                }
            }
            if(role==null)
            {
                lblMessage.Text = "Invalid Username  or password";
                return;
            }

            // Login has been succesfull
                string token = GenerateJwtToken(userId, username, role);
            // Remember the logged-in user for Web Forms pages.
            Session["UserId"] = userId;
            Session["Username"] = username;
            Session["Role"] = role;
            Session["JwtToken"] = token;

            // Redirect to the student registration page.
            Response.Redirect("~/Default.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
            return;
            lblMessage.Text = "Login Successfull <br/><br/>" + token;


            //// TEMPORARY: display token for API testing.
            //lblMessage.Text = "Login successful. Copy the token below.";
            //txtToken.Text = token;
            //return;



        }

    }
}