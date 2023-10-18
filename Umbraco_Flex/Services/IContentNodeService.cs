using Umbraco.Cms.Core.Models.PublishedContent;

namespace Umbraco_Flex.Services
{
	public interface IContentNodeService
	{
		public bool TryGetContentNodes(string searchString, out IEnumerable<IPublishedContent> nodes);

		public bool TryGetChildContentNodes(string searchString, int parentId, out IEnumerable<IPublishedContent> nodes);
	}
}
