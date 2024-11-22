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
        /*
        RailwayLine GetRailwayLineById(string id);
        
        List<RailwayLine> GetRailwayLines();

        //RailwayLine CRUD operations
        

        IEnumerable<RailwayLine> ReadAllRailwayLines();

        void UpdateRailwayLine(RailwayLine railwayLine);

        void DeleteRailwayLine(string id);*/

        public void CreateRailwayLine(RailwayLine railwayLine);

        public RailwayLine FindById(string id);

        public void DeleteById(string id);

        public void Delete(RailwayLine railwayLine);

        public IQueryable<RailwayLine> GetAll();

        public void Update(RailwayLine railwayLine);

        public void FillDatabaseFirstTime(string path);

        public void FillDatabaseWithNewData(string path);

    }

    public class RailwayLinesDataProvider : IRailwayLinesDataProvider
    {
        private readonly RailwayLinesDbContext ctx;

        public RailwayLinesDataProvider(RailwayLinesDbContext ctx)
        {
            this.ctx = ctx;
        }

        //CRUD operations: Create, Read, Update and Delete RailwayLine
        public void CreateRailwayLine(RailwayLine railwayLine)
        {
            ctx.Set<RailwayLine>().Add(railwayLine);
            ctx.SaveChanges();
        }

        public RailwayLine FindById(string id)
        {
            return ctx.Set<RailwayLine>().First(t => t.Id == id);
        }

        public void DeleteById(string id)
        {
            var railwayLine = FindById(id);
            ctx.Set<RailwayLine>().Remove(railwayLine);
            ctx.SaveChanges();
        }

        public void Delete(RailwayLine railwayLine)
        {
            ctx.Set<RailwayLine>().Remove(railwayLine);
            ctx.SaveChanges();
        }

        public IQueryable<RailwayLine> GetAll()
        {
            return ctx.Set<RailwayLine>();
        }

        public void Update(RailwayLine railwayLine)
        {
            var old = FindById(railwayLine.Id);

            foreach (var prop in typeof(RailwayLine).GetProperties())
            {
                prop.SetValue(old, prop.GetValue(railwayLine));
            }
            ctx.Set<RailwayLine>().Update(old);
            ctx.SaveChanges();
        }


        public void FillDatabaseFirstTime(string path)
        {
            string jsonString = File.ReadAllText(path);
            //Console.WriteLine(jsonString);

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

        public void FillDatabaseWithNewData(string path)
        {
            string jsonString = File.ReadAllText(path);
            //Console.WriteLine(jsonString);

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



    }
}
