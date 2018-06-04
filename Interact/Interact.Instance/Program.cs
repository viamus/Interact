using log4net.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Reflection;
using System.Xml;
using Microsoft.EntityFrameworkCore;
using Interact.Instance.Data.Interface;
using Interact.Instance.Data.Postgresql.InteractDomain.Context;

namespace Interact.Instance
{
    class Program
    {
        private static readonly log4net.ILog _Log = log4net.LogManager.GetLogger(typeof(Program));

        private static IConfigurationRoot _Configuration;

        private static ServiceProvider _Services;

        public static void Main(string[] args)
        {
            LoadLog4Net();
            LoadAppConfig();
            LoadInstanceServices();
        }

        private static void LoadLog4Net()
        {
            XmlDocument log4netConfig = new XmlDocument();

            log4netConfig.Load(File.OpenRead("log4net.config"));

            var repo = log4net.LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
        }

        private static void LoadAppConfig()
        {
            var builder = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _Configuration = builder.Build();
        }

        private static void LoadInstanceServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<InteractContext>(
                   options => options.UseNpgsql(
                       _Configuration.GetConnectionString("InteractConnectionString")));

            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = _Configuration.GetConnectionString("RedisConnectionString");
                option.InstanceName = "interact_redis";

            });

            _Services = services.BuildServiceProvider();
        }

        private static void LoadConsumerConfig(string threadGroup)
        {
            using (var consumerDal = _Services.GetService<IConsumerDAL>())
            {

            }
        }
    }
}
