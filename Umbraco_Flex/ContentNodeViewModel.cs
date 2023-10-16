using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace Umbraco_Flex
{
	public class ContentNodeViewModel : ContentNode
	{
		public ContentNodeViewModel(IPublishedContent content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
		{
		}

		public int NumberOfMs => this.ContentName?.Count(ch => ch == 'M') ?? 0;

		public string LongestWordInName => this.ContentName?.Split(" ").MaxBy(word => word.Length) ?? string.Empty;
	}
}
