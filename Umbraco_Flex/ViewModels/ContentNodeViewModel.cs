using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace Umbraco_Flex.ViewModels
{
    public class ContentNodeViewModel : ContentNode
    {
        public ContentNodeViewModel(IPublishedContent content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
        {
        }

        public int NumberOfMs => ContentName?.Count(ch => ch == 'M' || ch=='m') ?? 0;

        public string LongestWordInName => ContentName?.Split(" ").MaxBy(word => word.Length) ?? string.Empty;
    }
}
