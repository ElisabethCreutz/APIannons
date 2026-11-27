using Microsoft.EntityFrameworkCore;

namespace APIannons.Models
{
    public class DataInitializer
    {
        private readonly APIDbContext _dbContext;
        public DataInitializer(APIDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void MigrateData()
        {
            _dbContext.Database.Migrate();
            SeedData();
            _dbContext.SaveChanges();
        }
        public void SeedData()
        {
            if (!_dbContext.Categories.Any())
            {
                _dbContext.AddRange(new Category
                {
                    CategoryName = "Furniture"
                },
                new Category
                {
                    CategoryName = "Lamps"
                },
                 new Category
                 {
                     CategoryName = "Clothes"
                 });
            }
            _dbContext.SaveChanges();
            if (!_dbContext.Ads.Any())
            {
                var furnitureCategory = _dbContext.Categories
           .Single(category => category.CategoryName == "Furniture");

                _dbContext.Add(new Ad
                {
                    Name = "Furniture sale",
                    Description = "I'm selling all my furniture from my five bedroom house (including garden furniture and tools). Starting price for each item noted here.",
                    Price = 100,
                    Location = "Stockholm",
                    PublishedDate = DateTime.Now,
                    CategoryId = furnitureCategory.CategoryId
                });
            }
        }
    }
}
