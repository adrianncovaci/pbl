using System.ComponentModel.DataAnnotations;

namespace BankingApp.API.Models.Roles {
    public class CreateRoleModel {
        [Required]
        public string RoleName { get; set; }
    }
}
