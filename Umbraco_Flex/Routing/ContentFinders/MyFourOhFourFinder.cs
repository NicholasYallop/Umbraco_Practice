using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;

namespace Umbraco_Flex.Routing.ContentFinders
{
    public class MyFourOhFourFinder : IContentLastChanceFinder
    {
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        public MyFourOhFourFinder(IUmbracoContextAccessor contextAccessor)
        {
            _umbracoContextAccessor = contextAccessor;
        }

        public Task<bool> TryFindContent(IPublishedRequestBuilder request)
        {
            if (!_umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContenxt)) { return Task.FromResult(false); }

            var content = umbracoContenxt.Content?.GetAtRoot().FirstOrDefault(page => page.Name == "FourOhFour");

            request.SetPublishedContent(content);

            return Task.FromResult(true);
        }
    }
}