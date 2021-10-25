using Microsoft.Extensions.Configuration;
using Server.API.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using Server.Library;
namespace Server.API.Operations
{
    public class Authentication
    {
        SqlConnection sqlConnection;
        string conStr;
        private IConfiguration Configuration;
        public Authentication(IConfiguration _configuration)
        {
            Configuration = _configuration;
            conStr = this.Configuration.GetConnectionString("CarRentDB");
            sqlConnection = new SqlConnection(conStr);

        }
        public UserProfile SignIn(string userName, string password)
        {
            SqlCommand command = new SqlCommand("sp_login", sqlConnection);
            command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = userName;
            command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            SqlDataReader rdr = command.ExecuteReader();
            int userId=0;
            string role="";
            if (!rdr.HasRows)
            {
                userId = 0;
            }
            while (rdr.Read())
            {
                userId = Convert.ToInt32(rdr["UserId"].ToString());
                role = rdr["Role"].ToString();
            }
            sqlConnection.Close();
            UserProfile userProfile = new UserProfile
            {
                userId = userId
            };
            if (userId == 0)
            {
                userProfile.token = "unavailable";
                userProfile.userId = 0;
                userProfile.role = "customer";
                return userProfile;
            }
            else
            {
                if (role=="admin")
                {

                    userProfile.token = JSONWebToken.GenerateJSONWebToken(userName, "Admin");
                    userProfile.role = "admin";
                    return userProfile;
                }
                else
                {
                    userProfile.token = JSONWebToken.GenerateJSONWebToken(userName, "Customer");
                    userProfile.role = "customer";
                    return userProfile;
                }
            }
        }
        public UserProfile SignUp(User user)
        {
            SqlCommand commandCheck = new SqlCommand("sp_userexist", sqlConnection);
            commandCheck.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = user.UserName;
            commandCheck.Parameters.Add("@Role", SqlDbType.NVarChar).Value = user.Role;
            commandCheck.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            var response = (int)commandCheck.ExecuteScalar();
            sqlConnection.Close();

            UserProfile userProfile = new UserProfile();
            if (response > 0)
            {
                userProfile.userId = 0;
                userProfile.token = "unavailable";
                return userProfile;
            }
            SqlCommand commandInsert = new SqlCommand("sp_signup", sqlConnection);
            commandInsert.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = user.UserName;
            commandInsert.Parameters.Add("@Password", SqlDbType.NVarChar).Value = user.Password;
            commandInsert.Parameters.Add("@AadharNo", SqlDbType.NVarChar).Value = user.AadharNumber;
            commandInsert.Parameters.Add("@Role", SqlDbType.NVarChar).Value = user.Role;
            commandInsert.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            SqlDataReader rdr = commandInsert.ExecuteReader();
            int userId = 0;
            string role = "";
            if (!rdr.HasRows) {
                userId = 0;
                role = "customer";
            }
            while (rdr.Read())
            {
                userId = Convert.ToInt32(rdr["UserId"].ToString());
                role = rdr["Role"].ToString();
            } sqlConnection.Close();
            userProfile.userId = userId;
            userProfile.role = role;
            if (userId >= 1000)
            {
                if (userProfile.role=="admin")
                {
                    userProfile.token = JSONWebToken.GenerateJSONWebToken(user.UserName, "Admin");
                    
                    return userProfile;
                }
                else
                {
                    userProfile.token = JSONWebToken.GenerateJSONWebToken(user.UserName, "Customer");
                    return userProfile;
                }

            }
            else
            {
                userProfile.token = "unavailable";
                return userProfile;
            }
        }
        public bool CheckSecurityIdentity(SecurityTable securityIdentity)
        {
            if (securityIdentity.SecurityCode == "123123")
            {
                return true;
            }
            return false;
        }
    }
}
