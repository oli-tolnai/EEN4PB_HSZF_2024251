using EEN4PB_HSZF_2024251.Persistence.MsSql;

namespace EEN4PB_HSZF_2024251
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            var ctx = new RailwayLinesDbContext();
            var dataProvider = new RailwayLinesDataProvider(ctx);

            ;
        }
    }
}
