using EEN4PB_HSZF_2024251.Model;
using EEN4PB_HSZF_2024251.Persistence.MsSql;

namespace EEN4PB_HSZF_2024251.Application
{
    public interface IRailwayLinesLogic
    {
        RailwayLine GetRailwayLineById(string id);

        List<RailwayLine> GetRailwayLines();

        public void UpdateDatabase(string path);


    }

    public class RailwayLinesLogic : IRailwayLinesLogic
    {
        private readonly IRailwayLinesDataProvider provider;

        public RailwayLinesLogic(IRailwayLinesDataProvider railwayLinesDataProvider)
        {
            this.provider = railwayLinesDataProvider;
        }


        public RailwayLine GetRailwayLineById(string id)
        {
            return provider.GetRailwayLineById(id);
        }

        public List<RailwayLine> GetRailwayLines()
        {
            return provider.GetRailwayLines();
        }

        public void UpdateDatabase(string path)
        {
            provider.UpdateDatabase(path);
        }
    }
}
