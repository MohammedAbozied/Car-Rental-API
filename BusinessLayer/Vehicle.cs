using DataAccessLayer;
using DataAccessLayer.Models.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusinessLayer
{
    public class Vehicle
    {
        public enum eMode { AddNew = 0, Update = 1 }
        public eMode Mode = eMode.AddNew;

        public VehicleCreateDTO CreateDTO
        {
            get
            {
                return new VehicleCreateDTO(Make, Model, Year, Mileage, FuelTypeID, PlateNumber, VehicleCategoryID,
                    RentalPricePerDay, IsAvailableForRent, ImagePath);
            }
        }
        
        public VehicleUpdateDTO UpdateDTO
        {
            get
            {
                return new VehicleUpdateDTO(VehicleID,Make, Model, Year, Mileage, FuelTypeID, PlateNumber, VehicleCategoryID,
                    RentalPricePerDay, IsAvailableForRent, ImagePath);
            }
        }


        public async Task<VehicleReadDTO> ToReadDTO()
        {
            var fuelTypeName = await FuelTypeName();
                return new VehicleReadDTO(VehicleID, Make, Model, Year, Mileage, fuelTypeName, PlateNumber, "SOON",
                    RentalPricePerDay, IsAvailableForRent, ImagePath);
            
        }


        public int VehicleID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public int FuelTypeID { get; set; }
        public async Task<string> FuelTypeName()
        {
            var ft = await FuelType.Find(this.FuelTypeID);
            return ft.Type;
        }

        public string PlateNumber { get; set; }
        public int VehicleCategoryID { get; set; }
        public decimal RentalPricePerDay { get; set; }
        public bool IsAvailableForRent { get; set; }
        public string ImagePath { get; set; }

        public Vehicle(VehicleCreateDTO VDTO)
        {
            this.Make = VDTO.Make;
            this.Model = VDTO.Model;
            this.Year = VDTO.Year;
            this.Mileage = VDTO.Mileage;
            this.FuelTypeID = VDTO.FuelTypeID;
            this.PlateNumber = VDTO.PlateNumber;
            this.VehicleCategoryID = VDTO.VehicleCategoryID;
            this.RentalPricePerDay = VDTO.RentalPricePerDay;
            this.IsAvailableForRent = VDTO.IsAvailableForRent;
            this.ImagePath = VDTO.ImagePath;

            this.Mode = eMode.AddNew;
        }
        
        public Vehicle(VehicleUpdateDTO VDTO)
        {
            this.VehicleID = VDTO.Id;
            this.Make = VDTO.Make;
            this.Model = VDTO.Model;
            this.Year = VDTO.Year;
            this.Mileage = VDTO.Mileage;
            this.FuelTypeID = VDTO.FuelTypeID;
            this.PlateNumber = VDTO.PlateNumber;
            this.VehicleCategoryID = VDTO.VehicleCategoryID;
            this.RentalPricePerDay = VDTO.RentalPricePerDay;
            this.IsAvailableForRent = VDTO.IsAvailableForRent;
            this.ImagePath = VDTO.ImagePath;

            this.Mode = eMode.Update;
        }

        private async Task<bool> _AddNewVehicle()
        {
            this.VehicleID = await VehicleData.AddNewVehicle(CreateDTO);

            return this.VehicleID != -1;
        }

        private async Task<bool> _UpdateVehicle()
        {
            return await VehicleData.UpdateVehicle(UpdateDTO);
        }

        public async Task<bool> Save()
        {
            switch(Mode)
            {
                case eMode.AddNew:
                    if(await _AddNewVehicle())
                    {
                        this.Mode = eMode.Update;
                        return true;
                    }
                    else
                        return false;

                case eMode.Update:
                    return await _UpdateVehicle();
            }

            return false;

        }


        public static async Task<List<VehicleReadDTO>> GetAllVehicles()
        {
            return await VehicleData.GetAllVehicles();  
        } 

        public static async Task<Vehicle> Find(int id)
        {
            var VDTO =  await VehicleData.GetVehicleById(id);

            if (VDTO != null)
                return new Vehicle(VDTO);
            else
                return null;

        }

        public async Task<bool>DeleteVehicle()
        {
            return await VehicleData.DeleteVehicle(this.VehicleID);
        }

    }
}
