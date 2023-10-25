using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco_Flex.ViewModels;

namespace Umbraco_Flex.Controllers
{
	public class ContentNodeController : RenderController
	{
		private readonly IUmbracoContextAccessor _contextAccessor;
        private readonly ServiceContext _serviceContext;
        private readonly IVariationContextAccessor _variationContextAccessor;

        public ContentNodeController(
			ILogger<RenderController> logger,
			ICompositeViewEngine compositeViewEngine,
			IUmbracoContextAccessor umbracoContextAccessor,
			ServiceContext serviceContext,
			IVariationContextAccessor variationContextAccessor) 
			: base(logger, compositeViewEngine, umbracoContextAccessor)
		{
			_contextAccessor = umbracoContextAccessor;
            _serviceContext = serviceContext;
            _variationContextAccessor = variationContextAccessor;
        }

		public IActionResult ContentNodeToHijack()
		{
			if (CurrentPage is null
				|| !_contextAccessor.TryGetUmbracoContext(out var context))
			{
				return CurrentTemplate(CurrentPage);
			}

			return View("/Views/HiddenFromUmbraco.cshtml", new ContentNodeViewModel(CurrentPage, new PublishedValueFallback(_serviceContext, _variationContextAccessor)));
		}

		public override IActionResult Index()
		{
			return CurrentTemplate(CurrentPage);
		}
	}
}
