using EEN4PB_HSZF_2024251.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEN4PB_HSZF_2024251.Persistence.MsSql
{
    public interface IJsonImportToDb
    {
        /*
        List<RailwayLine> JsonDeserialize(string path);

        void JsonToDb(List<RailwayLine> railwayLines);*/

        void JsonIntoDb(string path, RailwayLinesDbContext ctx);
    }

    public class JsonImportToDb  : IJsonImportToDb
    {
        /*
        private readonly RailwayLinesDbContext ctx;

        public JsonImportToDb(RailwayLinesDbContext ctx)
        {
            ctx = this.ctx;
        }*/

        /*
        //JsonDeserialize method is used to deserialize the JSON data
        public List<RailwayLine> JsonDeserialize(string path)
        {
            string jsonString = File.ReadAllText(path);
            Console.WriteLine(jsonString);

            RailwayData railwayData = JsonConvert.DeserializeObject<RailwayData>(jsonString);


            return railwayData.RailwayLines;
        }

        //JsonToDb method is used to convert the deserialized JSON data to database entities
        public void JsonToDb(List<RailwayLine> railwayLines)
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
        }*/

        public void JsonIntoDb(string path, RailwayLinesDbContext ctx)
        {
            string jsonString = File.ReadAllText(path);
            Console.WriteLine(jsonString);

            RailwayData railwayData = JsonConvert.DeserializeObject<RailwayData>(jsonString);

            foreach (var railwayLine in railwayData!.RailwayLines)
            {
                ctx.RailwayLines.Add(railwayLine);
                foreach (var service in railwayLine.Services)
                {
                    ctx.Services.Add(service);
                }
            }
            ctx.SaveChanges();
        }
    }
}
