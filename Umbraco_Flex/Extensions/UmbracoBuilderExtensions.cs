using Polly;
using Polly.Extensions.Http;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Strings;
using Umbraco_Flex.Config;
using Umbraco_Flex.Routing.ContentFinders;
using Umbraco_Flex.Routing.SegmentProviders;
using Umbraco_Flex.Services.Concrete;
using Umbraco_Flex.Services.Interface;

namespace Umbraco_Flex.Extensions
{
	public static class UmbracoBuilderExtensions
	{
		public static IAsyncPolicy<HttpResponseMessage> MemexRetryPolicy => HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

		public static IUmbracoBuilder AddCustomContentFinders(this IUmbracoBuilder builder)
		{
			// Add our custom content finder just before the core ContentFinderByUrl
			builder.ContentFinders().InsertBefore<ContentFinderByUrl, MyContentFinder>();
			return builder;
		}

		public static IUmbracoBuilder ConfigureServices(this IUmbracoBuilder builder)
		{
			builder.Services.AddTransient<IContentNodeService, ContentNodeService>().AddScoped<ICommonServices, CommonServices>();

			return builder;
		}

		public static IUmbracoBuilder ConfigureMemexPortalService(this IUmbracoBuilder builder, PortalConfigOptions options)
		{
			if (options == null || string.IsNullOrWhiteSpace(options.BaseAddress))
			{
				throw new ArgumentNullException("Failed to obtain: 'Memex:Api:BaseAddress', from appsettings.");
			}

			builder.Services.AddHttpClient(PortalApiConstants.HttpClientContextKey, client =>
			{
				client.BaseAddress = new Uri(options.BaseAddress!);
			})
			.AddPolicyHandler(MemexRetryPolicy);

			return builder;
		}
	}
}
