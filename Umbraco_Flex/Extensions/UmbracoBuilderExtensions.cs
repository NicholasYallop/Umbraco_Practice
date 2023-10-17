using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Strings;
using Umbraco_Flex.Routing.ContentFinders;
using Umbraco_Flex.Routing.SegmentProviders;
using Umbraco_Flex.Services;

namespace Umbraco_Flex.Extensions
{
    public static class UmbracoBuilderExtensions
	{
		public static IUmbracoBuilder AddCustomContentFinders(this IUmbracoBuilder builder)
		{
			// Add our custom content finder just before the core ContentFinderByUrl
			builder.ContentFinders().InsertBefore<ContentFinderByUrl, MyContentFinder>();
			return builder;
		}

		public static IUmbracoBuilder ConfigureServices(this IUmbracoBuilder builder)
		{
			builder.Services.AddTransient<IContentNodeService, ContentNodeService>();

			return builder;
		}
	}
}
