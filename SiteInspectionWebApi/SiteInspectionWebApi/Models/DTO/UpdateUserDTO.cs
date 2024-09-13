using SiteInspectionWebApi.Models.Database_Models;
using SiteInspectionWebApi.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SiteInspectionWebApi.Models.DTO
{
    public class UpdateUserDTO
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
        [EmailAddress(ErrorMessage = "Invalid Email Format.")]
        [MaxLength(100)]
        [RegularExpression("^[a-zA-Z0-9._%±]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email Format.")]
        public string Email { get; set; }
        [Required]
        public Role Role { get; set; }
        public bool IsEmailVerified { get; set; } = false;
        public string? ProfileImage { get; set; }
        [Required]
        public Guid UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; } = true;

        public static UpdateUserDTO Mapping(User user)
        {
            return new UpdateUserDTO()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                Role = (Role)user.Role,
                IsEmailVerified = user.IsEmailVerified,
                ProfileImage = user.ProfileImage,
                UpdatedBy = user.UpdatedBy,
                UpdatedDate = user.UpdatedDate,
                IsActive = user.IsActive,
            };
        }
        public static UpdateUserDTO Mapping(UserDTO userDto)
        {
            return new UpdateUserDTO()
            {
                Id = userDto.Id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Username = userDto.Username,
                Email = userDto.Email,
                Role = userDto.Role,
                IsEmailVerified = userDto.IsEmailVerified,
                ProfileImage = userDto.ProfileImage,
                UpdatedBy = userDto.UpdatedBy,
                UpdatedDate = userDto.UpdatedDate,
                IsActive = userDto.IsActive,
            };
        }
    }
}
