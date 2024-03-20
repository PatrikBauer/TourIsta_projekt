using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows.Forms;
using WindowsFormsLifetime;

namespace WinFormsApp2
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            var app = CreateHost().Build();
            using (var serviceScope = app.Services.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<Persistence>();
                context.Database.EnsureCreated();
            }

            app.Run();
        }
        public static IHostBuilder CreateHost()
        {
            return Host.CreateDefaultBuilder(Array.Empty<string>()).UseWindowsFormsLifetime<Form1>().ConfigureServices((hostContext, services) =>
            {
                services.AddDbContext<Persistence>(options => { options.UseSqlite("Data Source=database.db;Cache=Shared"); });
                services.AddSingleton<Model>();
            });
        }
    }
}