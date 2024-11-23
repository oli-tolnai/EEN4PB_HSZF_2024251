﻿using EEN4PB_HSZF_2024251.Model;
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

        public void FillDatabaseFirstTime(string path);

        public void FillDatabaseWithNewData(string path);

        public void CreateRailwayLinesConsole(string lineName, string lineNumber);


    }

    public class RailwayLinesLogic : IRailwayLinesLogic
    {
        private readonly IRailwayLinesDataProvider provider;

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


        public void FillDatabaseFirstTime(string path)
        {
            provider.FillDatabaseFirstTime(path);
        }

        public void FillDatabaseWithNewData(string path)
        {
            provider.FillDatabaseWithNewData(path);
        }

        public void CreateRailwayLinesConsole(string lineName, string lineNumber)
        {
            var newRailwayLine = new RailwayLine { LineName = lineName, LineNumber = lineNumber };
            if (!AlreadyExists(newRailwayLine))
            {
                provider.CreateRailwayLine(newRailwayLine);
                Console.WriteLine("New railway line created");
            }
            else
            {
                System.Console.WriteLine("ERROR! One or two of the values specified are already in use.");
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


    }
}
