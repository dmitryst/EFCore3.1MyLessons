using Domain;
using Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using ScalarFunctionConsoleApp.Services;
using System;
using System.Threading.Tasks;

namespace ScalarFunctionConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddTransient<IPersonService, PersonService>();
            services.AddDbContext<AdvWorksDbContext>();

            var provider = services.BuildServiceProvider();
            var ps = provider.GetRequiredService<IPersonService>();

            var persons = ps.Search<Person>("Ken");

            foreach (var p in persons)
            {
                Console.WriteLine(p.ToString());
            }

            Console.WriteLine("Completed");
            Console.ReadLine();
        }
    }
}
