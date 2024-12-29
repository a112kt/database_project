using HotelSystem.Data;
using HotelSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Controllers
{
    public class GuestController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public GuestController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult GetGuestData()
        {
            var GuestData = _context.Guests.ToList();
            return View(GuestData);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Guest model)
        {
            if (ModelState.IsValid)
            {
                var parameters = new[]
            {
                new SqlParameter("@Fname", model.Fname),
                new SqlParameter("@Lname",model.Lname),
                new SqlParameter("@Email", model.Email),
                new SqlParameter("@address",model.Address),
                new SqlParameter("@phone_num",model.PhoneNumber)
            };

                // Call stored procedure with the parameters
                _context.Database.ExecuteSqlRaw("EXEC SPCreateGuest @Fname, @Lname, @Email, @address, @phone_num", parameters);

                _context.SaveChanges();

                return RedirectToAction("GetGuestData");
            }
            return View(model);
        }
    }
}
