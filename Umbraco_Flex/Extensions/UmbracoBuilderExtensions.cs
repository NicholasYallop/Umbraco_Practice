using Umbraco.Cms.Core.Routing;
using Umbraco_Flex.Routing;

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
	}
}
