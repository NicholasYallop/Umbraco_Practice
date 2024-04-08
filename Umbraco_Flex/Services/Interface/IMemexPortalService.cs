using Newtonsoft.Json.Linq;
using Umbraco_Flex.Config;
using Umbraco_Flex.Entities;

namespace Umbraco_Flex.Services.Interface
{
	public interface IMemexPortalService
	{
		Task<T?> GetAsync<T>(string path, string collectionAlias, PortalApiSearchOptions? options) where T : EntityBase, new();

		Task<List<T>?> GetListAsync<T>(string path, string collectionAlias, PortalApiSearchOptions? options) where T : EntityBase, new();
	}
}
