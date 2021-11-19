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
        public UserProfile SignIn(string emailId, string password)
        {
            SqlCommand command = new SqlCommand("sp_login", sqlConnection);
            command.Parameters.Add("@EmailId", SqlDbType.NVarChar).Value = emailId;
            command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            SqlDataReader rdr = command.ExecuteReader();
            int userId = 0;
            string role = "";
            string name = "";
            string imgurl = "";
            if (!rdr.HasRows)
            {
                userId = 0;
            }
            while (rdr.Read())
            {
                userId = Convert.ToInt32(rdr["UserId"].ToString());
                role = rdr["Role"].ToString().Trim();
                name = rdr["Name"].ToString().Trim();
                imgurl = rdr["ImgUrl"].ToString().Trim();
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
                if (role == "admin")
                {
                    userProfile.Name = name;
                    userProfile.ImgUrl = imgurl;
                    userProfile.token = JSONWebToken.GenerateJSONWebToken(emailId, "Admin");
                    userProfile.role = "admin";
                    return userProfile;
                }
                else
                {
                    userProfile.Name = name;
                    userProfile.ImgUrl = imgurl;
                    userProfile.token = JSONWebToken.GenerateJSONWebToken(emailId, "Customer");

                    userProfile.role = "customer";
                    return userProfile;
                }
            }
        }
        public UserProfile SignUp(User user)
        {
            SqlCommand commandCheck = new SqlCommand("sp_userexist", sqlConnection);
            commandCheck.Parameters.Add("@EmailId", SqlDbType.NVarChar).Value = user.EmailId;
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
            commandInsert.Parameters.Add("@EmailId", SqlDbType.NVarChar).Value = user.EmailId;
            commandInsert.Parameters.Add("@Password", SqlDbType.NVarChar).Value = user.Password;
            commandInsert.Parameters.Add("@LicenseNumber", SqlDbType.NVarChar).Value = user.LicenseNumber;
            commandInsert.Parameters.Add("@Role", SqlDbType.NVarChar).Value = user.Role;
            commandInsert.Parameters.Add("@ImgUrl", SqlDbType.NVarChar).Value = user.ImgUrl;
            commandInsert.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar).Value = user.PhoneNumber;
            commandInsert.Parameters.Add("@DOB", SqlDbType.Date).Value = user.DOB;
            commandInsert.Parameters.Add("@Name", SqlDbType.NVarChar).Value = user.Name;
            commandInsert.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            SqlDataReader rdr = commandInsert.ExecuteReader();
            int userId = 0;
            string role = "";
            if (!rdr.HasRows)
            {
                userId = 0;
                role = "customer";
            }
            while (rdr.Read())
            {
                userId = Convert.ToInt32(rdr["UserId"].ToString().Trim());
                role = rdr["Role"].ToString().Trim();
            }
            sqlConnection.Close();
            userProfile.userId = userId;
            userProfile.role = role;
            userProfile.Name = user.Name;
            userProfile.ImgUrl = user.ImgUrl;
            if (userId >= 1000)
            {
                if (userProfile.role == "admin")
                {

                    userProfile.token = JSONWebToken.GenerateJSONWebToken(user.EmailId, "Admin");

                    return userProfile;
                }
                else
                {
                    userProfile.token = JSONWebToken.GenerateJSONWebToken(user.EmailId, "Customer");
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
        public bool CheckUserExists(string EmailId)
        {
            SqlCommand commandCheck = new SqlCommand("sp_userexist", sqlConnection);
            commandCheck.Parameters.Add("@EmailId", SqlDbType.NVarChar).Value = EmailId;
            commandCheck.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            var response = (int)commandCheck.ExecuteScalar();
            sqlConnection.Close();
            if (response > 0)
            {
                return true;
            }
            return false;
        }
        public User GetUser(int userId)
        {
            SqlCommand command = new SqlCommand("sp_getuserdetails", sqlConnection);
            command.Parameters.Add("@UserId", SqlDbType.NVarChar).Value = userId;
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            SqlDataReader rdr = command.ExecuteReader();
            User user = new User();
            while (rdr.Read())
            {
                user.UserId = Convert.ToInt32(rdr["UserId"].ToString());
                user.Role = rdr["Role"].ToString().Trim();
                user.Name = rdr["Name"].ToString().Trim();
                user.PhoneNumber = rdr["PhoneNumber"].ToString().Trim();
                user.LicenseNumber = rdr["LicenseNumber"].ToString().Trim();
                user.EmailId = rdr["EmailId"].ToString().Trim();
                user.ImgUrl = rdr["ImgUrl"].ToString().Trim();
                user.Password = rdr["Password"].ToString().Trim();
                user.DOB = Convert.ToDateTime(rdr["DOB"].ToString().Trim());
            }
            sqlConnection.Close();
            return user;
        }
        public void updateUser(User user)
        {
            SqlCommand command = new SqlCommand("sp_updateuser", sqlConnection);
            command.Parameters.Add("@UserId", SqlDbType.Int).Value = user.UserId;
            command.Parameters.Add("@EmailId", SqlDbType.NVarChar).Value = user.EmailId;
            command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = user.Password;
            command.Parameters.Add("@LicenseNumber", SqlDbType.NVarChar).Value = user.LicenseNumber;
            command.Parameters.Add("@Role", SqlDbType.NVarChar).Value = user.Role;
            command.Parameters.Add("@ImgUrl", SqlDbType.NVarChar).Value = user.ImgUrl;
            command.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar).Value = user.PhoneNumber;
            command.Parameters.Add("@DOB", SqlDbType.Date).Value = user.DOB;
            command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = user.Name;
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}
