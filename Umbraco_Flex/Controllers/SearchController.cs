using Microsoft.AspNetCore.Mvc;
using StackExchange.Profiling.Internal;
using System.Text.Json;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Infrastructure.Persistence.Dtos;
using Umbraco.Cms.Web.BackOffice.ModelBinders;
using Umbraco.Cms.Web.Website.Controllers;
using Umbraco_Flex.Services;

namespace Umbraco_Flex.Controllers
{
	public class SearchController : SurfaceController
	{
		private readonly IContentNodeService _contentNodeService;

		public SearchController(IContentNodeService contentNodeService, IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
		{
			_contentNodeService = contentNodeService;
		}

		public IActionResult FilteredContentNodes(string search)
		{
			_contentNodeService.TryGetContentNodes(search, out var nodes);

			return PartialView("/Views/Partials/ContentListingResults.cshtml", nodes);
		}
		
		public IActionResult FilteredChildrenContentNodes(string search, int groupId)
		{
			_contentNodeService.TryGetChildContentNodes(search, groupId, out var nodes);

			return PartialView("/Views/Partials/ContentListingResults.cshtml", nodes);
		}
	}

	public class ChildrenData
	{
		public int Id { get; set; }

		public string Url { get; set; } = string.Empty;

		public string ImgUrl { get; set; } = string.Empty;

		public string Title { get; set; } = string.Empty;
	}
}


