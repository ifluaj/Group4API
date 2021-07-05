// https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-5.0&tabs=visual-studio
// https://entityframework.net/linq-queries

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BapApi.Models;

namespace BapApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreAppsController : ControllerBase
    {
        private readonly StoreAppsContext _context;

        public StoreAppsController(StoreAppsContext context)
        {
            _context = context;
        }

        // GET: api/StoreApps (StoreApps as in StoreAppsController, line 17)
        // Get all the data from the database
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<StoreAppDTO>>> GetStoreApps()
        {
            return await _context.StoreApps.Select(x => StoreAppToDTO(x)).ToListAsync();
        }

        // GET: api/StoreApps/1
        // Get a single row from the database by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<StoreAppDTO>> GetStoreApp(int id)
        {
            var storeApp = await _context.StoreApps.FindAsync(id);

            if (storeApp == null)
            {
                return NotFound();
            }

            return StoreAppToDTO(storeApp);
        }

        // GET: api/StoreApps/FirstTen
        // Get the first ten results from the database aftering ordering the data by Id
        [HttpGet("FirstTen")]
        public async Task<ActionResult<IEnumerable<StoreAppDTO>>> GetStoreTopTen()
        {

            var storeTopTen = await _context.StoreApps.Select(x => StoreAppToDTO(x)).Take(10).ToListAsync();

            if (storeTopTen == null)
            {
                return NotFound();
            }
            
            return storeTopTen; 
        }
        // GET: api/StoreApps/MostPopular
        // Get the top 100 apps from the database, aftering ordering by rating and by no. of ratings.
        [HttpGet("MostPopular")]
        public async Task<ActionResult<IEnumerable<StoreAppDTO>>> GetStoreAnalysis()
        {

            var storeAnalysis = await _context.StoreApps.OrderByDescending(r => r.Rating).ThenByDescending(p => p.People).Select(x => StoreAppToDTO(x)).Take(100).ToListAsync();

            if (storeAnalysis == null)
            {
                return NotFound();
            }

            return storeAnalysis;
        }

        // GET: api/StoreApps/MostRated
        // Get the 100 most rated apps from the database, ordering by no. of ratings.
        [HttpGet("MostRated")]
        public async Task<ActionResult<IEnumerable<StoreAppDTO>>> GetStoreRated()
        {



            var storeRated = await _context.StoreApps.OrderByDescending(r => r.People).Select(x => StoreAppToDTO(x)).Take(100).ToListAsync();



            if (storeRated == null)
            {
                return NotFound();
            }



            return storeRated;
        }



        // GET: api/StoreApps/New
        // Get the top 100 new apps from the database, aftering ordering by rating and by date added.
        [HttpGet("New")]
        public async Task<ActionResult<IEnumerable<StoreAppDTO>>> GetStoreNew()
        {



            var storeNew = await _context.StoreApps.OrderByDescending(r => r.Rating).ThenByDescending(d => d.Date).Select(x => StoreAppToDTO(x)).Take(100).ToListAsync();



            if (storeNew == null)
            {
                return NotFound();
            }



            return storeNew;
        }

        // POST: api/StoreApps
        // Add a new record to the database

        // Delete: api/StoreApps/1
        // Delete a single row from the database by Id

        // DTO helper method. "Production apps typically limit the data that's input and returned using a subset of the model"
        private static StoreAppDTO StoreAppToDTO(StoreApp storeApp) =>
            new StoreAppDTO
            {
                Id = storeApp.Id,
                Name = storeApp.Name,
                Rating = storeApp.Rating,
                People = storeApp.People,
                Category = storeApp.Category,
                Date = storeApp.Date,
                Price = storeApp.Price
            };
    }
    //This is Chloes test comment
}
