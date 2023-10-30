using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Mime;
using System.Text;
using Umbraco_Flex.Config;
using Umbraco_Flex.Entities;
using Umbraco_Flex.Extensions;
using Umbraco_Flex.Services.Interface;

namespace Umbraco_Flex.Services.Concrete
{
	public class MemexPortalService : IMemexPortalService
	{
		private readonly HttpClient _httpClient;
		private readonly ICommonServices _commonServices;

		private const string ApiKeyHeader = "MemexApi-Key";
		private const string ApiUserTokenHeader = "MemexApi-OAuth";
		private const string ApiCollectionAliasHeader = "MemexApi-CA";

		public MemexPortalService(ICommonServices commonServices, IHttpClientFactory httpClientFactory, PortalConfigOptions memeExApiOptions)
		{
			_httpClient = httpClientFactory.CreateClient(PortalApiConstants.HttpClientContextKey);
			_commonServices = commonServices;

			if (!_commonServices.Logger.LogAndThrowError(
				memeExApiOptions.BaseAddress,
				address => !string.IsNullOrEmpty(address),
				"Failed to obtain 'Memex:Api:BaseAddress' from appsettings."
				))
			{
				_httpClient.BaseAddress = new Uri(memeExApiOptions.BaseAddress!);
			}

			if (!_commonServices.Logger.LogAndThrowError(
					_commonServices.HttpContextAccessor.HttpContext,
					context => context is not null,
					"Failed to obtain the HttpContext")
					&&
					!_commonServices.Logger.LogAndThrowError(
					memeExApiOptions.Key,
					key => !string.IsNullOrEmpty(key),
					"Failed to obtain 'Memex:Api:Key' from appsettings.")
				)
			{
				_httpClient.DefaultRequestHeaders.Add(ApiKeyHeader, memeExApiOptions.Key);
			}
		}

		public async Task<T?> GetAsync<T>(string path, string collectionAlias, PortalApiSearchOptions? options) where T : EntityBase, new()
		{
			var responseToken = await HandleRequestAsync(path, collectionAlias, options);
			
			var entity = responseToken?[PortalApiConstants.AttributesKey]?.ToObject<T>();
			if (entity is null)
			{
				return null;
			}

			entity.LogicalName = responseToken?[PortalApiConstants.LogicalNameKey]?.Value<string>();
			entity.Id = responseToken?[PortalApiConstants.IdKey]?.Value<string>();

			return entity;
		}

		public async Task<List<T>?> GetListAsync<T>(string path, string collectionAlias, PortalApiSearchOptions? options) where T : EntityBase, new()
		{
			var responseJToken = await HandleRequestAsync(path, collectionAlias, options);
			if (responseJToken == null)
			{
				return null;
			}

			var entityChildren = responseJToken.Children().ToList();
			if (entityChildren.Count == 0)
			{
				return null;
			}

			return entityChildren.Select(node =>
			{
				var entity = node[PortalApiConstants.AttributesKey]?.ToObject<T>();
				if (entity == null)
				{
					return new T();
				}

				entity.LogicalName = node[PortalApiConstants.LogicalNameKey]?.Value<string>();
				entity.Id = node[PortalApiConstants.IdKey]?.Value<string>();

				return entity;
			})
			.ToList();
		}


		private async Task<JToken?> HandleRequestAsync(string path, string collectionAlias, PortalApiSearchOptions? options)
		{
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri(_httpClient.BaseAddress!.AbsoluteUri + path),
			};

			if (options is not null ) 
			{
				var json = JsonConvert.SerializeObject(options, Formatting.None, 
											new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
				request.Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
			}

			if (!string.IsNullOrWhiteSpace(collectionAlias))
			{
				request.Headers.Add(ApiCollectionAliasHeader, collectionAlias);
			}

			var response = await _httpClient.SendAsync(request);
			if(!response.IsSuccessStatusCode)
			{
				return null;
			}

			var responseData = await response.Content.ReadAsStringAsync();
			JObject? responseResults;

			try
			{
				responseResults = JObject.Parse(responseData);
			}
			catch(JsonReaderException e)
			{
				_commonServices.Logger.Error(e, "Failed to parse response data");
				return null;
			}

			return responseResults[PortalApiConstants.ResponseKey]!;
		}
	}
}
