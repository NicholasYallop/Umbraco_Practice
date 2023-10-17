using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace Umbraco_Flex.ViewModels
{
	public class ContentGroupViewModel : ContentGroup
	{
		public ContentGroupViewModel(IPublishedContent content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
		{
		}

		public IEnumerable<IPublishedContent> Content { get; set; } = Enumerable.Empty<IPublishedContent>();
		public bool HasSearched { get; set; }
	}
}
