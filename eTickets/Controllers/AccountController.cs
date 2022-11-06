using eTickets.Data;
using eTickets.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _appDbContext;
        public AccountController( UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInMangager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _signInManager = signInMangager;
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
