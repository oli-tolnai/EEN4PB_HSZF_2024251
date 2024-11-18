using EEN4PB_HSZF_2024251.Model;
using EEN4PB_HSZF_2024251.Persistence.MsSql;

namespace EEN4PB_HSZF_2024251.Application
{
    public interface IRailwayLinesLogic
    {
        RailwayLine GetRailwayLineById(string id);

        List<RailwayLine> GetRailwayLines();

    }

    public class RailwayLinesLogic : IRailwayLinesLogic
    {
        private readonly IRailwayLinesDataProvider railwayLinesDataProvider;

        public RailwayLinesLogic(IRailwayLinesDataProvider railwayLinesDataProvider)
        {
            this.railwayLinesDataProvider = railwayLinesDataProvider;
        }

        public RailwayLine GetRailwayLineById(string id)
        {
            return railwayLinesDataProvider.GetRailwayLineById(id);
        }

        public List<RailwayLine> GetRailwayLines()
        {
            return railwayLinesDataProvider.GetRailwayLines();
        }
    }
}
