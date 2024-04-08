using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco_Flex.ViewModels.Interfaces;

namespace Umbraco_Flex.ViewModels;

public class ContentPageViewModel : ContentPage, IHasContentGrid
{
    public ContentPageViewModel(
            IPublishedContent content,
            IPublishedValueFallback publishedValueFallback)
        : base(content, publishedValueFallback)
    {
    }
}
