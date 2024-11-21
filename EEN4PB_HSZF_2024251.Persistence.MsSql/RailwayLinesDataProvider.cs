using EEN4PB_HSZF_2024251.Model;
using Microsoft.EntityFrameworkCore;
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
        RailwayLine GetRailwayLineById(string id);

        List<RailwayLine> GetRailwayLines();

        //RailwayLine CRUD operations
        void CreateRailwayLine(RailwayLine railwayLine);

        IEnumerable<RailwayLine> ReadAllRailwayLines();

        void UpdateRailwayLine(RailwayLine railwayLine);

        void DeleteRailwayLine(string id);

        public void UpdateDatabase(string path);

    }

    public class RailwayLinesDataProvider : IRailwayLinesDataProvider
    {
        private readonly RailwayLinesDbContext ctx;

        public IJsonImportToDb jsonImportToDb = new JsonImportToDb();
        

        public RailwayLinesDataProvider(string path, RailwayLinesDbContext ctx)
        {
            this.ctx = ctx;
            jsonImportToDb!.JsonIntoDb(path, ctx);
        }

        public void CreateRailwayLine(RailwayLine railwayLine)
        {
            throw new NotImplementedException();
        }

        public void DeleteRailwayLine(string id)
        {
            throw new NotImplementedException();
        }

        public RailwayLine GetRailwayLineById(string id)
        {
            return ctx.RailwayLines.FirstOrDefault(x => x.Id == id);
        }

        public List<RailwayLine> GetRailwayLines()
        {
            return ctx.RailwayLines.ToList();
        }

        public IEnumerable<RailwayLine> ReadAllRailwayLines()
        {
            throw new NotImplementedException();
        }

        public void UpdateRailwayLine(RailwayLine railwayLine)
        {
            throw new NotImplementedException();
        }


        public void UpdateDatabase(string path)
        {
            jsonImportToDb!.NewJsonIntoDb(path, ctx);
        }


        /*
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
        }*/

        /*
        //JsonDeserialize method is used to deserialize the JSON data
        private List<RailwayLine> JsonDeserialize(string path)
        {
            
            string jsonString = File.ReadAllText(path);
            Console.WriteLine(jsonString);

            RailwayData railwayData = JsonConvert.DeserializeObject<RailwayData>(jsonString);
            

            return railwayData.RailwayLines;
                
        }

        //JsonToDb method is used to convert the deserialized JSON data to database entities
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
        }*/
    }
}
