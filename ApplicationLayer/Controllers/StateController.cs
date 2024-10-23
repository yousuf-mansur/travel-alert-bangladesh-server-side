using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Entities;
using DataAccessLayer.DTOs.InputModels;
using DataAccessLayer.DTOs.OutputModels;
using DataAccessLayer.Data;

namespace ApplicationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StateController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/State
        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<StateOutputModel>>> GetStates()
        {
            var states = await _context.States
                .Include(s => s.Country)
                .Select(s => new StateOutputModel
                {
                    StateID = s.StateID,
                    StateName = s.StateName,
                    CountryName = s.Country.CountryName
                })
                .ToListAsync();

            return states;
        }



        // GET: api/State/{id}
        [HttpGet("get/{id}")]
        public async Task<ActionResult<State>> GetState(int id)
        {
            var state = await _context.States.Include(s => s.Country).Include(s => s.Locations)
                .FirstOrDefaultAsync(s => s.StateID == id);

            if (state == null)
            {
                return NotFound();
            }

            return state;
        }

        // POST: api/State
        [HttpPost("add")]
        public async Task<ActionResult<State>> PostState(StateInsertModel model)
        {
            var state = new State
            {
                StateName = model.StateName,
                CountryID = model.CountryID
            };

            _context.States.Add(state);
            await _context.SaveChangesAsync();

            var request = HttpContext.Request;
            var rowPath = request.Path;
            var path = UrlTask.RemoveLastSegment(rowPath);

            var urlService = await _context.UrlServices
                .Include(u => u.RequestUrl).Include(u => u.CurrentUrl)
                .FirstOrDefaultAsync(e => e.CurrentUrl.Url == path.ToString());

            var requestUrl = "";

            if (urlService == null)
            {
                requestUrl = "dashboard";
            }
            else
            {
                requestUrl = urlService?.RequestUrl?.Url;
            }

            var url = Url.Action(nameof(GetState), new { id = state.StateID });
            return Created(url, new { state, requestUrl });
        }

        // PUT: api/State/{id}
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> PutState(int id, StateInsertModel model)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var state = await _context.States.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }

            state.StateName = model.StateName;
            state.CountryID = model.CountryID;

            _context.Entry(state).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var request = HttpContext.Request;
            var rowPath = request.Path;
            var path = UrlTask.RemoveLastSegment(rowPath);

            var urlService = await _context.UrlServices
                 .Include(u => u.RequestUrl).Include(u => u.CurrentUrl)
                 .FirstOrDefaultAsync(e => e.CurrentUrl.Url == path.ToString());

            var requestUrl = "";

            if (urlService == null)
            {
                requestUrl = "dashboard";
            }
            else
            {
                requestUrl = urlService?.RequestUrl?.Url;
            }

            return NoContent();
        }

        // DELETE: api/State/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteState(int id)
        {
            var state = await _context.States.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }

            _context.States.Remove(state);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StateExists(int id)
        {
            return _context.States.Any(e => e.StateID == id);
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
