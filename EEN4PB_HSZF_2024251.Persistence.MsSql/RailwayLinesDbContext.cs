using EEN4PB_HSZF_2024251.Model;
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


        public DbSet<RailwayLine> RailwayLines { get; set; }

        public DbSet<Service> Services { get; set; }

        //Constructor, which is used to delete and create the database
        public RailwayLinesDbContext()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }

        //OnConfiguring method is used to configure the database connection
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connStr = @"Server=(localdb)\\MSSQLLocalDB;Database=RailwayLineDb;Trusted_Connection=True;TrustServerCertificate=True";

            optionsBuilder
                .UseLazyLoadingProxies()
                .UseLoggerFactory(MyLoggerFactory)
                .UseSqlServer(connStr);
        }
    }
}
