using APIannons.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIannons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdController : ControllerBase
    {
        private readonly APIDbContext _context;
        private static List<Ad> AdList { get; set; } = new();
        private static List<Category> CategoryList { get; set; } = new();

        public AdController(APIDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Ad>>> GetAll()
        {
            AdList = await _context.Ads
                .Include(ad => ad.Category).ToListAsync();
            return Ok(AdList);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Ad>> GetOne(int id)
        {
            var ad = await _context.Ads
                .Include(a => a.Category).FirstOrDefaultAsync(a => a.Id == id);
            if (ad == null)
            {
                return NotFound();
            }
            return Ok(ad);
        }
        [HttpPost]
        public async Task<ActionResult<Ad>> PostAd(NewAd newAd)
        {
            var categoryExists = await _context.Categories
       .AnyAsync(category => category.CategoryId == newAd.CategoryId);

            if (!categoryExists)
            {
                return BadRequest($"Category {newAd.CategoryId} does not exist.");
            }

            Ad ad = new Ad
            {
                Name = newAd.Name,
                Description = newAd.Description,
                Price = newAd.Price,
                Location = newAd.Location,
                PublishedDate = DateTime.Now,
                CategoryId = newAd.CategoryId,
            };
            _context.Ads.Add(ad);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOne), new { id = ad.Id }, ad);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Ad>> UpdateHero(int id, UpdateAd ad)
        {
            var adToUpdate = await _context.Ads.FirstOrDefaultAsync(a => a.Id == id);
            if (adToUpdate == null) { return NotFound(); }
            adToUpdate.Name = ad.Name;
            adToUpdate.Description = ad.Description;
            adToUpdate.Price = ad.Price;
            adToUpdate.Location = ad.Location;
            adToUpdate.CategoryId = ad.CategoryId;
            await _context.SaveChangesAsync();
            return Ok(adToUpdate);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var adToRemove = await _context.Ads.FirstOrDefaultAsync(a => a.Id == id);
            if (adToRemove == null) { return NotFound(); }
            _context.Remove(adToRemove);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
