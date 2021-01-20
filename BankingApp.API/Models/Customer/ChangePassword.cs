using System.ComponentModel.DataAnnotations;

namespace BankingApp.API.Models.Customer
{
    public class ChangePassword
    {
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and the confirmation password do not match")]
        public string ConfirmPassword { get; set; }
    }
}