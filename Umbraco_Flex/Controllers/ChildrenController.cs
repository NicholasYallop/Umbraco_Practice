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

namespace Umbraco_Flex.Controllers
{
	public class ChildrenController : SurfaceController
	{
		public ChildrenController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
		{
		}

		public JsonResult GetChildrenAsJson(int id, string crit)
		{
			var children = UmbracoContext.Content?.GetById(id)?.Children;

			if (children == null) return new JsonResult(null);

			return new JsonResult(
				children
				.Where(child => 
						child.Value<string>("contentName")?.ToUpper()
						.Contains(crit?.ToUpper() ?? string.Empty) ?? false)
				.Select(child => new ChildrenData
			{
				Id = child.Id,
				Url = child.Url(),
				ImgUrl = child.Value<MediaWithCrops>("picture")?.Content.Url() ?? "no value",
				Title = child.Value<string>("contentName") ?? string.Empty
			}));
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


