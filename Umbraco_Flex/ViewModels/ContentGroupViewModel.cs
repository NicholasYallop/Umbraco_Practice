using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace Umbraco_Flex.ViewModels
{
	public class ContentGroupViewModel : ContentGroup
	{
		public int GroupId { get; set; }

		public ContentGroupViewModel(IPublishedContent content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
		{
			GroupId = content.Id;
		}
	}
}
