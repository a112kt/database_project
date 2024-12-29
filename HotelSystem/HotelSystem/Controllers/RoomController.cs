using HotelSystem.Data;
using HotelSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Controllers
{
    public class RoomController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public RoomController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult GetRoomDataa()
        {
            var RoomData = _context.Rooms.ToList();

            return View(RoomData);
        }
        public IActionResult Update(int id)
        {
            var RoomDetails = _context.Rooms.Find(id);

            return View(RoomDetails);
        }
        [HttpPost]
        public IActionResult Update(Room model)
        {
            var parameters = new[]
            {
                new SqlParameter("@id", model.RoomId),
                new SqlParameter("@Type",model.Type),
                new SqlParameter("@Status", model.Status),
                new SqlParameter("@price",model.Price),

            };

            _context.Database.ExecuteSqlRaw("SpUpdateRoom @id, @Type, @Status, @price", parameters);
            _context.SaveChanges();

            return RedirectToAction("GetRoomDataa");
        }
    }
}
