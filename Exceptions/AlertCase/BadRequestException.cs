namespace FraudMonitoringSystem.Exceptions.AlertCase
{
	public class BadRequestException:Exception
	{
		public BadRequestException(string message) :
				base(message)
		{
		}

	}
}
