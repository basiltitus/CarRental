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
    public class PaymentOperation
    {
        SqlConnection sqlConnection;
        string conStr;
        private IConfiguration Configuration;
        public PaymentOperation(IConfiguration _configuration)
        {
            Configuration = _configuration;
            conStr = this.Configuration.GetConnectionString("CarRentDB");
            sqlConnection = new SqlConnection(conStr);
        }
        public bool AddPayment(Payment payment)
        {
            SqlCommand command = new SqlCommand("sp_insertpayment", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@OrderId", SqlDbType.Int).Value = payment.OrderId;
            command.Parameters.Add("@UserId", SqlDbType.Int).Value = payment.UserId;
            command.Parameters.Add("@Total", SqlDbType.Int).Value = payment.Total;
            sqlConnection.Open();
            int rows = command.ExecuteNonQuery();
            sqlConnection.Close();
            if (rows >= 1)
                return true;
            else
                return false;
        }
        public List<Payment> GetPaymentList(int userId)
        {
            List<Payment> paymentList = new List<Payment>();
            SqlCommand command = new SqlCommand("sp_getpaymentList", sqlConnection);
            command.Parameters.Add("@UserId", SqlDbType.Int).Value = (int)userId;
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            SqlDataReader rdr = command.ExecuteReader();
            while (rdr.Read())
            {
                Payment payment = new Payment
                {
                    OrderId = Convert.ToInt32(rdr["OrderId"]),
                    UserId = Convert.ToInt32(rdr["UserId"]),
                    PaymentId = Convert.ToInt32(rdr["PaymentId"]),
                    Total = Convert.ToInt32(rdr["Total"])
                };
                paymentList.Add(payment);
            }
            sqlConnection.Close();
            return paymentList;
        }
    }
}
