using DeluxeEdit.Extensions.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DeluxeEdit.Extensions
{
    public static class Util
    {
        /// <summary>
        /// retrieves automatically information about the application such as Namw, Version and Environment
        /// Works with ASPNETCORE_ENVIRONMENT environment variable
        /// </summary>
        /// <returns></returns>
        public static AppInfo GetAppInfo() 
        {
            var result=new AppInfo();
            result.Name = PlatformServices.Default.Application.ApplicationName;
            result.Version = PlatformServices.Default.Application.ApplicationVersion;
            var env = AppEnvironment.Debug;
            var envData= Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            Enum.TryParse<AppEnvironment>(envData, true, out env);

            result.Environment = env;

            return result;
        }
        /// <summary>
        /// Loads appsettings regrdlwss of project type
        /// Also loads a environment appsettings
        /// </summary>
        /// <returns></returns>
        public static IConfigurationRoot BuildConfiuration()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(System.IO.Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true);
            var result  = builder.Build();
            return result;

        }


        /// <summary>
        /// We go through all PATHs, where there exists a scalc.exe        /// </summary>
        /// <returns></returns>
        public static string GetOfficeCalculatorPath()
        {
            var exeName = "scalc.exe";
            var paths = Environment.GetEnvironmentVariable("PATH").Split(";");
            var result = paths.Where(p => File.Exists(Path.Combine(p, exeName))).Select(r => Path.Combine(r, exeName)).FirstOrDefault();
            return result;
        }


        /// <summary>
        /// Corresponds to DayOfWeek 
        /// </summary>
        public static IEnumerable<Weekday> GetAllWeekdays()
        {
            var result = new List<Weekday> {
                new Weekday { Id=1, Name="Måndag" , SortOrder=1},
                new Weekday { Id=2, Name="Tisdag" , SortOrder=2},
                new Weekday { Id=3, Name="Onsdag" , SortOrder=3},
                new Weekday { Id=4, Name="Torsdag" , SortOrder=4},
                new Weekday { Id=5, Name="Freday" , SortOrder=5},
                new Weekday { Id=6, Name="Lördag" , SortOrder=6},
                new Weekday { Id=0, Name="Söndag" , SortOrder=7},
            };
            return result;
        }
    public static string NormalizePostCode(string value)
        {

            var digits = (value + "").Trim().ToCharArray().Where(c => Char.IsDigit(c)).ToArray();
            var result = new string(digits).ToInt().ToString();
            return result;
        }
        public static string NormalizeMobileNumber(string value)
        {
            var digits = new string((value + "").Trim().ToCharArray().Where(c => Char.IsDigit(c)).ToArray());

            if (digits.StartsWith("07")) digits = digits.Substring(1);

            if (digits.StartsWith("46"))  digits=digits.Substring(2);

            var result = $"+46{digits}";
            return result;
        }

        public static List<string> GetEnumNames<T>() where T : Enum
        {
            var result=Enum.GetNames(typeof(T)).ToList();
            return result;
        }


    }

}
