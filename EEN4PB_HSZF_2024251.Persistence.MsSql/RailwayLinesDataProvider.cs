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
        void CreateRailwayLine(RailwayLine railwayLine);

        IEnumerable<RailwayLine> ReadAllRailwayLines();

        void UpdateRailwayLine(RailwayLine railwayLine);

        void DeleteRailwayLine(string id);*/

        public void FillDatabaseWithNewData(string path);

    }

    public class RailwayLinesDataProvider : IRailwayLinesDataProvider
    {
        private readonly RailwayLinesDbContext ctx;

        public IJsonImportToDb jsonImportToDb = new JsonImportToDb();
        

        public RailwayLinesDataProvider(RailwayLinesDbContext ctx)
        {
            this.ctx = ctx;
        }

        public void FillDatabaseFirstTime(string path)
        {
            jsonImportToDb!.JsonIntoDb(path, ctx);
        }

        public void FillDatabaseWithNewData(string path)
        {
            jsonImportToDb!.NewJsonIntoDb(path, ctx);
        }
    }
}
