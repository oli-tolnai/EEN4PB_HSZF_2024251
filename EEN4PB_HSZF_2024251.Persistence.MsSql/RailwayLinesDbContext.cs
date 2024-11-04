using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EEN4PB_HSZF_2024251.Persistence.MsSql
{
    public class RailwayLinesDbContext : DbContext
    {
        //Write things to the console
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });
    }
}
