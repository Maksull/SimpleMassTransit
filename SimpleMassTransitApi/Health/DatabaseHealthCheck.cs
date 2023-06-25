using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SimpleMassTransitApi.Health
{
    public sealed class DatabaseHealthCheck : IHealthCheck
    {
		private readonly IProductService _productService;
		private readonly ILogger<DatabaseHealthCheck> _logger;

		public DatabaseHealthCheck(IProductService productService, ILogger<DatabaseHealthCheck> logger)
		{
			_productService = productService;
			_logger = logger;
		}

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
			try
			{
				var products = _productService.GetProducts();

				return Task.FromResult(HealthCheckResult.Healthy());
			}
			catch (Exception ex)
			{
				_logger.LogError($"DB does not work. {ex.InnerException}");

				return Task.FromResult(HealthCheckResult.Unhealthy(exception: ex));
			}
        }
    }
}
