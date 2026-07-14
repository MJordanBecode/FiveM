using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Data
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Contains("--reset-db"))
            {
                LaunchResetDbScript();
                return;
            }

            using var host = CreateHostBuilder(args).Build();

            Console.WriteLine("Projet Data prêt.");
            Console.WriteLine("Pour reset la DB : dotnet run --project .\\Data\\Data.csproj -- --reset-db");
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var solutionRoot = Path.GetFullPath(
                Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..")
            );

            var dataProjectDir = Path.Combine(solutionRoot, "Data");

            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(config =>
                {
                    config.SetBasePath(dataProjectDir);
                    config.AddJsonFile("appsettings.json", optional: false);
                    config.AddJsonFile("appsettings.Development.json", optional: true);
                    config.AddJsonFile("appsettings.Production.json", optional: true);
                })
                .ConfigureServices((context, services) =>
                {
                    var connectionString = context.Configuration
                        .GetConnectionString("DefaultConnection")
                        ?? throw new Exception("Connection string introuvable.");

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseMySql(
                            connectionString,
                            ServerVersion.AutoDetect(connectionString)
                        );
                    });
                });
        }

        private static void LaunchResetDbScript()
        {
            var solutionRoot = FindSolutionRoot();

            var scriptPath = Path.Combine(
                solutionRoot,
                "Scripts",
                "reset-db.ps1"
            );

            Console.WriteLine($"Recherche du script : {scriptPath}");

            if (!File.Exists(scriptPath))
            {
                Console.WriteLine($"Script introuvable : {scriptPath}");
                Console.ReadKey();
                return;
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = $"-ExecutionPolicy Bypass -NoExit -File \"{scriptPath}\"",
                UseShellExecute = true,
                WorkingDirectory = solutionRoot
            });

            Console.WriteLine("Script reset-db.ps1 lancé.");
        }

        private static string FindSolutionRoot()
        {
            var directory = new DirectoryInfo(AppContext.BaseDirectory);

            while (directory != null)
            {
                var scriptsFolder = Path.Combine(directory.FullName, "Scripts");

                if (Directory.Exists(scriptsFolder))
                {
                    return directory.FullName;
                }

                directory = directory.Parent;
            }

            throw new DirectoryNotFoundException(
                "Impossible de trouver la racine CsharpCore."
            );
        }
    }
}