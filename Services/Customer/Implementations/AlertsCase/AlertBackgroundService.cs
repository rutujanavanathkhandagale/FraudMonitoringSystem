using FraudMonitoringSystem.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FraudMonitoringSystem.Services.Customer.Interfaces.AlertsCase;

namespace FraudMonitoringSystem.Services.Customer.Implementations.AlertsCase
{
	public class AlertBackgroundService : BackgroundService
	{
		private readonly IServiceProvider _serviceProvider;

		public AlertBackgroundService(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				using (var scope = _serviceProvider.CreateScope())
				{
					var context = scope.ServiceProvider.GetRequiredService<WebContext>();
					var alertService = scope.ServiceProvider.GetRequiredService<IAlertService>();

					// 🔥 Get RiskScores without alerts
					var scores = context.RiskScores
						.Where(r => !context.Alerts.Any(a => a.TransactionID == r.TransactionID))
						.ToList();

					if (scores.Any())
					{
						await alertService.GenerateAlertsFromRiskScores(scores);
					}
				}

				await Task.Delay(5000, stoppingToken); // check every 5 sec
			}
		}
	}
}