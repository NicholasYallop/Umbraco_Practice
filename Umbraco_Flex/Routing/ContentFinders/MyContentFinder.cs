using System.Configuration;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;

namespace Umbraco_Flex.Routing.ContentFinders
{
    public class MyContentFinder : IContentFinder
    {
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        public MyContentFinder(IUmbracoContextAccessor umbracoContextAccessor)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        public Task<bool> TryFindContent(IPublishedRequestBuilder request)
        {
            var path = request.Uri.GetAbsolutePathDecoded();
            var redirect = Consts.ROUTING_ALIAS;

            if (!path.StartsWith(redirect)) { return Task.FromResult(false); }
            if (!_umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext)) { return Task.FromResult(false); };

            var newPath = path.Trim(redirect);
            var content = umbracoContext.Content?.GetByRoute(newPath.Length > 0 ? newPath : "/");
            if (content is null) { return Task.FromResult(false); }

            request.SetPublishedContent(content);
            return Task.FromResult(true);
        }
    }
}
