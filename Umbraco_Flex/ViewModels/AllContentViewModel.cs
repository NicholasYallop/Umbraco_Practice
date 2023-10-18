using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace Umbraco_Flex.ViewModels
{
	public class AllContentViewModel : ContentGroup
	{
		public AllContentViewModel(IPublishedContent content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
		{
		}
	}
}
