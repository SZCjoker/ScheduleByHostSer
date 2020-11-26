using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace ScheduleByHostSer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).ConfigureLogging((hostContext,loggong)=>
            {                                 //設定logger 相關
                var env = hostContext.HostingEnvironment;//擷取環境內容
                var configuration = new ConfigurationBuilder()  //使用設定擋
                   .SetBasePath(Directory.GetCurrentDirectory())//設定檔路徑
                   .AddJsonFile($"appsettings.json", false, true)//讀取json格式的設定檔
                   .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true).Build(); //可以 照環境變數來決定讀取不同JSON格式的設訂檔
                loggong.AddConfiguration(configuration.GetSection("Logging"));//讀取的段落
            }).UseNLog()
                .UseStartup<Startup>();
    }
}
