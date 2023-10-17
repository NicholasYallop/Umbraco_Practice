using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco_Flex.ViewModels;

namespace Umbraco_Flex.Controllers
{
	public class ContentNodeController : RenderController
	{
		private readonly IUmbracoContextAccessor _contextAccessor;

		public ContentNodeController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor)
		{
			_contextAccessor = umbracoContextAccessor;
		}

		public IActionResult ContentNodeToHijack()
		{
			return View("/Views/HiddenFromUmbraco.cshtml", new ContentNodeViewModel(CurrentPage, null));
		}

		public override IActionResult Index()
		{
			return CurrentTemplate(CurrentPage);
		}
	}
}
