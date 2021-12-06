using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DataAccess.Repos;
using Domain;
using Domain.Interfaces;
using Domain.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WpfVoetbaltruitjes
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        private readonly ServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        public App()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _configuration = builder.Build();
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(_configuration); 
            services.AddTransient<MainWindow>();
            services.AddSingleton<IKlantRepo, KlantRepo>();
            services.AddSingleton<KlantManager>();
            services.AddSingleton<IClubRepo, ClubRepo>();
            services.AddSingleton<ClubManager>();
            services.AddSingleton<IVoetbaltruitjeRepo, VoetbaltruitjeRepo>();
            services.AddSingleton<VoetbaltruitjeManager>();


        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow?.Show();
        }
    }
}
