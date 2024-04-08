namespace Umbraco_Flex.Services.Interface
{
	public interface ICommonServices
	{
		public Serilog.ILogger Logger { get; set; }

		public IHttpContextAccessor HttpContextAccessor { get; set; }

		public IWebHostEnvironment Environment { get; set; }
	}
}
