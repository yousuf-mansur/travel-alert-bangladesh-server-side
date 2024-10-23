using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Entities;
using DataAccessLayer.DTOs.InputModels;
using DataAccessLayer.DTOs.OutputModels;
using DataAccessLayer.Data;

namespace DataAccessLayer.Entities.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoomsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        public IActionResult GetRooms()
        {
            var rooms = _context.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.RoomType)
                .Include(r => r.RoomSubType);
            return Ok(rooms.Select(r => new RoomDTO
            {
                RoomID = r.RoomID,
                AveragePrice = r.AveragePrice,
                MaxOccupancy = r.MaxOccupancy,
                IsAvailable = r.IsAvailable,
                HotelID = r.HotelID,

                RoomTypeID = r.RoomTypeID,

                RoomSubTypeID = r.RoomSubTypeID,

            }));
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public IActionResult GetRoom(int id)
        {
            var room = _context.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.RoomType)
                .Include(r => r.RoomSubType)
                .FirstOrDefault(r => r.RoomID == id);
            if (room == null)
            {
                return NotFound();
            }
            return Ok(new RoomDTO
            {
                RoomID = room.RoomID,
                AveragePrice = room.AveragePrice,
                MaxOccupancy = room.MaxOccupancy,
                IsAvailable = room.IsAvailable,
                HotelID = room.HotelID,

                RoomTypeID = room.RoomTypeID,

                RoomSubTypeID = room.RoomSubTypeID,

            });
        }

        // POST: api/Rooms
        [HttpPost("add/room")]
        public IActionResult CreateRoom(RoomDTOs roomDTO)
        {
            // Check if the referenced Hotel, RoomType, and RoomSubType exist
            var hotel = _context.Hotels.Find(roomDTO.HotelID);
            var roomType = _context.RoomTypes.Find(roomDTO.RoomTypeID);
            var roomSubType = _context.RoomSubTypes.Find(roomDTO.RoomSubTypeID);

            // If any of these entities are not found, return a 404 response
            if (hotel == null || roomType == null || roomSubType == null)
            {
                return NotFound("Hotel, Room Type, or Room Sub-Type not found.");
            }

            // Create a new Room entity based on the data provided
            var newRoom = new Room
            {
                AveragePrice = roomDTO.AveragePrice,
                MaxOccupancy = roomDTO.MaxOccupancy,
                IsAvailable = roomDTO.IsAvailable,
                HotelID = roomDTO.HotelID,
                RoomTypeID = roomDTO.RoomTypeID,
                RoomSubTypeID = roomDTO.RoomSubTypeID,
            };

            // Add the new room to the context and save changes
            _context.Rooms.Add(newRoom);
            _context.SaveChanges();

            // Get the current request path
            var request = HttpContext.Request;
            var currentPath = request.Path;

            // Remove the last segment of the path
            var basePath = UrlTask.RemoveLastSegment(currentPath);

            // Find the corresponding UrlService entry asynchronously
            var urlService = _context.UrlServices
                .Include(u => u.RequestUrl)
                .Include(u => u.CurrentUrl)
                .FirstOrDefaultAsync(e => e.CurrentUrl.Url == basePath.ToString())
                .Result;

            // Determine the request URL based on whether the UrlService entry was found
            var requestUrl = urlService?.RequestUrl.Url ?? "dashboard";

            // Return the RoomID and the determined request URL
            return Ok(new { id = newRoom.RoomID, url = requestUrl });
        }



        // PUT: api/Rooms/update/room/5
        [HttpPut("update/room/{id}")]
        public IActionResult UpdateRoom(int id, RoomDTOs roomDTO)
        {

            var room = _context.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.RoomType)
                .Include(r => r.RoomSubType)
                .FirstOrDefault(r => r.RoomID == id);

            if (room == null)
            {
                return NotFound("Room not found.");
            }

            var hotel = _context.Hotels.Find(roomDTO.HotelID);
            var roomType = _context.RoomTypes.Find(roomDTO.RoomTypeID);
            var roomSubType = _context.RoomSubTypes.Find(roomDTO.RoomSubTypeID);

            if (hotel == null || roomType == null || roomSubType == null)
            {
                return NotFound("Related entities not found.");
            }

            room.AveragePrice = roomDTO.AveragePrice;
            room.MaxOccupancy = roomDTO.MaxOccupancy;
            room.IsAvailable = roomDTO.IsAvailable;
            room.HotelID = roomDTO.HotelID;
            room.RoomTypeID = roomDTO.RoomTypeID;
            room.RoomSubTypeID = roomDTO.RoomSubTypeID;

            _context.Rooms.Update(room);
            _context.SaveChanges();

            var request = HttpContext.Request;
            var currentPath = request.Path;
            var basePath = UrlTask.RemoveLastSegment(currentPath);


            var urlService = _context.UrlServices
                .Include(u => u.RequestUrl)
                .Include(u => u.CurrentUrl)
                .FirstOrDefaultAsync(e => e.CurrentUrl.Url == basePath.ToString())
                .Result;


            var requestUrl = urlService?.RequestUrl.Url ?? "dashboard";

            return Ok(new { id = room.RoomID, url = requestUrl });
        }


        // DELETE: api/Rooms/5
        [HttpDelete("Rooms/{id}")]
        public IActionResult DeleteRoom(int id)
        {
            var room = _context.Rooms.Find(id);
            if (room == null)
            {
                return NotFound("Room not found.");
            }

            _context.Rooms.Remove(room);
            _context.SaveChanges();

            return Ok(new { message = "Deleted successfully" });
        }

        
        public static string RemoveLastSegment(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return url;
            }

            url = url.TrimStart('/');

            var segments = url.Split('/');

            if (segments.Length > 1)
            {
                var lastSegment = segments[^1];

                if (int.TryParse(lastSegment, out _))
                {
                    return string.Join("/", segments, 0, segments.Length - 1);
                }
            }

            return url;
        }
    }
}