namespace Umbraco_Flex.Extensions
{
	public static class LoggerExtensions
	{
		public static bool LogAndThrowError<T>(this Serilog.ILogger logger, T value, Predicate<T> validIfTrue, string errorMessage)
		{
			if (!validIfTrue.Invoke(value))
			{
				logger.Error(errorMessage);
				throw new ArgumentException(errorMessage);
			}

			return false;
		}
	}
}
