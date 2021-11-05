using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Server.API.Models;
using System.Data;

namespace Server.API.Operations
{
    public class OrderOperation
    {
        SqlConnection sqlConnection;
        readonly string conStr;
        private IConfiguration Configuration;
        public OrderOperation(IConfiguration _configuration)
        {
            Configuration = _configuration;
            conStr = Configuration.GetConnectionString("CarRentDB");
            sqlConnection = new SqlConnection(conStr);
        }
        public int AddOrder(OrderTable order)
        {
            string[] res= this.GetCarAvailability(order.FromDate, order.ToDate, order.CarId);
            if (res[0] != "available")
                return 0;
            SqlCommand command = new SqlCommand("sp_addorder", sqlConnection);
            command.Parameters.Add("@CarId", SqlDbType.Int).Value = (int)order.CarId;
            command.Parameters.Add("@UserId", SqlDbType.Int).Value = (int)order.UserId;
            command.Parameters.Add("@FromDate", SqlDbType.Date).Value = order.FromDate;
            command.Parameters.Add("@ToDate", SqlDbType.Date).Value = order.ToDate;
            command.Parameters.Add("@ExtraDays", SqlDbType.Int).Value = order.ExtraDays;
            command.Parameters.Add("@Total", SqlDbType.Int).Value = order.Total;
            command.Parameters.Add("@Completed", SqlDbType.NVarChar).Value = order.Completed;
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            int response = Convert.ToInt32(command.ExecuteScalar());
            sqlConnection.Close();
            if (response >= 100)
                return response;
            else
                return 0;
        }
        public bool CompleteOrder(int orderId, int extraDays)
        {

            SqlCommand command = new SqlCommand("sp_completeOrder", sqlConnection);
            command.Parameters.Add("@OrderId", SqlDbType.Int).Value = orderId;
            command.Parameters.Add("@ExtraDays", SqlDbType.Int).Value = extraDays;
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            int response = command.ExecuteNonQuery();
            sqlConnection.Close();
            if (response >= 1)
                return true;
            else
                return false;
        }
        public bool MakePayment(int orderId)
        {
            SqlCommand command = new SqlCommand("sp_makepayment", sqlConnection);
            command.Parameters.Add("@OrderId", SqlDbType.Int).Value = orderId;
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            int response = command.ExecuteNonQuery();
            sqlConnection.Close();
            if (response >= 1)
                return true;
            else
                return false;
        }
        public OrderTable GetOrderDetails(int orderId,int userId)
        {
            SqlCommand command = new SqlCommand("sp_getorderdetail", sqlConnection);
            command.Parameters.Add("@OrderId", SqlDbType.Int).Value = (int)orderId;
            command.Parameters.Add("@UserId", SqlDbType.Int).Value = (int)userId;
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            SqlDataReader rdr = command.ExecuteReader();
            OrderTable order = new OrderTable();
            if (!rdr.HasRows)
            {
                return null;
            }
            while (rdr.Read())
            {
                order.OrderId = Convert.ToInt32(rdr["OrderId"]);
                order.UserId = Convert.ToInt32(rdr["UserId"]);
                order.CarId = Convert.ToInt32(rdr["CarId"]);
                order.FromDate = Convert.ToDateTime(rdr["FromDate"].ToString());
                order.ToDate = Convert.ToDateTime(rdr["ToDate"].ToString());
                order.Total = Convert.ToInt32(rdr["Total"]);
                order.ExtraDays = Convert.ToInt32(rdr["ExtraDays"]);
                order.Completed = rdr["Completed"].ToString();
            }
            sqlConnection.Close();
            return order;
        }
        public List<OrderTable> GetOrderDetailsByUserId(int userId)
        {
            List<OrderTable> orderList = new List<OrderTable>();
            SqlCommand command = new SqlCommand("sp_carjoinorderbyuserid", sqlConnection);
            command.Parameters.Add("@UserId", SqlDbType.Int).Value = (int)userId;
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            SqlDataReader rdr = command.ExecuteReader();
            while (rdr.Read())
            {
                OrderTable order = new OrderTable
                {
                    OrderId = Convert.ToInt32(rdr["OrderId"]),
                    UserId = Convert.ToInt32(rdr["UserId"]),
                    CarId = Convert.ToInt32(rdr["CarId"])
                };
                order.Cardetail.CarName = Convert.ToString(rdr["CarName"]);
                order.Cardetail.CarTransmission = (CarTransmission) Convert.ToInt32(rdr["CarTransmission"]);
                order.Cardetail.CarCount = Convert.ToInt32(rdr["CarCount"]);
                order.Cardetail.CarType = (CarVarient)Convert.ToInt32(rdr["CarType"]);
                order.Cardetail.ChargePerDay = Convert.ToInt32(rdr["ChargePerDay"]);
                order.FromDate = Convert.ToDateTime(rdr["FromDate"].ToString());
                order.ToDate = Convert.ToDateTime(rdr["ToDate"].ToString());
                order.Total = Convert.ToInt32(rdr["Total"]);
                order.ExtraDays = Convert.ToInt32(rdr["ExtraDays"]);
                order.Completed = rdr["Completed"].ToString();
                orderList.Add(order);
            }
            sqlConnection.Close();
            return orderList;
        }
        public string[] GetCarAvailability(DateTime FromDate,DateTime ToDate,int CarId)
        {
            SqlCommand command = new SqlCommand("sp_checkcaravailable", sqlConnection);
            command.Parameters.Add("@CarId", SqlDbType.Int).Value = (int)CarId;
            command.Parameters.Add("@FromDate", SqlDbType.Date).Value = FromDate;
            command.Parameters.Add("@ToDate", SqlDbType.Date).Value = ToDate;
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            string response= (string)command.ExecuteScalar();
            sqlConnection.Close();
            string[] res = {"res"};
            res[0] = response;
            return res;
                
        }
    }
}
