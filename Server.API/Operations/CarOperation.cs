﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Server.API.Models;
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
        public bool AddCar(CarTable car)
        {
            SqlCommand command = new SqlCommand("sp_addcar", sqlConnection);
            command.Parameters.Add("@CarName", SqlDbType.NVarChar).Value = car.CarName;
            command.Parameters.Add("@CarTransmission", SqlDbType.NVarChar).Value = (int)car.CarTransmission;
            command.Parameters.Add("@CarCount", SqlDbType.NVarChar).Value = (int)car.CarCount;
            command.Parameters.Add("@CarType", SqlDbType.Int).Value = (int)car.CarType;
            command.Parameters.Add("@ChargePerDay", SqlDbType.Int).Value = car.ChargePerDay;
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            int response = (int)command.ExecuteNonQuery();
            sqlConnection.Close();
            if (response == 1)
                return true;
            else
                return false;
        }

        public List<CarTable> GetList()
        {
            List<CarTable> carList = new List<CarTable>();
            SqlCommand command = new SqlCommand("sp_getcarlist", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            SqlDataReader rdr = command.ExecuteReader();

            while (rdr.Read())
            {
                CarTable car = new CarTable
                {
                    CarId = Convert.ToInt32(rdr["CarId"]),
                    CarName = rdr["CarName"].ToString(),
                    CarTransmission =(CarTransmission)Convert.ToInt32(rdr["CarTransmission"]),
                    CarCount=Convert.ToInt32(rdr["CarCount"]),
                    CarType = (CarVarient)Convert.ToInt32(rdr["CarType"]),
                    ChargePerDay = Convert.ToInt32(rdr["ChargePerDay"])
                };
                carList.Add(car);
                /* lstStudent.Add(student);*/
            }
            sqlConnection.Close();
            return carList;
        }
        public CarTable GetCar(int id)
        {
            SqlCommand command = new SqlCommand("sp_getcardetails", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@CarId", SqlDbType.Int).Value = id;
            sqlConnection.Open();
            SqlDataReader rdr = command.ExecuteReader();
            CarTable car = new CarTable();
            while (rdr.Read())
            {
                car.CarId = Convert.ToInt32(rdr["CarId"]);
                car.CarName = rdr["CarName"].ToString();
                car.CarTransmission = (CarTransmission)Convert.ToInt32(rdr["CarTransmission"]);
                car.CarCount = Convert.ToInt32(rdr["CarCount"]);
                car.CarType = (CarVarient)Convert.ToInt32(rdr["CarType"]);
                car.ChargePerDay = Convert.ToInt32(rdr["ChargePerDay"]);
            }
            sqlConnection.Close();
            return car;
        }
        public List<CarTable> GetCar(int transmission,int varient)
        {
            List<CarTable> CarList = new List<CarTable>();
            SqlCommand command = new SqlCommand("sp_getcarbytransmissionvarient", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@CarTransmission", SqlDbType.Int).Value = transmission;
            command.Parameters.Add("@CarType", SqlDbType.Int).Value = varient;
            sqlConnection.Open();
            SqlDataReader rdr = command.ExecuteReader();
            while (rdr.Read())
            {
            CarTable car = new CarTable();
                car.CarId = Convert.ToInt32(rdr["CarId"]);
                car.CarName = rdr["CarName"].ToString();
                car.CarTransmission = (CarTransmission)Convert.ToInt32(rdr["CarTransmission"]);
                car.CarCount = Convert.ToInt32(rdr["CarCount"]);
                car.CarType = (CarVarient)Convert.ToInt32(rdr["CarType"]);
                car.ChargePerDay = Convert.ToInt32(rdr["ChargePerDay"]);
                CarList.Add(car);
            }
            sqlConnection.Close();
            return CarList;
        }
        public bool UpdateCar(CarTable car)
        {

            SqlCommand command = new SqlCommand("sp_updatecar", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@CarId", SqlDbType.Int).Value = car.CarId;
            command.Parameters.Add("@CarName", SqlDbType.NVarChar).Value = car.CarName;
            command.Parameters.Add("@CarTransmission", SqlDbType.NVarChar).Value = (int)car.CarTransmission;
            command.Parameters.Add("@CarCount", SqlDbType.NVarChar).Value = (int)car.CarCount;
            command.Parameters.Add("@CarType", SqlDbType.Int).Value = car.CarType;
            command.Parameters.Add("@ChargePerDay", SqlDbType.Int).Value = car.ChargePerDay;
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
            SqlCommand command = new SqlCommand("sp_deletecar", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@CarId", SqlDbType.Int).Value = id;
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

