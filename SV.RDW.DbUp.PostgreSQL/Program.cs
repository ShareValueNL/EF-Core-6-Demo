using DbUp;
using Microsoft.Extensions.Configuration;
using System.Reflection;

var connectionString = args.FirstOrDefault();

if (string.IsNullOrWhiteSpace(connectionString))
{
	var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
	var builder = new ConfigurationBuilder()
		.AddJsonFile($"appsettings.json", true, true)
		.AddJsonFile($"appsettings.{env}.json", true, true);

	var config = builder.Build();
	connectionString = config["connectionString"];
}
	

var upgrader = DeployChanges.To
				.PostgresqlDatabase(connectionString)
				.WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
				.LogToConsole().LogScriptOutput()
				.Build();

var result = upgrader.PerformUpgrade();

if (!result.Successful)
{
	Console.ForegroundColor = ConsoleColor.Red;
	Console.WriteLine(result.Error);
	Console.ResetColor();
#if DEBUG
	Console.ReadLine();
#endif
	return -1;
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("DbUp succesvol afgerond!");
Console.ResetColor();

return 0;
