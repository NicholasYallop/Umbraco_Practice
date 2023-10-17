using Examine;
using System.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common;
using Umbraco_Flex.Controllers;

namespace Umbraco_Flex.Services
{
	public class ContentNodeService : IContentNodeService
	{
		private readonly IExamineManager _examineManager;
		private readonly UmbracoHelper _umbacoHelper;

		public ContentNodeService(IExamineManager manager, UmbracoHelper umbracoHelper)
		{
			_examineManager = manager;
			_umbacoHelper = umbracoHelper;
		}

		public bool TryGetContentNodes(string searchString, out IEnumerable<IPublishedContent> nodes)
		{
			if(!string.IsNullOrEmpty(searchString) && _examineManager.TryGetIndex("ExternalIndex", out IIndex? index))
			{
				nodes = index.Searcher.CreateQuery("content")
					.NodeTypeAlias("contentNode")
					//.And()
					//.Field("contentName", searchString)
					.Execute()
					.Select(x => int.TryParse(x.Id, out int id) ? _umbacoHelper.Content(id) : null)
					.WhereNotNull();

				return true;
			}

			nodes = Array.Empty<IPublishedContent>();
			return false;
		}
	}
}
