using Microsoft.Extensions.DependencyInjection;
using Semestralka.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Semestralka
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();
        }

        public new static App Current => (App)Application.Current;

        public IServiceProvider Services { get; }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<TextEditorViemModel>();

            return services.BuildServiceProvider();
        }

        public TextEditorViemModel MainVM => Services.GetService<TextEditorViemModel>();
    }
}
