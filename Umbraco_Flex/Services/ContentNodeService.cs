using Examine;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace Umbraco_Flex.Services
{
	public class ContentNodeService : IContentNodeService
	{
		private readonly IExamineManager _examineManager;
		private readonly UmbracoHelper _umbracoHelper;

		public ContentNodeService(IExamineManager manager, UmbracoHelper umbracoHelper)
		{
			_examineManager = manager;
			_umbracoHelper = umbracoHelper;
		}

		public bool TryGetContentNodes(string searchString, out IEnumerable<ContentNode> nodes)
		{
			if(_examineManager.TryGetIndex("ExternalIndex", out IIndex? index))
			{
				nodes = index.Searcher.CreateQuery("content")
					.NodeTypeAlias("contentNode")
					.Execute()
					.Where(x => x.Values.TryGetValue("contentName", out var name) ? name.Contains(searchString ?? string.Empty) : false)
					.Select(x => int.TryParse(x.Id, out int id) ? _umbracoHelper.Content(id) as ContentNode : null)
					.WhereNotNull();

				return true;
			}

			nodes = Array.Empty<ContentNode>();
			return false;
		}
		public bool TryGetContentNodes(out IEnumerable<ContentNode> nodes)
		{
			if(_examineManager.TryGetIndex("ExternalIndex", out IIndex? index))
			{
				nodes = index.Searcher.CreateQuery("content")
					.NodeTypeAlias("contentNode")
					.Execute()
					.Select(x => int.TryParse(x.Id, out int id) ? _umbracoHelper.Content(id) as ContentNode : null)
					.WhereNotNull();

				return true;
			}

			nodes = Array.Empty<ContentNode>();
			return false;
		}
		public bool TryGetChildContentNodes(string searchString, int parentId, out IEnumerable<ContentNode> nodes)
		{
			if(_examineManager.TryGetIndex("ExternalIndex", out IIndex? index))
			{
				nodes = index.Searcher.CreateQuery("content")
					.NodeTypeAlias("contentNode")
					.And()
					.ParentId(parentId)
					.Execute()
					.Where(x => x.Values.TryGetValue("contentName", out var name) ? name?.ToLower().Contains(searchString?.ToLower() ?? string.Empty) ?? false : false)
					.Select(x => int.TryParse(x.Id, out int id) ? _umbracoHelper.Content(id) as ContentNode : null)
					.WhereNotNull();

				return true;
			}

			nodes = Array.Empty<ContentNode>();
			return false;
		}

		public bool TryGetChildContentNodes(int parentId, out IEnumerable<ContentNode> nodes)
		{
			if(_examineManager.TryGetIndex("ExternalIndex", out IIndex? index))
			{
				nodes = index.Searcher.CreateQuery("content")
					.NodeTypeAlias("contentNode")
					.And()
					.ParentId(parentId)
					.Execute()
					.Select(x => int.TryParse(x.Id, out int id) ? _umbracoHelper.Content(id) as ContentNode : null)
					.WhereNotNull();

				return true;
			}

			nodes = Array.Empty<ContentNode>();
			return false;
		}
	}
}
