using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Vehicle
{
    public class VehicleCreateDTO
    {
        public VehicleCreateDTO( string make, string model, int year, int mileage,
            int fuelTypeID, string plateNumber,int vehicleCategoryID, decimal rentalPricePerDay,
            bool isAvailableForRent, string imagePath)
        {
            Make = make;
            Model = model;
            Year = year;
            Mileage = mileage;
            FuelTypeID = fuelTypeID;
            PlateNumber = plateNumber;
            VehicleCategoryID = vehicleCategoryID;
            RentalPricePerDay = rentalPricePerDay;
            IsAvailableForRent = isAvailableForRent;
            ImagePath = imagePath;
        }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public int FuelTypeID { get; set; }
        public string PlateNumber { get; set; }
        public int VehicleCategoryID { get; set; }
        public decimal RentalPricePerDay { get; set; }
        public bool IsAvailableForRent { get; set; }
        public string ImagePath { get; set; }

    }
    
    public class VehicleUpdateDTO
    {
        public VehicleUpdateDTO(int id , string make, string model, int year, int mileage,
            int fuelTypeID, string plateNumber,int vehicleCategoryID, decimal rentalPricePerDay,
            bool isAvailableForRent, string imagePath)
        {
            Id = id;
            Make = make;
            Model = model;
            Year = year;
            Mileage = mileage;
            FuelTypeID = fuelTypeID;
            PlateNumber = plateNumber;
            VehicleCategoryID = vehicleCategoryID;
            RentalPricePerDay = rentalPricePerDay;
            IsAvailableForRent = isAvailableForRent;
            ImagePath = imagePath;
        }
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public int FuelTypeID { get; set; }
        public string PlateNumber { get; set; }
        public int VehicleCategoryID { get; set; }
        public decimal RentalPricePerDay { get; set; }
        public bool IsAvailableForRent { get; set; }
        public string ImagePath { get; set; }

    }
    
    // with GetVehicle, GetAllVehicles (show info)
    public class VehicleReadDTO
    {
        public VehicleReadDTO(int vehicleID, string make, string model, int year, int mileage,
            string fuelType, string plateNumber,string categoryName, decimal rentalPricePerDay,
            bool isAvailableForRent, string imagePath)
        {
            VehicleID = vehicleID;
            Make = make;
            Model = model; 
            Year = year;
            Mileage = mileage;
            FuelType = fuelType;
            PlateNumber = plateNumber;
            CategoryName = categoryName;
            RentalPricePerDay = rentalPricePerDay;
            IsAvailableForRent = isAvailableForRent;
            ImagePath = imagePath;
        }
        public int VehicleID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public string FuelType { get; set; }
        public string PlateNumber { get; set; }
        public string CategoryName { get; set; }
        public decimal RentalPricePerDay { get; set; }
        public bool IsAvailableForRent { get; set; }
        public string ImagePath { get; set; }

    }


    
















}
