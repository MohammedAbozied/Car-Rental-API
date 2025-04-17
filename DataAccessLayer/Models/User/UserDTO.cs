using Microsoft.Data.SqlClient.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.User
{
    public class NewUserDTO
    {
        public NewUserDTO(int roleId, string firstName, string lastName, string email, string password,
            string? phoneNumber, bool isActive, string? imagePath)
        {
            RoleId = roleId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            IsActive = isActive;
            ImagePath = imagePath;
        }

        [Required]
        [Range(minimum:1,maximum: int.MaxValue)]
        [DefaultValue(2)]// 1: admin , 2: user
        public int RoleId {  get; set; }
        [Required]
        [StringLength(maximumLength:50,MinimumLength =1)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 1)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 8)]
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public string? ImagePath { get; set; }

    }
    
    public class NewUserDataDTO
    {
        public NewUserDataDTO(NewUserDTO User_DTO)
        {
            RoleId = User_DTO.RoleId;
            FirstName = User_DTO.FirstName;
            LastName = User_DTO.LastName;
            Email = User_DTO.Email;
            PasswordHash = Helper.ComputeHash(User_DTO.Password); // We Should Stored Hashed Password
            PhoneNumber = User_DTO.PhoneNumber;
            IsActive = User_DTO.IsActive;
            ImagePath = User_DTO.ImagePath;

        }
        public int RoleId {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public string? ImagePath { get; set; }

    }


    public class UpdateUserDTO
    {
        public UpdateUserDTO( int roleId, string fName, string lName, string email,string phoneNumber,bool isActive,
            string imagePath)
        {
            RoleId = roleId;
            FirstName = fName;
            LastName = lName;
            Email = email;
            PhoneNumber = phoneNumber;
            IsActive = isActive;
            ImagePath = imagePath;

        }
        public UpdateUserDTO()
        {
            
        }
        [Range(minimum: 1, maximum: int.MaxValue)]
        [DefaultValue(2)]// 1: admin , 2: user
        public int RoleId { get; set; }
        [StringLength(maximumLength: 50, MinimumLength = 1)]
        public string FirstName { get; set; }
        [StringLength(maximumLength: 50, MinimumLength = 1)]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public string? ImagePath { get; set; }

    }
    
    
    public class UserInfoDTO
    {
        public UserInfoDTO(int userId,int roleId,string role ,string fName, string lName, string email,
            string phoneNumber, bool isActive,DateTime createdAt, string imagePath) 
        {
            UserId = userId;
            RoleId = roleId;
            Role = role;
            FirstName = fName;
            LastName = lName;
            Email = email;
            PhoneNumber = phoneNumber;
            IsActive = isActive;
            CreatedAt = createdAt;
            ImagePath = imagePath;
        }

        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ImagePath { get; set; }

    }




}
