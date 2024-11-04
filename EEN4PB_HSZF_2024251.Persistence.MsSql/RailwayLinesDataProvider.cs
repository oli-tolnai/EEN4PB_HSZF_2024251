using EEN4PB_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEN4PB_HSZF_2024251.Persistence.MsSql
{
    public class RailwayLinesDataProvider
    {
        RailwayLinesDbContext ctx;

        public RailwayLinesDataProvider(RailwayLinesDbContext ctx)
        {
            this.ctx = ctx;
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            var railwayLines = new List<RailwayLine>
            {
                new RailwayLine("BP-Keleti->Szolnok", "120A"),
                new RailwayLine("BP-Nyugati->Szolnok", "100A")
            };
            ctx.RailwayLines.AddRange(railwayLines);
            ctx.SaveChanges();

            var services = new List<Service>
            {
                new Service("Szolnok", "Budapest-Keleti", 3320, 3, "InterCity"),
                new Service("Budapest-Keleti", "Sülysáp", 3210, 10, "Passenger"),
                new Service("BP-Nyugati", "Cegléd", 4320, 25, "Passenger")
            };
            ctx.Services.AddRange(services);
            ctx.SaveChanges();

            railwayLines[0].Services.Add(services[0]);
            railwayLines[0].Services.Add(services[1]);
            railwayLines[1].Services.Add(services[2]);
            ctx.SaveChanges();
        }
    }
}
