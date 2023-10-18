using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco_Flex.Services;
using Umbraco_Flex.ViewModels;

namespace Umbraco_Flex.Controllers
{
	public class ContentGroupController : RenderController
	{
		private readonly IPublishedValueFallback _publishedValueFallback;
		private readonly IContentNodeService _contentNodeService;

		public ContentGroupController(
			ILogger<RenderController> logger, 
			ICompositeViewEngine compositeViewEngine, 
			IUmbracoContextAccessor umbracoContextAccessor,
			IPublishedValueFallback publishedValueFallback,
			IContentNodeService contentNodeService)
				: base(logger, compositeViewEngine, umbracoContextAccessor)
		{
			_publishedValueFallback = publishedValueFallback;
			_contentNodeService = contentNodeService;
		}

		public IActionResult AllContent()
		{
			AllContentViewModel vm = new(CurrentPage!, _publishedValueFallback);

			return View("/Views/AllContent.cshtml", vm);
		}


		public override IActionResult Index()
		{
			ContentGroupViewModel vm = new(CurrentPage!, _publishedValueFallback);

			return View("/Views/ContentPage.cshtml", vm);
		}
	}

}
