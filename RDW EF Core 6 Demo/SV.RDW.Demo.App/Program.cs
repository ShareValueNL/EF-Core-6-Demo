using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

using var log = new LoggerConfiguration()
    .WriteTo.Console(
        theme: AnsiConsoleTheme.Code,
        outputTemplate: "[{Timestamp:dd.MM.yyyy HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

Log.Logger = log;
Log.Information("ShareValue Tech Thursday - 24 februari 2022");
Log.Information("EF Core 6 Demo");
