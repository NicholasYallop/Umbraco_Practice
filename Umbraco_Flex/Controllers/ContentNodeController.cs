using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace Umbraco_Flex.Controllers
{
	public class ContentNodeController : RenderController
	{
		public ContentNodeController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor)
		{
		}

		public IActionResult ContentNodeToHijack()
		{
			return Content($"the link to \"{(CurrentPage as ContentNode)?.ContentName ?? "an unknown content node"}\" has been hijacked :)");
		}

		public override IActionResult Index()
		{
			return CurrentTemplate(CurrentPage);
		}
	}
}
