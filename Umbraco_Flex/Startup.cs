using Umbraco_Flex.Config;
using Umbraco_Flex.Extensions;
using Umbraco_Flex.Routing;
using Umbraco_Flex.Routing.ContentFinders;

namespace Umbraco_Flex
{
	public class Startup
	{
		private readonly IWebHostEnvironment _env;
		private readonly IConfiguration _config;

		/// <summary>
		/// Initializes a new instance of the <see cref="Startup" /> class.
		/// </summary>
		/// <param name="webHostEnvironment">The web hosting environment.</param>
		/// <param name="config">The configuration.</param>
		/// <remarks>
		/// Only a few services are possible to be injected here https://github.com/dotnet/aspnetcore/issues/9337.
		/// </remarks>
		public Startup(IWebHostEnvironment webHostEnvironment, IConfiguration config)
		{
			_env = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
			_config = config ?? throw new ArgumentNullException(nameof(config));
		}

		/// <summary>
		/// Configures the services.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <remarks>
		/// This method gets called by the runtime. Use this method to add services to the container.
		/// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940.
		/// </remarks>
		public void ConfigureServices(IServiceCollection services)
		{
			var memexOptions = BindOptions<PortalConfigOptions>(_config, services, "Memex:Api");

			services.AddUmbraco(_env, _config)
					.ConfigureMemexPortalService(memexOptions)
					.AddBackOffice()
					.AddWebsite()
					.AddDeliveryApi()
					.AddComposers()
					.AddCustomContentFinders()
					.SetContentLastChanceFinder<MyFourOhFourFinder>()
					.ConfigureServices()
					.Build();
		}

		/// <summary>
		/// Configures the application.
		/// </summary>
		/// <param name="app">The application builder.</param>
		/// <param name="env">The web hosting environment.</param>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseUmbraco()
					.WithMiddleware(u =>
					{
						u.UseBackOffice();
						u.UseWebsite();
					})
					.WithEndpoints(u =>
					{
						u.UseInstallerEndpoints();
						u.UseBackOfficeEndpoints();
						u.UseWebsiteEndpoints();
					});
		}

		private static T BindOptions<T>(IConfiguration configuration, IServiceCollection serviceCollection, string sectionKey)
						where T : class, new()
		{
			var configMapClass = new T();

			configuration.GetSection(sectionKey).Bind(configMapClass);

			// Allow the options to be accessed via DI.
			serviceCollection.AddSingleton(configMapClass);

			return configMapClass;
		}
	}
}
