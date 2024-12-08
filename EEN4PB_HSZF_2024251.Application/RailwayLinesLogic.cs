using EEN4PB_HSZF_2024251.Model;
using EEN4PB_HSZF_2024251.Persistence.MsSql;

namespace EEN4PB_HSZF_2024251.Application
{
    public interface IRailwayLinesLogic
    {
        /*
        RailwayLine GetRailwayLineById(string id);

        List<RailwayLine> GetRailwayLines();*/

        public void CreateRailwayLine(RailwayLine railwayLine);

        public void DeleteRailwayLine(string id);

        public IQueryable<RailwayLine> GetAllRailwayLines();

        public void FillDatabase(string path);

        public void CreateRailwayLinesConsole(string lineName, string lineNumber);

        public void UpdateRailwayLineConsole(string id, string lineName, string lineNumber);

        public RailwayLine FindById(string id);

        public List<Service> GetAllServices();

        public List<string> ServicesLessThan5();

        public List<string> AverageDelays();

        public List<string> MostDelayedDestinations();
 
        public event Action RailwayLineCreatedEventHandler;
        public event Action AlreadyInUseEventHandler;
    }

    
    public class RailwayLinesLogic : IRailwayLinesLogic
    {
        private readonly IRailwayLinesDataProvider provider;
        
        public event Action RailwayLineCreatedEventHandler;
        public event Action AlreadyInUseEventHandler;

        public RailwayLinesLogic(IRailwayLinesDataProvider railwayLinesDataProvider)
        {
            this.provider = railwayLinesDataProvider;
        }

        public void CreateRailwayLine(RailwayLine railwayLine)
        {
            provider.CreateRailwayLine(railwayLine);
        }

        public void DeleteRailwayLine(string id)
        {
            provider.DeleteById(id);
        }

        public IQueryable<RailwayLine> GetAllRailwayLines()
        {
            return provider.GetAll();
        }


        public void FillDatabase(string path)
        {
            provider.FillDatabase(path);
        }
        
        public void CreateRailwayLinesConsole(string lineName, string lineNumber)
        {
            var newRailwayLine = new RailwayLine { LineName = lineName, LineNumber = lineNumber };
            if (!AlreadyExists(newRailwayLine))
            {
                provider.CreateRailwayLine(newRailwayLine);
                RailwayLineCreatedEventHandler?.Invoke();
            }
            else
            {
                AlreadyInUseEventHandler?.Invoke();
            }
        }

        public bool AlreadyExists(RailwayLine railwayLine)
        {
            var railwayLines = provider.GetAll();
            foreach (var line in railwayLines)
            {
                if (line.LineName == railwayLine.LineName || line.LineNumber == railwayLine.LineNumber)
                {
                    return true;
                }
            }
            return false;
        }

        public void UpdateRailwayLineConsole(string id, string lineName, string lineNumber)
        {
            RailwayLine railwayline = provider.FindById(id);
            provider.UpdateRailwayLineNameAndNumber(railwayline, lineName, lineNumber);
        }

        public RailwayLine FindById(string id)
        {
            return provider.FindById(id);
        }

        public List<Service> GetAllServices()
        {
            var allRailwayLines = provider.GetAll();
            List<Service> allServices = new List<Service>();
            foreach (var line in allRailwayLines)
            {
                allServices.AddRange(line.Services);
            }
            return allServices;
        }

        //The number of trains that ran with a delay of less than 5 minutes per railway line.
        public List<string> ServicesLessThan5()
        {
            List<string> lessThan5Statistics = new List<string>();

            var allServices = GetAllServices();

            foreach (var railwayline in GetAllRailwayLines())
            {
                var servicesLessThan5 = GetAllServices().Where(s => s.DelayAmount < 5 && s.RailwayLineId == railwayline.Id).Count();
                if (servicesLessThan5 > 0)
                {
                    lessThan5Statistics.Add($"{railwayline.LineNumber} - {railwayline.LineName}:\n\thas {servicesLessThan5} services that delayed less than 5 minutes.\n");
                }
            }
            return lessThan5Statistics;
        }

        //Average delays per railwayline, with data on the least and most delayed service
        public List<string> AverageDelays()
        {
            List<string> averageDelays = new List<string>();
            var allServices = GetAllServices();
            foreach (var railwayline in GetAllRailwayLines())
            {
                var services = allServices.Where(s => s.RailwayLineId == railwayline.Id).ToList();
                if (services.Count > 0)
                {
                    var averageDelay = services.Average(s => s.DelayAmount);
                    var leastDelayed = services.OrderBy(s => s.DelayAmount).First();
                    var mostDelayed = services.OrderByDescending(s => s.DelayAmount).First();
                    averageDelays.Add($"{railwayline.LineNumber} - {railwayline.LineName}:" +
                        $"\n\tAverage delay: {averageDelay} minutes" +
                        $"\n\tLeast delayed service:" +
                        $"\n\t\tFrom: {leastDelayed.From}" +
                        $"\n\t\tTo: {leastDelayed.To}" +
                        $"\n\t\tTrain Number: {leastDelayed.TrainNumber}" +
                        $"\n\t\tDelay: {leastDelayed.DelayAmount} minutes" +
                        $"\n\t\tTrain Type: {leastDelayed.TrainType}" +
                        $"\n\tMost delayed service:" +
                        $"\n\t\tFrom: {mostDelayed.From}" +
                        $"\n\t\tTo: {mostDelayed.To}" +
                        $"\n\t\tTrain Number: {mostDelayed.TrainNumber}" +
                        $"\n\t\tDelay: {mostDelayed.DelayAmount} minutes" +
                        $"\n\t\tTrain Type: {mostDelayed.TrainType}\n");
                }
            }
            return averageDelays;
        }

        //The most delayed destinations per railway line. (We do not consider a “delay” of 5 minutes or less as a delay)
        public List<string> MostDelayedDestinations()
        {
            List<string> mostDelayedDestinations = new List<string>();
            var allServices = GetAllServices();
            foreach (var railwayline in GetAllRailwayLines())
            {
                var services = allServices.Where(s => s.RailwayLineId == railwayline.Id).ToList();
                if (services.Count > 0)
                {
                    var mostDelayed = services.OrderByDescending(s => s.DelayAmount).First();
                    if (mostDelayed.DelayAmount > 5)
                    {
                        mostDelayedDestinations.Add($"{railwayline.LineNumber} - {railwayline.LineName}:" +
                            $"\n\tDestination of the most delayed service: {mostDelayed.To}\n");
                    }
                }
            }
            return mostDelayedDestinations;


        }
    
        

    }
}
