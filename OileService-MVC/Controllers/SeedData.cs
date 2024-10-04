using Microsoft.EntityFrameworkCore;
using OilChangeApp.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new OilChangeDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<OilChangeDbContext>>()))
        {
            if (!context.Cars.Any())
            {
                SeedCars(context);
            }
            if (!context.Services.Any())
            {
                SeedServices(context);
            }

            context.SaveChanges();
        }
    }

    private static void SeedServices(OilChangeDbContext context)
    {
        var service = new List<Service>();
        service.Add(new Service { ServiceName = "تعویض روغن", Price = 100000 });
        service.Add(new Service { ServiceName = "تعویض فیلتر روغن", Price = 150000 });
        service.Add(new Service { ServiceName = "تعویض فیلتر هوا", Price = 250000 });
        service.Add(new Service { ServiceName = "تعویض فیلتر بنزین", Price = 200000 });
        service.Add(new Service { ServiceName = "تعویض روغن گیربکس", Price = 300000 });
        service.Add(new Service { ServiceName = "تعویض روغن ترمز", Price = 350000 });
        service.Add(new Service { ServiceName = "تعویض روغن دیفرانسیل", Price = 400000 });
        service.Add(new Service { ServiceName = "تعویض فیلتر کابین", Price = 450000 });
        service.Add(new Service { ServiceName = "تعویض ضد یخ", Price = 500000 });
        context.Services.AddRange(service);
    }

    private static void SeedCars(OilChangeDbContext context)
    {
        // Add Iranian and Chinese cars
        var cars = new List<Car>
            {
                // Iranian cars
                new Car { Name = "پراید" },
                new Car { Name = "سمند" },
                new Car { Name = "دنا" },
                new Car { Name = "پژو ۴۰۵" },
                new Car { Name = "پژو پارس" },
                new Car { Name = "رانا" },
                new Car { Name = "تیبا" },
                new Car { Name = "آریا" },
                new Car { Name = "شاهین" },
                new Car { Name = "رانا پلاس" },
                new Car { Name = "ساینا" },
                new Car { Name = "کوییک" },
                new Car { Name = "ایران خودرو تارا" },
                new Car { Name = "ایران خودرو رونا" },
                new Car { Name = "پژو ۲۰۶" },
                new Car { Name = "پژو ۲۰۷" },
                new Car { Name = "سایپا ۱۳۱" },
                new Car { Name = "سایپا ۱۳۲" },
                new Car { Name = "سایپا ۱۵۱" },
                new Car { Name = "سایپا ۱۱۱" },
                new Car { Name = "ایران خودرو باردو" },
                new Car { Name = "پارس خودرو پی۹۰" },
                new Car { Name = "ایران خودرو سورن" },
                new Car { Name = "ایران خودرو XU7" },
                new Car { Name = "پارس خودرو زانتیا" },
                new Car { Name = "ایران خودرو دنا پلاس" },
                new Car { Name = "ایران خودرو K132" },
                new Car { Name = "ایران خودرو هایما S7" },
                new Car { Name = "سایپا شاهین پلاس" },
                new Car { Name = "سایپا آریو" },
                new Car { Name = "سایپا ساینا اس" },
                new Car { Name = "پارس خودرو رنو L90" },
                new Car { Name = "پارس خودرو برلیانس H220" },
                new Car { Name = "پارس خودرو برلیانس H230" },
                new Car { Name = "ایران خودرو پژو ۳۰۱" },
                new Car { Name = "ایران خودرو K132 توربو" },
                new Car { Name = "سایپا تیبا ۲" },
                new Car { Name = "ایران خودرو هایما S5" },
                new Car { Name = "ایران خودرو آریسان" },
                new Car { Name = "ایران خودرو رانا ال ایکس" },
                new Car { Name = "ایران خودرو دستان" },
                new Car { Name = "ایران خودرو رامین" },
                new Car { Name = "سایپا مینا" },
                new Car { Name = "سایپا رهام" },
                new Car { Name = "ایران خودرو K125" },
                new Car { Name = "ایران خودرو دنا ELX" },

                // Chinese cars
                new Car { Name = "Chery Tiggo" },
                new Car { Name = "Chery Arrizo" },
                new Car { Name = "Great Wall Wingle" },
                new Car { Name = "Haval H9" },
                new Car { Name = "Changan CS35" },
                new Car { Name = "Dongfeng S30" },
                new Car { Name = "Geely Emgrand 7" },
                new Car { Name = "Lifan X60" },
                new Car { Name = "BYD F3" },
                new Car { Name = "JAC S5" },
                new Car { Name = "JAC J4" },
                new Car { Name = "FAW Besturn B30" },
                new Car { Name = "Brilliance H330" },
                new Car { Name = "MG 360" },
                new Car { Name = "Chery Tiggo 7" },
                new Car { Name = "Chery Tiggo 8" },
                new Car { Name = "Haval H6" },
                new Car { Name = "Geely Coolray" },
                new Car { Name = "BAIC X25" },
                new Car { Name = "Dongfeng AX7" },
                new Car { Name = "Lifan 820" },
                new Car { Name = "JAC S3" },
                new Car { Name = "Great Wall Haval M4" },
                new Car { Name = "BYD S6" },
                new Car { Name = "Geely Emgrand X7" },
                new Car { Name = "Changan CS75" },
                new Car { Name = "Haval H2" },
                new Car { Name = "BAIC BJ40" },
                new Car { Name = "Dongfeng Fengon 580" },
                new Car { Name = "Brilliance V5" },
                new Car { Name = "Zotye T600" },
                new Car { Name = "BAIC Senova X55" },
                new Car { Name = "Chery E5" },
                new Car { Name = "Haval Jolion" },
                new Car { Name = "Geely Atlas" },
                new Car { Name = "JAC Refine S4" },
                new Car { Name = "Chery QQ" },
                new Car { Name = "Dongfeng Rich 6" },
                new Car { Name = "Changan Alsvin" },
                new Car { Name = "FAW Besturn T77" },
                new Car { Name = "Great Wall Poer" },
                new Car { Name = "MG ZS" }
            };

        context.Cars.AddRange(cars);
    }
}