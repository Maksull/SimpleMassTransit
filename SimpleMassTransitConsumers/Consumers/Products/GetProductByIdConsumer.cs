using Core.Entities;
using MassTransit;

namespace SimpleMassTransitConsumers.Consumers.Products
{
    public sealed class GetProductByIdConsumer : IConsumer<Product>
    {
        private readonly ILogger<GetProductByIdConsumer> _logger;

        public GetProductByIdConsumer(ILogger<GetProductByIdConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Product> context)
        {
            _logger.LogInformation("GetProductById: {@Product}", context.Message.Name);

            return Task.CompletedTask;
        }
    }
}
