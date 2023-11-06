using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco_Flex.Entities;
using Umbraco_Flex.Services.Interface;

namespace Umbraco_Flex.Controllers
{
	public class MemexPortalApiController : UmbracoApiController
	{
		private readonly IMemexPortalService _portalService;

		public MemexPortalApiController(IMemexPortalService portalService)
		{
			_portalService = portalService;
		}

		[HttpGet]
		public async Task<List<object>?> GetProducts()
		{
			var products =  await _portalService.GetListAsync<ProductEntity>("api/Product/List", "ioe-product-umb-plugin", null);
			if (products == null)
			{
				return null;
			}

			return products.Select(product => new { name= product.Name, description= product.Description } as object).ToList();
		}
	}
}
