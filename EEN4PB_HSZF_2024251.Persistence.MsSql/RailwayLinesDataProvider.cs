using EEN4PB_HSZF_2024251.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEN4PB_HSZF_2024251.Persistence.MsSql
{
    public interface IRailwayLinesDataProvider
    {
        RailwayLine GetRailwayLineById(int id);

        List<RailwayLine> GetRailwayLines();

        Service GetServiceById(int id);

        List<Service> GetServices();

        //RailwayLine CRUD operations
        void CreateRailwayLine(RailwayLine railwayLine);

        IEnumerable<RailwayLine> ReadAllRailwayLines();

        void UpdateRailwayLine(RailwayLine railwayLine);

        void DeleteRailwayLine(int id);


        //Service CRUD operations
        void CreateService(Service service);

        IEnumerable<Service> ReadAllServices();

        void UpdateService(Service service);

        void DeleteService(int id);

    }

    public class RailwayLinesDataProvider
    {
        RailwayLinesDbContext ctx;

        public RailwayLinesDataProvider(RailwayLinesDbContext ctx)
        {
            this.ctx = ctx;
            JsonDeserialize();
            JsonToDb(JsonDeserialize());
            //SeedDatabase();
        }

        //SeedDatabase method is used to fill the database with some initial data for testing purposes
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

        private List<RailwayLine> JsonDeserialize()
        {
            
            string jsonString = File.ReadAllText("railwayLines.json");
            Console.WriteLine(jsonString);

            RailwayData railwayData = JsonConvert.DeserializeObject<RailwayData>(jsonString);
            

            return railwayData.RailwayLines;

            ;
        }

        private void JsonToDb(List<RailwayLine> railwayLines)
        {
            
            foreach (var railwayLine in railwayLines)
            {
                ctx.RailwayLines.Add(railwayLine);
                foreach (var service in railwayLine.Services)
                {
                    ctx.Services.Add(service);
                }
            }
            ctx.SaveChanges();
            ;
        }
    }
}
