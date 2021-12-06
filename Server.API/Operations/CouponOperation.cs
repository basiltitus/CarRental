using Microsoft.Extensions.Configuration;
using Server.API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Server.API.Operations
{
    public class CouponOperation
    {
        SqlConnection sqlConnection;
        string conStr;
        private IConfiguration Configuration;
        Authentication auth;
        public CouponOperation(IConfiguration _configuration)
        {
            Configuration = _configuration;
            conStr = this.Configuration.GetConnectionString("CarRentDB");
            sqlConnection = new SqlConnection(conStr);
            auth = new Authentication(_configuration);

        }
        public string GetUserName(int id)
        {
            User user = auth.GetUser(id);
            return user.Name;
        }
        public List<Coupon> GetCoupons()
        {
            SqlCommand command = new SqlCommand("sp_getcoupons", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            SqlDataReader rdr = command.ExecuteReader();
            List<Coupon> Coupons = new List<Coupon>();
            while (rdr.Read())
            {
            Coupon coupon= new Coupon();
                coupon.CouponId = Convert.ToInt32(rdr["CouponId"].ToString());
                coupon.CouponName = rdr["CouponName"].ToString().Trim();
                coupon.MinOrderAmount = Convert.ToInt32(rdr["MinOrderAmount"].ToString());
                coupon.PercentageDiscount = Convert.ToInt32(rdr["PercentageDiscount"].ToString());
                coupon.MaxDiscount = Convert.ToInt32(rdr["MaxDiscount"].ToString());
                coupon.Active =Convert.ToBoolean(rdr["Active"].ToString());
                coupon.CreatedBy =Convert.ToInt32(rdr["CreatedBy"].ToString());
                coupon.CreatedOn = Convert.ToDateTime(rdr["CreatedOn"].ToString());
                Coupons.Add(coupon);
            }
            sqlConnection.Close();
            foreach(var item in Coupons)
            {
                item.CreatedByName = this.GetUserName(item.CreatedBy);
            }
            return Coupons;
        }
        public Coupon GetCoupon(int id)
        {
            SqlCommand command = new SqlCommand("sp_getcoupon", sqlConnection);

            command.Parameters.Add("@CouponId", SqlDbType.Int).Value = id;
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            SqlDataReader rdr = command.ExecuteReader();
            Coupon coupon = new Coupon();
            while (rdr.Read())
            {
                
                coupon.CouponId = Convert.ToInt32(rdr["CouponId"].ToString());
                coupon.CouponName = rdr["CouponName"].ToString().Trim();
                coupon.MinOrderAmount = Convert.ToInt32(rdr["MinOrderAmount"].ToString());
                coupon.PercentageDiscount = Convert.ToInt32(rdr["PercentageDiscount"].ToString());
                coupon.MaxDiscount = Convert.ToInt32(rdr["MaxDiscount"].ToString());
                coupon.Active = Convert.ToBoolean(rdr["Active"].ToString());
                coupon.CreatedBy = Convert.ToInt32(rdr["CreatedBy"].ToString());
                coupon.CreatedOn = Convert.ToDateTime(rdr["CreatedOn"].ToString());
            }
            sqlConnection.Close();
            return coupon;
        }
        public bool AddCoupon(Coupon coupon)
        {
            SqlCommand command = new SqlCommand("sp_insertCoupon", sqlConnection);
            command.Parameters.Add("@CouponName", SqlDbType.NVarChar).Value = coupon.CouponName;
            command.Parameters.Add("@MinOrderAmount", SqlDbType.Int).Value = coupon.MinOrderAmount;
            command.Parameters.Add("@PercentageDiscount", SqlDbType.Int).Value = coupon.PercentageDiscount;
            command.Parameters.Add("@MaxDiscount", SqlDbType.Int).Value = coupon.MaxDiscount;
            command.Parameters.Add("@Active", SqlDbType.Bit).Value = coupon.Active;
            command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = coupon.CreatedBy;
            command.Parameters.Add("@CreatedOn", SqlDbType.Date).Value = coupon.CreatedOn;
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            int response = (int)command.ExecuteNonQuery();
            sqlConnection.Close();
            if (response == 1)
                return true;
            else
                return false;
        }
        public bool EditCoupon(Coupon coupon)
        {
            SqlCommand command = new SqlCommand("sp_editCoupon", sqlConnection);
            command.Parameters.Add("@CouponId", SqlDbType.Int).Value = coupon.CouponId;
            command.Parameters.Add("@CouponName", SqlDbType.NVarChar).Value = coupon.CouponName;
            command.Parameters.Add("@MinOrderAmount", SqlDbType.Int).Value = coupon.MinOrderAmount;
            command.Parameters.Add("@PercentageDiscount", SqlDbType.Int).Value = coupon.PercentageDiscount;
            command.Parameters.Add("@MaxDiscount", SqlDbType.Int).Value = coupon.MaxDiscount;
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            int response = (int)command.ExecuteNonQuery();
            sqlConnection.Close();
            if (response == 1)
                return true;
            else
                return false;
        }
        public bool ChangeCouponStatus(int id,bool status)
        {
            SqlCommand command = new SqlCommand("sp_changecouponstatus", sqlConnection);
            command.Parameters.Add("@CouponId", SqlDbType.Int).Value =id;
            command.Parameters.Add("@Active", SqlDbType.Bit).Value = status;
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            int response = (int)command.ExecuteNonQuery();
            sqlConnection.Close();
            if (response == 1)
                return true;
            else
                return false;
        }
        
    }
}
