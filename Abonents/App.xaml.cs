using Abonents.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Abonents
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IConfiguration configuration = new ConfigurationBuilder()
              .AddJsonFile("D:\\Microsoft VS\\Projects\\Abonents\\Abonents\\appsettings.json", optional: false, reloadOnChange: true)
              .Build();

            var services = new ServiceCollection();

            var serviceProvider = services.InitializeApp(configuration.ConfigureApp());

            var mainWindow = serviceProvider.GetService<MainWindow>();

            mainWindow.Show();

        }

    }

}
