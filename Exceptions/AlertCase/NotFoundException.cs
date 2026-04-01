namespace FraudMonitoringSystem.Exceptions.AlertCase
{
	
		public class NotFoundException : System.Exception
		{
			public NotFoundException()

					: base("The requested resource was not found.")

			{ }

			public NotFoundException(string message)

				: base(message)

			{ }

			public NotFoundException(string message, System.Exception innerException)

				: base(message, innerException)

			{

			}
		}
}
