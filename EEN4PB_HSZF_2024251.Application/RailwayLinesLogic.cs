using EEN4PB_HSZF_2024251.Model;
using EEN4PB_HSZF_2024251.Persistence.MsSql;

namespace EEN4PB_HSZF_2024251.Application
{
    public interface IRailwayLinesLogic
    {
        /*
        RailwayLine GetRailwayLineById(string id);

        List<RailwayLine> GetRailwayLines();*/


        public void FillDatabaseFirstTimeWithProvider(string path);

        public void FillDatabaseWithNewDataWithProvider(string path);


    }

    public class RailwayLinesLogic : IRailwayLinesLogic
    {
        private readonly IRailwayLinesDataProvider provider;

        public RailwayLinesLogic(IRailwayLinesDataProvider railwayLinesDataProvider)
        {
            this.provider = railwayLinesDataProvider;
        }

        public void FillDatabaseFirstTimeWithProvider(string path)
        {
            provider.FillDatabaseWithNewData(path);
        }

        public void FillDatabaseWithNewDataWithProvider(string path)
        {
            provider.FillDatabaseWithNewData(path);
        }

    }
}
