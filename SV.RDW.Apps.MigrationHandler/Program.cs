using Microsoft.EntityFrameworkCore;
using SV.RDW.Migrations.MySQL;

public class Program
{
    public static void Main(string[] args)
        => CreateHostBuilder(args).Build().Run();

    // EF Core uses this method at design time to access the DbContext
    public static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(
                webBuilder => webBuilder.UseStartup<Startup>());
}

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true);
        var config = builder.Build();
        string connectionStringPostgres = config["ConnectionStrings:Postgres"];
        string connectionStringMySql = config["ConnectionStrings:mysql"];


        // services.AddDbContext<PostgreSQLContext>();
        services.AddDbContext<MySQLContext>(options =>
           {
               options.UseMySql(ServerVersion.AutoDetect(connectionStringMySql));
           });
        
    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
    }
}
