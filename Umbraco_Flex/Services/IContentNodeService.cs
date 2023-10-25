using Umbraco.Cms.Web.Common.PublishedModels;

namespace Umbraco_Flex.Services
{
	public interface IContentNodeService
	{
		public bool TryGetContentNodes(string searchString, out IEnumerable<ContentNode> nodes);

		public bool TryGetContentNodes(out IEnumerable<ContentNode> nodes);

		public bool TryGetChildContentNodes(string searchString, int parentId, out IEnumerable<ContentNode> nodes);

		public bool TryGetChildContentNodes(int parentId, out IEnumerable<ContentNode> nodes);
	}
}
