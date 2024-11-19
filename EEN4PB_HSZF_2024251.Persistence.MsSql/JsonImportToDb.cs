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
        public void JsonIntoDbUpdate(string path, RailwayLinesDbContext ctx);
    }

    public class JsonImportToDb  : IJsonImportToDb
    {
        /*
        private readonly RailwayLinesDbContext ctx;

        public JsonImportToDb(RailwayLinesDbContext ctx)
        {
            ctx = this.ctx;
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

        //Import another JSON file to the database, if it contains the same RailwayLine which means the LineNumber and the LineName are the same, then only add the NEW services to the existing RailwayLine. Servicies are equal if the TrainNumber, From, To, DelayAmount and TrainType are the same. If the railwayline new then add the whole railwayline and its services to the database
        public void JsonIntoDbUpdate(string path, RailwayLinesDbContext ctx)
        {
            string jsonString = File.ReadAllText(path);
            Console.WriteLine(jsonString);

            RailwayData railwayData = JsonConvert.DeserializeObject<RailwayData>(jsonString);

            foreach (var railwayLine in railwayData!.RailwayLines)
            {
                var existingRailwayLine = ctx.RailwayLines.FirstOrDefault(x => x.LineNumber == railwayLine.LineNumber && x.LineName == railwayLine.LineName);
                if (existingRailwayLine != null)
                {
                    foreach (var service in railwayLine.Services)
                    {
                        var existingService = ctx.Services.FirstOrDefault(x => x.TrainNumber == service.TrainNumber && x.From == service.From && x.To == service.To && x.DelayAmount == service.DelayAmount && x.TrainType == service.TrainType);
                        if (existingService == null)
                        {
                            existingRailwayLine.Services.Add(service);
                        }
                    }
                }
                else
                {
                    ctx.RailwayLines.Add(railwayLine);
                    foreach (var service in railwayLine.Services)
                    {
                        ctx.Services.Add(service);
                    }
                }
            }
            ctx.SaveChanges();
        }




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


    }
}
