using Newtonsoft.Json;

namespace Umbraco_Flex.Entities
{
	public class ProductEntity : EntityBase
	{
		[JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("description")]
    public string? Description { get; set; }

    [JsonProperty("wattle_annualsubsfee")]
    public decimal AnnualFee { get; set; }

    [JsonProperty("price")]
    public decimal Price { get; set; }

    [JsonProperty("pricelevelid")]
    public EntityBase? PriceListId { get; set; }

    [JsonProperty("wattle_vatcodeid")]
    public EntityBase? VatId { get; set; }
    
    [JsonProperty("wattle_memberdiscountedprice")]
    public decimal MemberPrice { get; set; }

    [JsonIgnore]
    public decimal VatAdded 
    { 
        get {
            if (string.Equals(VatId, TaxCodeTypes.Standard20))
            {
                return Price * 0.2M;
            }
            return 0;
        } 
    }

    [JsonIgnore]
    public decimal FullPrice 
    {
        get {
            return Price + VatAdded;
        }
    }
	}
}

public static class TaxCodeTypes
{
  public const string Standard20 = "d3507afd-8e5e-ed11-9562-002248438f6c";
}
