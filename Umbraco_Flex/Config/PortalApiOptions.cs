namespace Umbraco_Flex.Config
{
	public class PortalApiSearchOptions
	{
		public Guid? Id { get; set; }

		public List<PortalApiFilter>? Filters { get; set; }
	}

	public class PortalApiFilter
	{
		public string? LogicalName { get; set; }

		public string? Operator { get; set; }

		public string? Value { get; set; }

		public bool? StringSearch { get; set; }

		public List<PortalApiFilter>? SubFilters { get; set; }
	}
}
