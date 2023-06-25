using Core.Contracts.Consumers.Products;
using MassTransit;

namespace SimpleMassTransitConsumers.Consumers.Products
{
    public sealed class UpdateProductConsumer : IConsumer<ProductUpdated>
    {
        private readonly ILogger<GetProductByIdConsumer> _logger;

        public UpdateProductConsumer(ILogger<GetProductByIdConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<ProductUpdated> context)
        {
            _logger.LogInformation("ProductUpdated: {@Product}", context.Message.Name);

            return Task.CompletedTask;
        }
    }
}
