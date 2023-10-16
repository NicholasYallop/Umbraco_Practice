using System.Configuration;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;

namespace Umbraco_Flex.Routing
{
	public class MyContentFinder : IContentFinder
	{
		private readonly IUmbracoContextAccessor _umbracoContextAccessor;

		public MyContentFinder(IUmbracoContextAccessor umbracoContextAccessor) {
			this._umbracoContextAccessor = umbracoContextAccessor;
		}

		public Task<bool> TryFindContent(IPublishedRequestBuilder request)
		{
			var path = request.Uri.GetAbsolutePathDecoded();
			var redirect = Consts.ROUTING_ALIAS;

			if (!path.StartsWith(redirect)) { return Task.FromResult(false); }
			if (!this._umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext)) { return Task.FromResult(false); };

			var content = umbracoContext.Content.GetByRoute(path.Trim(redirect));
			if (content is null) { return Task.FromResult(false); }

			request.SetPublishedContent(content);
			return Task.FromResult(true);
		}
	}
}
