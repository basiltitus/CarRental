using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Server.API.Models;
using System.Data;
using Server.API.Models.ViewModels;
using CarTransmission = Server.API.Models.ViewModels.CarTransmission;
using CarVarient = Server.API.Models.ViewModels.CarVarient;

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
        public int AddOrder(Order order)
        {
           /* string[] res= this.GetCarAvailability(order.FromDate, order.ToDate, order.CarId);
            if (res[0] != "available")
                return 0;*/
            SqlCommand command = new SqlCommand("sp_addorder", sqlConnection);
            command.Parameters.Add("@CarId", SqlDbType.Int).Value = (int)order.CarId;
            command.Parameters.Add("@UserId", SqlDbType.Int).Value = (int)order.UserId;
            command.Parameters.Add("@FromDate", SqlDbType.Date).Value = order.FromDate;
            command.Parameters.Add("@ToDate", SqlDbType.Date).Value = order.ToDate;
            command.Parameters.Add("@ExtraDays", SqlDbType.Int).Value = order.ExtraDays;
            command.Parameters.Add("@Total", SqlDbType.Int).Value = order.Total;
            command.Parameters.Add("@Status", SqlDbType.NVarChar).Value = order.Status;
            command.Parameters.Add("@FineAmount", SqlDbType.Int).Value = order.FineAmount;
            command.Parameters.Add("@OrderDate", SqlDbType.Date).Value = order.OrderDate;
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            int response = Convert.ToInt32(command.ExecuteScalar());
            sqlConnection.Close();
            if (response >= 100)
                return response;
            else
                return 0;
        }
        public bool CompleteOrder(int orderId)
        {

            SqlCommand command = new SqlCommand("sp_completeOrder", sqlConnection);
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
        public Order GetOrderDetails(int orderId,int userId)
        {
            SqlCommand command = new SqlCommand("sp_getorderdetail", sqlConnection);
            command.Parameters.Add("@OrderId", SqlDbType.Int).Value = (int)orderId;
            command.Parameters.Add("@UserId", SqlDbType.Int).Value = (int)userId;
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            SqlDataReader rdr = command.ExecuteReader();
            Order order = new Order();
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
                order.Status = rdr["Status"].ToString();
                order.FineAmount = Convert.ToInt32(rdr["FineAmount"]);
                order.OrderDate = Convert.ToDateTime(rdr["OrderDate"].ToString());
            }
            sqlConnection.Close();
            return order;
        }
        public ReceiptVM GetReciept(int orderId,int userId)
        {
            SqlCommand command = new SqlCommand("sp_getorderdetailjoined", sqlConnection);
            command.Parameters.Add("@OrderId", SqlDbType.Int).Value = (int)orderId;
            command.Parameters.Add("@UserId", SqlDbType.Int).Value = (int)userId;
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            SqlDataReader rdr = command.ExecuteReader();
            ReceiptVM receipt= new ReceiptVM();
            if (!rdr.HasRows)
            {
                return null;
            }
            while (rdr.Read())
            {
                receipt.UserId = Convert.ToInt32(rdr["UserId"]);
                receipt.Name = rdr["Name"].ToString();
                receipt.DOB = Convert.ToDateTime(rdr["DOB"].ToString());
                receipt.PhoneNumber = rdr["PhoneNumber"].ToString();
                receipt.LicenseNumber = rdr["LicenseNumber"].ToString();
                receipt.CarId = Convert.ToInt32(rdr["CarId"]);
                receipt.RegNo = rdr["RegNo"].ToString();
                receipt.Colour = rdr["Colour"].ToString();
                receipt.ImgUrl = rdr["ImgUrl"].ToString();
                receipt.CarModelId = Convert.ToInt32(rdr["CarModelId"]);
                receipt.CarName = rdr["CarName"].ToString();
                receipt.CarTransmission = (CarTransmission)Enum.Parse(typeof(CarTransmission), rdr["CarTransmission"].ToString());
                receipt.CarType = (CarVarient)Enum.Parse(typeof(CarVarient), rdr["CarType"].ToString());
                receipt.SeatCount = Convert.ToInt32(rdr["SeatCount"]);
                receipt.OrderId = Convert.ToInt32(rdr["OrderId"]);
                receipt.FromDate = Convert.ToDateTime(rdr["FromDate"].ToString());
                receipt.ToDate = Convert.ToDateTime(rdr["ToDate"].ToString());
                receipt.Total = Convert.ToInt32(rdr["Total"]);
                receipt.Status = rdr["Status"].ToString();
                receipt.FineAmount = Convert.ToInt32(rdr["FineAmount"]);
                receipt.ExtraDays = Convert.ToInt32(rdr["ExtraDays"]);
                receipt.PaymentId = Convert.ToInt32(rdr["PaymentId"]);
                receipt.OrderDate = Convert.ToDateTime(rdr["OrderDate"].ToString());
                receipt.ChargePerDay = Convert.ToInt32(rdr["ChargePerDay"]);
            }
            sqlConnection.Close();
            return receipt;
        }
        public List<OrderHistoryVM> GetOrderDetailsByUserId(int userId)
        {
            List<OrderHistoryVM> orderList = new List<OrderHistoryVM>();
            SqlCommand command = new SqlCommand("sp_getorderforuseridjoined", sqlConnection);
            command.Parameters.Add("@UserId", SqlDbType.Int).Value = (int)userId;
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            SqlDataReader rdr = command.ExecuteReader();
            while (rdr.Read())
            {
                OrderHistoryVM order = new OrderHistoryVM();

                order.OrderId = Convert.ToInt32(rdr["OrderId"]);
                    order.CarModelId = Convert.ToInt32(rdr["CarModelId"]);
                order.CarId = Convert.ToInt32(rdr["CarId"]);
                order.OrderDate = Convert.ToDateTime(rdr["OrderDate"]);
                order.Status = rdr["status"].ToString();
                order.FromDate = Convert.ToDateTime(rdr["FromDate"]);
                order.ToDate = Convert.ToDateTime(rdr["ToDate"]);
                order.CarName = rdr["CarName"].ToString();
                order.ImgUrl = rdr["ImgUrl"].ToString();
                order.Total = Convert.ToInt32(rdr["Total"]);
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

        public bool RequestReturn(int id)
        {
            SqlCommand command = new SqlCommand("sp_requestreturn", sqlConnection);
            command.Parameters.Add("@OrderId", SqlDbType.Int).Value = id;
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            int response = command.ExecuteNonQuery();
            sqlConnection.Close();
            if (response >= 1)
                return true;
            else
                return false;
        }
        public List<AdminRequestVM> GetAdminRequests()
        {
            List<AdminRequestVM> orderList = new List<AdminRequestVM>();
            SqlCommand command = new SqlCommand("sp_getadminrequests", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            SqlDataReader rdr = command.ExecuteReader();
            while (rdr.Read())
            {
                AdminRequestVM order = new AdminRequestVM();

                order.OrderId = Convert.ToInt32(rdr["OrderId"]);
                order.CarModelId = Convert.ToInt32(rdr["CarModelId"]);
                order.CarId = Convert.ToInt32(rdr["CarId"]);
                order.UserId = Convert.ToInt32(rdr["UserId"]);
                order.ChargePerDay = Convert.ToInt32(rdr["ChargePerDay"]);
                order.OrderDate = Convert.ToDateTime(rdr["OrderDate"]);
                order.FromDate = Convert.ToDateTime(rdr["FromDate"]);
                order.ToDate = Convert.ToDateTime(rdr["ToDate"]);
                order.CarName = rdr["CarName"].ToString();
                order.ImgUrl = rdr["ImgUrl"].ToString();
                order.Colour=rdr["Colour"].ToString();
                order.RegNo = rdr["RegNo"].ToString();
                order.Name = rdr["Name"].ToString();
                order.PhoneNumber = rdr["PhoneNumber"].ToString();
                order.Status= rdr["Status"].ToString();
                orderList.Add(order);
            }
            sqlConnection.Close();
            return orderList;
        }
        public bool UpdateOrder(Order order)
        {
            SqlCommand command = new SqlCommand("sp_updateorder", sqlConnection);
            command.Parameters.Add("@OrderId", SqlDbType.Int).Value = (int)order.OrderId;
            command.Parameters.Add("@CarId", SqlDbType.Int).Value = (int)order.CarId;
            command.Parameters.Add("@UserId", SqlDbType.Int).Value = (int)order.UserId;
            command.Parameters.Add("@FromDate", SqlDbType.Date).Value = order.FromDate;
            command.Parameters.Add("@ToDate", SqlDbType.Date).Value = order.ToDate;
            command.Parameters.Add("@ExtraDays", SqlDbType.Int).Value = order.ExtraDays;
            command.Parameters.Add("@Total", SqlDbType.Int).Value = order.Total;
            command.Parameters.Add("@Status", SqlDbType.NVarChar).Value = order.Status;
            command.Parameters.Add("@FineAmount", SqlDbType.Int).Value = order.FineAmount;
            command.Parameters.Add("@OrderDate", SqlDbType.Date).Value = order.OrderDate;
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            int response = command.ExecuteNonQuery();
            sqlConnection.Close();
            if (response >= 1)
                return true;
            else
                return false;
        }
    }
}
