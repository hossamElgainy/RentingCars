
using Core.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Seeding
{
    public class DataSeeder(AppDbContext _context)
    {
        
        public async Task SeedAsync()
        {
            await SeedBrandsAsync();
           await SeedCarsAsync();
        }
        public static readonly Guid ToyotaId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        public static readonly Guid HondaId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        public static readonly Guid BMWId = Guid.Parse("33333333-3333-3333-3333-333333333333");
        public static readonly Guid MercedesBenzId = Guid.Parse("44444444-4444-4444-4444-444444444444");
        public static readonly Guid AudiId = Guid.Parse("55555555-5555-5555-5555-555555555555");
        public static readonly Guid FordId = Guid.Parse("66666666-6666-6666-6666-666666666666");
        public static readonly Guid ChevroletId = Guid.Parse("77777777-7777-7777-7777-777777777777");
        public static readonly Guid TeslaId = Guid.Parse("88888888-8888-8888-8888-888888888888");
        public static readonly Guid PorscheId = Guid.Parse("99999999-9999-9999-9999-999999999999");
        public static readonly Guid LandRoverId = Guid.Parse("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA");
        public static readonly Guid LexusId = Guid.Parse("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB");
        public static readonly Guid JeepId = Guid.Parse("CCCCCCCC-CCCC-CCCC-CCCC-CCCCCCCCCCCC");
        public static readonly Guid NissanId = Guid.Parse("DDDDDDDD-DDDD-DDDD-DDDD-DDDDDDDDDDDD");
        public static readonly Guid HyundaiId = Guid.Parse("EEEEEEEE-EEEE-EEEE-EEEE-EEEEEEEEEEEE");
        public async Task SeedBrandsAsync()
        {
            var brands =   new List<Brand>
        {
            new Brand { Id = ToyotaId, Name = "Toyota", Country = "Japan", FoundedYear = 1937, LogoUrl = "https://logo.clearbit.com/toyota.com" },
            new Brand { Id = HondaId, Name = "Honda", Country = "Japan", FoundedYear = 1948, LogoUrl = "https://logo.clearbit.com/honda.com" },
            new Brand { Id = BMWId, Name = "BMW", Country = "Germany", FoundedYear = 1916, LogoUrl = "https://logo.clearbit.com/bmw.com" },
            new Brand { Id = MercedesBenzId, Name = "Mercedes-Benz", Country = "Germany", FoundedYear = 1926, LogoUrl = "https://logo.clearbit.com/mercedes-benz.com" },
            new Brand { Id = AudiId, Name = "Audi", Country = "Germany", FoundedYear = 1909, LogoUrl = "https://logo.clearbit.com/audi.com" },
            new Brand { Id = FordId, Name = "Ford", Country = "USA", FoundedYear = 1903, LogoUrl = "https://logo.clearbit.com/ford.com" },
            new Brand { Id = ChevroletId, Name = "Chevrolet", Country = "USA", FoundedYear = 1911, LogoUrl = "https://logo.clearbit.com/chevrolet.com" },
            new Brand { Id = TeslaId, Name = "Tesla", Country = "USA", FoundedYear = 2003, LogoUrl = "https://logo.clearbit.com/tesla.com" },
            new Brand { Id = PorscheId, Name = "Porsche", Country = "Germany", FoundedYear = 1931, LogoUrl = "https://logo.clearbit.com/porsche.com" },
            new Brand { Id = LandRoverId, Name = "Land Rover", Country = "UK", FoundedYear = 1948, LogoUrl = "https://logo.clearbit.com/landrover.com" },
            new Brand { Id = LexusId, Name = "Lexus", Country = "Japan", FoundedYear = 1989, LogoUrl = "https://logo.clearbit.com/lexus.com" },
            new Brand { Id = JeepId, Name = "Jeep", Country = "USA", FoundedYear = 1941, LogoUrl = "https://logo.clearbit.com/jeep.com" },
            new Brand { Id = NissanId, Name = "Nissan", Country = "Japan", FoundedYear = 1933, LogoUrl = "https://logo.clearbit.com/nissan.com" },
            new Brand { Id = HyundaiId, Name = "Hyundai", Country = "South Korea", FoundedYear = 1967, LogoUrl = "https://logo.clearbit.com/hyundai.com" }
        };

            // Check if the stages already exist
            var existingBrands = await _context.Brands.ToListAsync();

            foreach (var brand in brands)
            {
                var existingRole = existingBrands.FirstOrDefault(e => e.Name.ToLower() == brand.Name.ToLower());
                if (existingRole == null)
                {
                    await _context.Brands.AddAsync(brand);
                }
            }

            await _context.SaveChangesAsync();
        }
        public async Task SeedCarsAsync()
        {
            var cars = new List<Car>
    {
        // Toyota
        new Car { Id = Guid.NewGuid(),TotalCount=10, ModelName = "Camry XSE", ModelType = "Sedan", ModelYear = "2023", BrandId = ToyotaId, Power = 301 },
        new Car { Id = Guid.NewGuid(),TotalCount=5, ModelName = "RAV4 Hybrid", ModelType = "SUV", ModelYear = "2023", BrandId = ToyotaId, Power = 219 },
        new Car { Id = Guid.NewGuid(),TotalCount=2, ModelName = "Tacoma TRD Pro", ModelType = "Pickup", ModelYear = "2023", BrandId = ToyotaId, Power = 278 },
        
        // Honda
        new Car { Id = Guid.NewGuid(),TotalCount=3, ModelName = "Accord Touring", ModelType = "Sedan", ModelYear = "2023", BrandId = HondaId, Power = 192 },
        new Car { Id = Guid.NewGuid(),TotalCount=1, ModelName = "CR-V EX-L", ModelType = "SUV", ModelYear = "2023", BrandId = HondaId, Power = 190 },
        
        // BMW
        new Car { Id = Guid.NewGuid(),TotalCount=2, ModelName = "330i", ModelType = "Sedan", ModelYear = "2023", BrandId = BMWId, Power = 255 },
        new Car { Id = Guid.NewGuid(),TotalCount=5, ModelName = "X5 xDrive40i", ModelType = "SUV", ModelYear = "2023", BrandId = BMWId, Power = 335 },
        new Car { Id = Guid.NewGuid(),TotalCount=3, ModelName = "M440i", ModelType = "Coupe", ModelYear = "2023", BrandId = BMWId, Power = 382 },
        
        // Mercedes-Benz
        new Car { Id = Guid.NewGuid(),TotalCount=2, ModelName = "C300", ModelType = "Sedan", ModelYear = "2023", BrandId = MercedesBenzId, Power = 255 },
        new Car { Id = Guid.NewGuid(),TotalCount=1, ModelName = "GLE 350", ModelType = "SUV", ModelYear = "2023", BrandId = MercedesBenzId, Power = 255 },
        
        // Audi
        new Car { Id = Guid.NewGuid(),TotalCount=4, ModelName = "A4 Premium", ModelType = "Sedan", ModelYear = "2023", BrandId = AudiId, Power = 201 },
        new Car { Id = Guid.NewGuid(),TotalCount=2, ModelName = "Q5 Premium", ModelType = "SUV", ModelYear = "2023", BrandId = AudiId, Power = 261 },
        
        // Ford
        new Car { Id = Guid.NewGuid(),TotalCount=3, ModelName = "Mustang GT", ModelType = "Coupe", ModelYear = "2023", BrandId = FordId, Power = 450 },
        new Car { Id = Guid.NewGuid(),TotalCount=2, ModelName = "F-150 Lariat", ModelType = "Pickup", ModelYear = "2023", BrandId = FordId, Power = 400 },
        new Car { Id = Guid.NewGuid(),TotalCount=1, ModelName = "Explorer ST", ModelType = "SUV", ModelYear = "2023", BrandId = FordId, Power = 400 },
        
        // Chevrolet
        new Car { Id = Guid.NewGuid(),TotalCount=2, ModelName = "Camaro SS", ModelType = "Coupe", ModelYear = "2023", BrandId = ChevroletId, Power = 455 },
        new Car { Id = Guid.NewGuid(),TotalCount=2, ModelName = "Tahoe Premier", ModelType = "SUV", ModelYear = "2023", BrandId = ChevroletId, Power = 355 },
        
        // Tesla
        new Car { Id = Guid.NewGuid(),TotalCount=1, ModelName = "Model 3 Long Range", ModelType = "Sedan", ModelYear = "2023", BrandId = TeslaId, Power = 346 },
        new Car { Id = Guid.NewGuid(),TotalCount=1, ModelName = "Model Y Performance", ModelType = "SUV", ModelYear = "2023", BrandId = TeslaId, Power = 456 },
        
        // Luxury/Sports
        new Car { Id = Guid.NewGuid(),TotalCount=2, ModelName = "Porsche 911 Carrera", ModelType = "Coupe", ModelYear = "2023", BrandId = LexusId, Power = 379 },
        new Car { Id = Guid.NewGuid(),TotalCount=2, ModelName = "Range Rover Velar", ModelType = "SUV", ModelYear = "2023", BrandId = LexusId, Power = 247 },
        new Car { Id = Guid.NewGuid(),TotalCount=2, ModelName = "Lexus RX 350", ModelType = "SUV", ModelYear = "2023", BrandId = LexusId, Power = 275 },
        new Car { Id = Guid.NewGuid(),TotalCount=2, ModelName = "Jeep Wrangler Rubicon", ModelType = "SUV", ModelYear = "2023", BrandId = LexusId, Power = 285 },
        
        // Economy
        new Car { Id = Guid.NewGuid(),TotalCount=3, ModelName = "Nissan Altima SR", ModelType = "Sedan", ModelYear = "2023", BrandId = NissanId, Power = 188 },
        new Car { Id = Guid.NewGuid(),TotalCount=1, ModelName = "Hyundai Elantra Limited", ModelType = "Sedan", ModelYear = "2023", BrandId = NissanId, Power = 147 }
    };
            // Check if the stages already exist
            var existingcars = await _context.Cars.ToListAsync();

            foreach (var brand in cars)
            {
                var existingRole = existingcars.FirstOrDefault(e => e.ModelName.ToLower() == brand.ModelName.ToLower());
                if (existingRole == null)
                {
                    await _context.Cars.AddAsync(brand);
                }
            }

            await _context.SaveChangesAsync();
        }


    }
}
