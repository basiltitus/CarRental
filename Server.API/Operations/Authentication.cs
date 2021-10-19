using Microsoft.Extensions.Configuration;
using Server.API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
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
            SqlCommand command;
            command = new SqlCommand("sp_login", sqlConnection);
            command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = userName;
            command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            var response = command.ExecuteScalar();
            int userId;
            if (response != null)
                userId = (int)response;
            else
                userId = 0;
            sqlConnection.Close();
            UserProfile userProfile = new UserProfile();
            userProfile.userId = userId;
            if (userId == 0)
            {
                userProfile.token = "unavailable";
                userProfile.userId = 0;
                return userProfile;
            }
            else 
            {
                string token;
                if (userName.Contains("admin"))
                {

                    userProfile.token = JSONWebToken.GenerateJSONWebToken(userName, "Admin");
                    return userProfile;
                }
                else
                {
                    userProfile.token = JSONWebToken.GenerateJSONWebToken(userName, "Customer");
                    return userProfile;
                }
            } 
            }
        public UserProfile SignUp(User user)
        {
            SqlCommand commandCheck;
            commandCheck = new SqlCommand("sp_userexist", sqlConnection);
            commandCheck.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = user.UserName;
            commandCheck.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            var response = (int)commandCheck.ExecuteScalar();
            sqlConnection.Close();

            UserProfile userProfile = new UserProfile();
            if (response >0)
            {
                userProfile.userId = 0;
                userProfile.token = "unavailable";
                return userProfile;
            }
            SqlCommand commandInsert;
            commandInsert = new SqlCommand("sp_signup", sqlConnection);
            commandInsert.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = user.UserName;
            commandInsert.Parameters.Add("@Password", SqlDbType.NVarChar).Value = user.Password;
            commandInsert.Parameters.Add("@AadharNo", SqlDbType.NVarChar).Value = user.AadharNumber;
            commandInsert.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            int userId;
           userId= Convert.ToInt32(commandInsert.ExecuteScalar());
            
            sqlConnection.Close();
            userProfile.userId = userId;
            if (userId >= 1000)
            {
                string token;
                if (user.UserName.Contains("admin"))
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
    }
}
