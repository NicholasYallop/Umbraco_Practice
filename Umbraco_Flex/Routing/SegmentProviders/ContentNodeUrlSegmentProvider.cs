/// couldn't seem to get this into the UrlSegmentProviders collection
/// <see cref="RegisterCustomSegmentProviderComposer"/> 


using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Strings;

namespace Umbraco_Flex.Routing.SegmentProviders
{
	public class ContentNodeUrlSegmentProvider : IUrlSegmentProvider
	{
		private readonly DefaultUrlSegmentProvider _provider;

		public ContentNodeUrlSegmentProvider(IShortStringHelper stringHelper) {
			_provider = new DefaultUrlSegmentProvider(stringHelper);
		}

		public string? GetUrlSegment(IContentBase content, string? culture = null)
		{
			if (content.ContentType.Alias != "contentNode") { return  null; }

			var vanillaSegment = _provider.GetUrlSegment(content, culture);
			var nodeID = content.Id;

			return $"{vanillaSegment}--{nodeID}".ToLower();
		}
	}
}
