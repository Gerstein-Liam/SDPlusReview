using Esri.ArcGISRuntime;
using Esri.ArcGISRuntime.Security;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using WPF.CustomArcGisLibrary.ExampleUsage;
using WPF.CustomArcGisLibrary.Lib;
using WPF.Delegates;
using WPF.Models;
using WPF.Models.Mappers;
using WPF.Services.HttpServices;
using WPF.State.Context;
using WPF.State.Navigators;
using WPF.ViewModels;
using WPF.ViewModels.Factories;
using SettingsContext = WPF.State.Context.SettingsContext;
namespace WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            InitArcGis();
            IServiceProvider serviceProvider = CreateServiceProvider();
            Window window = serviceProvider.GetRequiredService<MainWindow>();
            window.Show();
            base.OnStartup(e);
        }
        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();
            //Il manque ici un truc
            services.AddSingleton<IBackendHTTPService, BackendHTTPService>();
            services.AddSingleton<IViewModelFactory, MainNavigationViewModelFactory>();
            services.AddSingleton<IModelsMapper, ModelsMapper>();
            services.AddSingleton<IApplicationContext<Settings>, SettingsContext>();
            services.AddSingleton<AbstractMapViewModel<List<Property>, Property>, ConcreteMapViewModel>();
            services.AddSingleton<CreateViewModel<HomeViewModel>>(() => new HomeViewModel());
            services.AddSingleton<SettingsViewModel>(s => new SettingsViewModel(s.GetRequiredService<IApplicationContext<Settings>>()));
            services.AddSingleton<CreateViewModel<SettingsViewModel>>(s =>
            {
                return () => s.GetRequiredService<SettingsViewModel>();
            });
            services.AddSingleton<DashboardViewModel>(s => new DashboardViewModel(s.GetRequiredService<AbstractMapViewModel<List<Property>, Property>>()
                                                                                , s.GetRequiredService<IApplicationContext<Settings>>(),
                                                                                s.GetRequiredService<IBackendHTTPService>()
                                                                                ));
            services.AddSingleton<CreateViewModel<DashboardViewModel>>(s =>
            {
                return () => s.GetRequiredService<DashboardViewModel>();
            });
            // services.AddSingleton<ViewModelDelegateRenavigator<HomeViewModel>>();
            services.AddSingleton<INavigator, Navigator>();
            services.AddScoped<MainWindowViewModel>();
            services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainWindowViewModel>()));
            return services.BuildServiceProvider();
        }
        private void InitArcGis()
        {
            /* Authentication for ArcGIS location services:
             * Use of ArcGIS location services, including basemaps and geocoding, requires either:
             * 1) User authentication: Automatically generates a unique, short-lived access token when a user signs in to your application with their ArcGIS account
             *    giving your application permission to access the content and location services authorized to an existing ArcGIS user's account.
             * 2) API key authentication: Uses a long-lived access token to authenticate requests to location services and private content.
             *    Go to https://links.esri.com/create-an-api-key to learn how to create and manage an API key using API key credentials, and then call 
             *    .UseApiKey("Your ArcGIS location services API Key")
             *    in the initialize call below. */
            /* Licensing:
             * Production deployment of applications built with the ArcGIS Maps SDK requires you to license ArcGIS functionality.
             * For more information see https://links.esri.com/arcgis-runtime-license-and-deploy.
             * You can set the license string by calling .UseLicense(licenseString) in the initialize call below 
             * or retrieve a license dynamically after signing into a portal:
             * ArcGISRuntimeEnvironment.SetLicense(await myArcGISPortal.GetLicenseInfoAsync()); */
            try
            {
                // Initialize the ArcGIS Maps SDK runtime before any components are created.
                ArcGISRuntimeEnvironment.Initialize(config => config
                // .UseLicense("[Your ArcGIS Maps SDK license string]")
                  .UseApiKey("AAPTxy8BH1VEsoebNVZXo8HurHDDhiU1_5Jb_NeNOxSVcan1MrktnOqh5nj2nBvMq6FG3MnS8x2QOD3T_rAskwX4oZLRKjplBKvmoW-5UuFiw6gbtAR3knmyvmZfx0HMzOWeUI3YR7JmJaslshpnpYf8VgHvnOAYR8xs0cvuhs5fCLui1TZlzlIv9GCLSsV4eZuSNWG_P1CeNXsO7sWsbFdvn8FxT069zqV48DmpiV8Djis.AT1_0Tubh68E")
                  .ConfigureAuthentication(auth => auth
                     .UseDefaultChallengeHandler() // Use the default authentication dialog
                  // .UseOAuthAuthorizeHandler(myOauthAuthorizationHandler) // Configure a custom OAuth dialog
                   )
                );
                // Enable support for TimestampOffset fields, which also changes behavior of Date fields.
                // For more information see https://links.esri.com/DotNetDateTime
                ArcGISRuntimeEnvironment.EnableTimestampOffsetSupport = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ArcGIS Maps SDK runtime initialization failed.");
                // Exit application
                this.Shutdown();
            }
        }
    }
}
