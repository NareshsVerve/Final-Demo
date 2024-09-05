using Microsoft.AspNetCore.Mvc;
using SiteInspectionWebApi.Helper;
using SiteInspectionWebApi.Models.Database_Models;
using SiteInspectionWebApi.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace SiteInspectionWebApi.Models.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } 
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } 
        [Required]
        [MaxLength(50)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@#$&*])[A-Za-z\\d@#$&*]{6,}$", ErrorMessage = "Username must contain at least one uppercase letter, one lowercase letter, one number, one special character (@,#,*) and be at least 6 characters long.")]
        public string Username { get; set; } 
        [Required]
        [EmailAddress(ErrorMessage ="Invalid Email Format.")]
        [MaxLength(100)]
        [RegularExpression("^[a-zA-Z0-9._%±]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$", ErrorMessage ="Invalid Email Format.")]
        public string Email { get; set; } 
        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@#$&*])[A-Za-z\\d@#$&*]{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, one special character (@,#,$,&,*) and be at least 8 characters long.")]
        public string Password { get; set; } 
        [Required]
        public Role Role { get; set; }
        public bool IsEmailVerified { get; set; } = false;
        public string? ProfileImage { get; set; } 
        [Required]
        public Guid CreatedBy { get; set; }      
        public DateTime CreatedDate { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; } = true;


        public static User Mapping(UserDTO userDto)
        {
            return new User() { 
                Id = userDto.Id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = userDto.Password,
                Role = (int)userDto.Role,
                IsEmailVerified = userDto.IsEmailVerified,
                ProfileImage = userDto.ProfileImage,
                CreatedBy = userDto.CreatedBy,
                CreatedDate = userDto.CreatedDate,
                UpdatedBy = userDto.UpdatedBy,
                UpdatedDate = userDto.UpdatedDate,
                IsActive = userDto.IsActive,
            };
        }

        public static IEnumerable<User> Mapping(IEnumerable<UserDTO> userDtos)
        {
            var users = new List<User>();
            foreach (var userDto in userDtos)
            {
                users.Add(Mapping(userDto));
            }
            return users;
        }

        public static UserDTO Mapping(User user)
        {
            return new UserDTO()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                Password = user.PasswordHash,
                Role = (Role)user.Role,
                IsEmailVerified = user.IsEmailVerified,
                ProfileImage = user.ProfileImage,
                CreatedBy = user.CreatedBy,
                CreatedDate = user.CreatedDate,
                UpdatedBy = user.UpdatedBy,
                UpdatedDate = user.UpdatedDate,
                IsActive = user.IsActive,
            };
        }

        public static IEnumerable<UserDTO> Mapping(IEnumerable<User> users)
        {
            var userDtos = new List<UserDTO>();
            foreach (var user in users)
            {
                userDtos.Add(Mapping(user));
            }
            return userDtos;
        }
    }
}
