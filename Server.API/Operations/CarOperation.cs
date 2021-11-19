using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Server.API.Models;
using Server.API.Models.ViewModels;

namespace Server.API.Operations
{
    public class CarOperation
    {
        SqlConnection sqlConnection;
        string conStr;
        private IConfiguration Configuration;
        public CarOperation(IConfiguration _configuration)
        {
            Configuration = _configuration;
            conStr = this.Configuration.GetConnectionString("CarRentDB");
            sqlConnection = new SqlConnection(conStr);
        }
        public bool AddCar(Car car)
        {
            SqlCommand command = new SqlCommand("sp_addcar", sqlConnection);
            command.Parameters.Add("@CarModelId", SqlDbType.Int).Value = car.CarModelId;
            command.Parameters.Add("@Colour", SqlDbType.NVarChar).Value = car.Colour;
            command.Parameters.Add("@RegNo", SqlDbType.NVarChar).Value = car.RegNo;
            command.Parameters.Add("@ImgUrl", SqlDbType.NVarChar).Value = car.ImgUrl;
            command.Parameters.Add("@UserId", SqlDbType.Int).Value = car.UserId;
            command.Parameters.Add("@CreatedOn", SqlDbType.Date).Value = car.CreatedOn;
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            int response = (int)command.ExecuteNonQuery();
            sqlConnection.Close();
            if (response == 1)
                return true;
            else
                return false;
        }
        public bool CheckRegNoExists(string regNo)
        {
            SqlCommand command = new SqlCommand("sp_checkregnoexists", sqlConnection);
            command.Parameters.Add("@RegNo", SqlDbType.NVarChar).Value =regNo;
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            int response = (int)command.ExecuteScalar();
            sqlConnection.Close();
            if (response > 0)
            {
                return true;
            }
            return false;
        }
        public bool AddCarModel(CarModel car)
        {
            SqlCommand command = new SqlCommand("sp_addcarmodel", sqlConnection);
            command.Parameters.Add("@CarName", SqlDbType.NVarChar).Value = car.CarName;
            command.Parameters.Add("@CarTransmission", SqlDbType.NVarChar).Value = car.CarTransmission.ToString();
            command.Parameters.Add("@CarType", SqlDbType.NVarChar).Value = car.CarType.ToString();
            command.Parameters.Add("@ChargePerDay", SqlDbType.Int).Value = (int)car.ChargePerDay;
            command.Parameters.Add("@SeatCount", SqlDbType.Int).Value = (int)car.SeatCount;
            command.Parameters.Add("@UserId", SqlDbType.Int).Value = car.UserId;
            command.Parameters.Add("@CreatedOn", SqlDbType.Date).Value = car.CreatedOn;
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            int response = (int)command.ExecuteNonQuery();
            sqlConnection.Close();
            if (response == 1)
                return true;
            else
                return false;
        }
        public List<CarListVM> GetCarList()
        {
            List<CarListVM> carList = new List<CarListVM>();
            SqlCommand command = new SqlCommand("sp_getuserjoincarjoincarmodel", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            SqlDataReader rdr = command.ExecuteReader();

            while (rdr.Read())
            {
                CarListVM car = new CarListVM();
                car.UserId = Convert.ToInt32(rdr["UserId"]);
                car.UserDetails.Name = Convert.ToString(rdr["Name"]);
                car.CarModelDetails.CarName = Convert.ToString(rdr["CarName"]);
                car.CarModelDetails.CarTransmission = (CarTransmission)Enum.Parse(typeof(CarTransmission), rdr["CarTransmission"].ToString());
                    car.CarModelDetails.CarType = (CarVarient)Enum.Parse(typeof(CarVarient), rdr["CarType"].ToString());
                car.CarModelDetails.ChargePerDay = Convert.ToInt32(rdr["ChargePerDay"]);
                car.CarModelDetails.SeatCount = Convert.ToInt32(rdr["SeatCount"]);
                car.CreatedOn = Convert.ToDateTime(rdr["CreatedOn"].ToString());
                car.RegNo = Convert.ToString(rdr["RegNo"]);
                car.Colour =Convert.ToString( rdr["Colour"]);
                car.ImgUrl = Convert.ToString(rdr["ImgUrl"]);
                

                carList.Add(car);
            }
            sqlConnection.Close();
            return carList;
        }
        public List<CarModel> GetList()
        {
            List<CarModel> carList = new List<CarModel>();
            SqlCommand command = new SqlCommand("sp_getcarmodellist", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            SqlDataReader rdr = command.ExecuteReader();

            while (rdr.Read())
            {
                CarModel car = new CarModel
                {
                    CarModelId = Convert.ToInt32(rdr["CarModelId"]),
                    CarName = rdr["CarName"].ToString(),
                    CarTransmission = (CarTransmission)Enum.Parse(typeof(CarTransmission), rdr["CarTransmission"].ToString()),
                    CarType = (CarVarient)Enum.Parse(typeof(CarVarient), rdr["CarType"].ToString()),
                    ChargePerDay =Convert.ToInt32(rdr["ChargePerDay"]),
                    SeatCount = Convert.ToInt32(rdr["SeatCount"]),
                    UserId=Convert.ToInt32(rdr["UserId"]),
                    CreatedOn=Convert.ToDateTime(rdr["CreatedOn"].ToString())
                };
                carList.Add(car);
                /* lstStudent.Add(student);*/
            }
            sqlConnection.Close();
            return carList;
        }
        public CarModel GetCar(int id)
        {
            SqlCommand command = new SqlCommand("sp_getcarmodeldetails", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@CarModelId", SqlDbType.Int).Value = id;
            sqlConnection.Open();
            SqlDataReader rdr = command.ExecuteReader();
            CarModel car = new CarModel();
            while (rdr.Read())
            {
                car.CarModelId = Convert.ToInt32(rdr["CarModelId"]);
                car.CarName = rdr["CarName"].ToString();
                car.CarTransmission = (CarTransmission)Enum.Parse(typeof(CarTransmission), rdr["CarTransmission"].ToString());
                 car.CarType = (CarVarient)Enum.Parse(typeof(CarVarient), rdr["CarType"].ToString());
                car.ChargePerDay = Convert.ToInt32(rdr["ChargePerDay"]);
                car.SeatCount = Convert.ToInt32(rdr["SeatCount"]);
                car.UserId = Convert.ToInt32(rdr["UserId"]);
                car.CreatedOn = Convert.ToDateTime(rdr["CreatedOn"].ToString());
            }
            sqlConnection.Close();
            return car;
        }
        public List<CarModel> GetCar(int transmission, int varient)
        {
            List<CarModel> CarList = new List<CarModel>();
            SqlCommand command = new SqlCommand("sp_getcarbytransmissionvarient", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@CarTransmission", SqlDbType.Int).Value = transmission;
            command.Parameters.Add("@CarType", SqlDbType.Int).Value = varient;
            sqlConnection.Open();
            SqlDataReader rdr = command.ExecuteReader();
            while (rdr.Read())
            {
                CarModel car = new CarModel();
                car.CarModelId = Convert.ToInt32(rdr["CarModelId"]);
                car.CarName = rdr["CarName"].ToString();
                car.CarTransmission = (CarTransmission)Convert.ToInt32(rdr["CarTransmission"]);
                car.CarType = (CarVarient)Convert.ToInt32(rdr["CarType"]);
                CarList.Add(car);
            }
            sqlConnection.Close();
            return CarList;
        }
        public bool UpdateCar(CarModel car)
        {

            SqlCommand command = new SqlCommand("sp_updatecarmodel", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@CarModelId", SqlDbType.Int).Value = car.CarModelId;
            command.Parameters.Add("@CarName", SqlDbType.NVarChar).Value = car.CarName;
            command.Parameters.Add("@CarTransmission", SqlDbType.NVarChar).Value = car.CarTransmission.ToString();
            command.Parameters.Add("@CarType", SqlDbType.NVarChar).Value = car.CarType.ToString();
            command.Parameters.Add("@ChargePerDay", SqlDbType.Int).Value = car.ChargePerDay;
            command.Parameters.Add("@SeatCount", SqlDbType.Int).Value = car.SeatCount;
            sqlConnection.Open();
            int rows = command.ExecuteNonQuery();
            sqlConnection.Close();
            if (rows == 1)
                return true;
            else
                return false;

        }
        public string[] GetCarVarients()
        {
            string[] varients = { "MINI_HATCHBACK", "SMALL_HATCHBACKS", "Small_Sedan", "Sedans", "Executive_Luxury_Cars", "MPVs", "SUV", "Crossovers" };

            return varients;
        }
        public bool DeleteCar(int id)
        {
            SqlCommand command = new SqlCommand("sp_deletecarmodel", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@CarModelId", SqlDbType.Int).Value = id;
            sqlConnection.Open();
            int rows = command.ExecuteNonQuery();
            sqlConnection.Close();
            if (rows == 1)
                return true;
            else
                return false;

        }
       
    }
}

