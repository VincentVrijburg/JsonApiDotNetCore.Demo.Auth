using System;
using System.Linq;
using JsonApiDotNetCore.Demo.Auth.Data.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace JsonApiDotNetCore.Demo.Auth.Data.Seeds
{
    public class CarSeed
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            using var serviceScope = serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<DataContext>();
                
            var cars = new []
            {
                new Car
                {
                    Id = 1,
                    Brand = "Tesla",
                    Model = "Model S",
                    Released = DateTime.Parse("2012-06-22")
                },
                new Car
                {
                    Id = 2,
                    Brand = "Tesla",
                    Model = "Model x",
                    Released = DateTime.Parse("2015-09-01")
                },
                new Car
                {
                    Id = 3,
                    Brand = "Tesla",
                    Model = "Model 3",
                    Released = DateTime.Parse("2017-07-07")
                }
            };

            foreach (var car in cars)
            {
                if (context.Car.FirstOrDefault(c => c.Id == car.Id) == null)
                {
                    await context.AddAsync(car);
                }
            }
            
            await context.SaveChangesAsync();
        }
    }
}