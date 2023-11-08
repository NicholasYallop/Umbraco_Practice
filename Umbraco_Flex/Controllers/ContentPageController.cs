using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco_Flex.ViewModels;

namespace Umbraco_Flex.Controllers;

public class ContentPageController : RenderController
{
    private readonly ServiceContext serviceContext;
    private readonly IVariationContextAccessor variationContextAccessor;

    public ContentPageController(
            ILogger<RenderController> logger, 
            ICompositeViewEngine compositeViewEngine, 
            IUmbracoContextAccessor umbracoContextAccessor,
            ServiceContext serviceContext,
            IVariationContextAccessor variationContextAccessor) 
        : base(logger, compositeViewEngine, umbracoContextAccessor)
    {
        this.serviceContext = serviceContext;
        this.variationContextAccessor = variationContextAccessor;
    }

    public override IActionResult Index()
    {
        if (CurrentPage is null)
        {
            return CurrentTemplate(CurrentPage);
        }

        return CurrentTemplate(new ContentPageViewModel(
                    CurrentPage, 
                    new PublishedValueFallback(
                        serviceContext, 
                         variationContextAccessor
                        )));
    }
}
