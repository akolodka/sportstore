using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            StoreDbContext context = app.ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<StoreDbContext>();

            if(context.Database.GetPendingMigrations().Any() == true)
            {
                context.Database.Migrate();
            }

            if(context.Products.Any() == false)
            {
                context.Products.AddRange(

                    new Product{
                        Name = "Kayak", 
                        Description = "A boat for one person", 
                        Category = "Watersports", 
                        Price = 275
                    },
                    new Product{
                        Name = "Lifejacket", 
                        Description = "Protective and fashionable", 
                        Category = "Watersports", 
                        Price = 48.95m
                    }
                    
                );

                context.SaveChanges();
            }
        }
    }
}