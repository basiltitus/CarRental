using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Server.API.Models;
using Server.API.Models.ViewModels;
using CarTransmission = Server.API.Models.CarTransmission;
using CarVarient = Server.API.Models.CarVarient;

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
        public string checkAvailability(DateTime FromDate, DateTime ToDate,int CarId)
        {
            SqlCommand commandchecker = new SqlCommand("sp_checkcaravailable", sqlConnection);
            commandchecker.Parameters.Add("@CarId", SqlDbType.Int).Value = (int)CarId;
            commandchecker.Parameters.Add("@FromDate", SqlDbType.Date).Value = FromDate;
            commandchecker.Parameters.Add("@ToDate", SqlDbType.Date).Value = ToDate;
            commandchecker.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            string response = (string)commandchecker.ExecuteScalar();
            sqlConnection.Close();
            return response;
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
            command.Parameters.Add("@RegNo", SqlDbType.NVarChar).Value = regNo;
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
        public List<CarListVM> GetAvailableCarList(DateTime FromDate,DateTime ToDate)
        {
            List<CarListVM> carList = new List<CarListVM>();
            SqlCommand command = new SqlCommand("sp_getuserjoincarjoincarmodel", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            SqlDataReader rdr = command.ExecuteReader();

            while (rdr.Read())
            {
                CarListVM car = new CarListVM();
                car.CarModelId = Convert.ToInt32(rdr["CarModelId"]);
                car.CarId = Convert.ToInt32(rdr["CarId"]);
                car.UserId = Convert.ToInt32(rdr["UserId"]);
                car.UserDetails.Name = Convert.ToString(rdr["Name"]);
                car.CarModelDetails.CarName = Convert.ToString(rdr["CarName"]);
                car.CarModelDetails.CarTransmission = (CarTransmission)Enum.Parse(typeof(CarTransmission), rdr["CarTransmission"].ToString());
                car.CarModelDetails.CarType = (CarVarient)Enum.Parse(typeof(CarVarient), rdr["CarType"].ToString());
                car.CarModelDetails.ChargePerDay = Convert.ToInt32(rdr["ChargePerDay"]);
                car.CarModelDetails.SeatCount = Convert.ToInt32(rdr["SeatCount"]);
                car.CreatedOn = Convert.ToDateTime(rdr["CreatedOn"].ToString());
                car.RegNo = Convert.ToString(rdr["RegNo"]);
                car.Colour = Convert.ToString(rdr["Colour"]);
                car.ImgUrl = Convert.ToString(rdr["ImgUrl"]);
                car.CarModelDetails.CarModelId = Convert.ToInt32(rdr["CarModelId"]);
                car.Active = Convert.ToBoolean(rdr["Active"]);
                carList.Add(car);
            }
            sqlConnection.Close();
            foreach(var item in carList)
            {
                if (this.checkAvailability(FromDate, ToDate, item.CarId) == "available")
                {
                    item.Available = true;
                }
                else
                {
                    item.Available = false;
                    item.NextAvailable = this.checkAvailability(FromDate, ToDate, item.CarId);
                }
            }
            return carList;
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
                car.CarModelId = Convert.ToInt32(rdr["CarModelId"]);
                car.CarId = Convert.ToInt32(rdr["CarId"]);
                car.UserId = Convert.ToInt32(rdr["UserId"]);
                car.UserDetails.Name = Convert.ToString(rdr["Name"]);
                car.CarModelDetails.CarName = Convert.ToString(rdr["CarName"]);
                car.CarModelDetails.CarTransmission = (CarTransmission)Enum.Parse(typeof(CarTransmission), rdr["CarTransmission"].ToString());
                car.CarModelDetails.CarType = (CarVarient)Enum.Parse(typeof(CarVarient), rdr["CarType"].ToString());
                car.CarModelDetails.ChargePerDay = Convert.ToInt32(rdr["ChargePerDay"]);
                car.CarModelDetails.SeatCount = Convert.ToInt32(rdr["SeatCount"]);
                car.CreatedOn = Convert.ToDateTime(rdr["CreatedOn"].ToString());
                car.RegNo = Convert.ToString(rdr["RegNo"]);
                car.Colour = Convert.ToString(rdr["Colour"]);
                car.ImgUrl = Convert.ToString(rdr["ImgUrl"]);
                car.CarModelDetails.CarModelId = Convert.ToInt32(rdr["CarModelId"]);
                car.Active = Convert.ToBoolean(rdr["Active"]);
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
                    ChargePerDay = Convert.ToInt32(rdr["ChargePerDay"]),
                    SeatCount = Convert.ToInt32(rdr["SeatCount"]),
                    UserId = Convert.ToInt32(rdr["UserId"]),
                    CreatedOn = Convert.ToDateTime(rdr["CreatedOn"].ToString())
                };
                carList.Add(car);
                /* lstStudent.Add(student);*/
            }
            sqlConnection.Close();
            return carList;
        }
        /*public CarModel GetCarModel(int id)
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
        }*/
        /*public List<CarModel> GetCar(int transmission, int varient)
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
        }*/
        public bool UpdateCarModel(CarModel car)
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
        public bool UpdateCar(Car car)
        {
            SqlCommand command = new SqlCommand("sp_updatecar", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@CarId", SqlDbType.Int).Value = car.CarId;
            command.Parameters.Add("@RegNo", SqlDbType.NVarChar).Value = car.RegNo.ToString();
            command.Parameters.Add("@Colour", SqlDbType.NVarChar).Value = car.Colour.ToString();
            command.Parameters.Add("@ImgUrl", SqlDbType.NVarChar).Value = car.ImgUrl.ToString();
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
        public bool DeleteCarModel(int id)
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
        public bool ChangeCarActive(int id,bool active)
        {
            SqlCommand command = new SqlCommand("sp_changecaractive", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@CarId", SqlDbType.Int).Value = id;
            command.Parameters.Add("@Active", SqlDbType.Bit).Value = active;
            sqlConnection.Open();
            int rows = command.ExecuteNonQuery();
            sqlConnection.Close();
            if (rows == 1)
                return true;
            else
                return false;

        }
        public CarModelListVM GetCarModel(int id)
        {
            SqlCommand command = new SqlCommand("sp_getcarmodel", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@CarModelId", SqlDbType.Int).Value = id;
            sqlConnection.Open();
            SqlDataReader rdr = command.ExecuteReader();
            CarModelListVM car = null;
            while (rdr.Read())
            {
                car = new CarModelListVM();
                car.CarModelId = Convert.ToInt32(rdr["CarModelId"].ToString());
                car.CarName = rdr["CarName"].ToString();
                car.CarTransmission = (Models.ViewModels.CarTransmission)Enum.Parse(typeof(CarTransmission), rdr["CarTransmission"].ToString());
                car.CarType = (Models.ViewModels.CarVarient)Enum.Parse(typeof(CarVarient), rdr["CarType"].ToString());
                car.UserId = Convert.ToInt32(rdr["UserId"].ToString());
                car.SeatCount = Convert.ToInt32(rdr["SeatCount"].ToString());
                car.ChargePerDay = Convert.ToInt32(rdr["ChargePerDay"].ToString());
                car.CreatedOn = Convert.ToDateTime(rdr["CreatedOn"].ToString());
                car.UserDetails.Name = rdr["Name"].ToString();

            }
            sqlConnection.Close();
            return car;
        }
        public CarListVM GetCarJoined(int id)
        {
            SqlCommand command = new SqlCommand("sp_getcarjoined", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@CarId", SqlDbType.Int).Value = id;
            sqlConnection.Open();
            SqlDataReader rdr = command.ExecuteReader();
            CarListVM car = null;
            while (rdr.Read())
            {
                car = new CarListVM();
                car.CarModelId = Convert.ToInt32(rdr["CarModelId"].ToString());
                car.CarModelDetails.CarName = rdr["CarName"].ToString();
                car.CarModelDetails.CarTransmission = (CarTransmission)Enum.Parse(typeof(CarTransmission), rdr["CarTransmission"].ToString());
                car.CarModelDetails.CarType = (CarVarient)Enum.Parse(typeof(CarVarient), rdr["CarType"].ToString());
                car.UserId = Convert.ToInt32(rdr["UserId"].ToString());
                car.CarModelDetails.SeatCount = Convert.ToInt32(rdr["SeatCount"].ToString());
                car.CarModelDetails.ChargePerDay = Convert.ToInt32(rdr["ChargePerDay"].ToString());
                car.CreatedOn = Convert.ToDateTime(rdr["CreatedOn"].ToString());
                car.UserDetails.Name = rdr["Name"].ToString();
                car.CreatedOn = Convert.ToDateTime(rdr["CreatedOn"].ToString());
                car.RegNo = Convert.ToString(rdr["RegNo"]);
                car.Colour = Convert.ToString(rdr["Colour"]);
                car.ImgUrl = Convert.ToString(rdr["ImgUrl"]);

            }
            sqlConnection.Close();
            return car;
        }
        public Car GetCar(int id)
        {
            SqlCommand command = new SqlCommand("sp_getcar", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@CarId", SqlDbType.Int).Value = id;
            sqlConnection.Open();
            SqlDataReader rdr = command.ExecuteReader();
            Car car = null;
            while (rdr.Read())
            {
                car = new Car();
                car.CarId = Convert.ToInt32(rdr["CarId"].ToString());
                car.CarModelId = Convert.ToInt32(rdr["CarModelId"].ToString());
                car.UserId = Convert.ToInt32(rdr["UserId"].ToString());
                car.CreatedOn = Convert.ToDateTime(rdr["CreatedOn"].ToString());
                car.CreatedOn = Convert.ToDateTime(rdr["CreatedOn"].ToString());
                car.RegNo = Convert.ToString(rdr["RegNo"]);
                car.Colour = Convert.ToString(rdr["Colour"]);
                car.ImgUrl = Convert.ToString(rdr["ImgUrl"]);

            }
            sqlConnection.Close();
            return car;
        }
    }
}

