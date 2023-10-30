using Umbraco_Flex.Services.Interface;
using Serilog;

namespace Umbraco_Flex.Services.Concrete
{
	public class CommonServices : ICommonServices
	{
		public CommonServices(Serilog.ILogger logger, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env)
		{
			Logger = logger;
			HttpContextAccessor = httpContextAccessor;
			Environment = env;
		}

		public Serilog.ILogger Logger { get; set; }

		public IHttpContextAccessor HttpContextAccessor { get; set; }

		public IWebHostEnvironment Environment { get; set; }
	}
}
