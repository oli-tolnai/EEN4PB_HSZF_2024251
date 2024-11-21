using EEN4PB_HSZF_2024251.Model;
using EEN4PB_HSZF_2024251.Persistence.MsSql;

namespace EEN4PB_HSZF_2024251.Application
{
    public interface IRailwayLinesLogic
    {
        /*
        RailwayLine GetRailwayLineById(string id);

        List<RailwayLine> GetRailwayLines();*/

        public void FillDatabaseFirstTime(string path);

        public void FillDatabaseWithNewData(string path);


    }

    public class RailwayLinesLogic : IRailwayLinesLogic
    {
        private readonly IRailwayLinesDataProvider provider;

        public RailwayLinesLogic(IRailwayLinesDataProvider railwayLinesDataProvider)
        {
            this.provider = railwayLinesDataProvider;
        }
        public void FillDatabaseFirstTime(string path)
        {
            provider.FillDatabaseFirstTime(path);
        }

        public void FillDatabaseWithNewData(string path)
        {
            provider.FillDatabaseWithNewData(path);
        }

    }
}
