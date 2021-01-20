using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.API.Controllers {
    [ApiControllerAttribute]
    [Route("[controller]")]
    public class AdministrationController: ControllerBase {
        // private readonly RoleManager<IdentityRole> _roleManager;

        // public AdministrationController(RoleManager<IdentityRole> roleManager) {
        //     _roleManager = roleManager;
        // }

        // [Authorize(Policy = "AdminRole")]
        // [HttpGetAttribute("users")]
        // public async Task<IActionResult> GetUsesrsWithRoles() {
        //     return Ok("only admins!");
        // }

    //     [HttpPostAttribute]
    //     public Task<IActionResult> CreateRole()
    }
}
