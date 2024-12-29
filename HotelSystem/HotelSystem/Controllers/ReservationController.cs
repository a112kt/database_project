using HotelSystem.Data;
using HotelSystem.Models;
using HotelSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public ReservationController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult RoomReservation()
        {
            var roomReservations = _context.Reservations
            .Include(res => res.Rooms) 
            .Where(res => res.Rooms.Any()) // Only include reservations with rooms
            .Select(res => new RoomReservationViewModel
            {
                RoomId = res.Rooms.First().RoomId,  
                RoomType = res.Rooms.First().Type,   
                RoomStatus = res.Rooms.First().Status,
                ReservationId = res.ResId,
            }).ToList();

            return View(roomReservations);
        }
        [HttpPost]
        public IActionResult Delete(Room model)
        {
            var parameter = new SqlParameter("@id", model.RoomId);

            _context.Database.ExecuteSqlRaw("EXEC DeleteReservationByRoomId @id", parameter);
            _context.SaveChanges();

            return RedirectToAction("GetRoomData","Home");
        }
    }
}
