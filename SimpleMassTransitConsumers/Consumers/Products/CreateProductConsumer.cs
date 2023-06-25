using Core.Contracts.Consumers.Products;
using MassTransit;

namespace SimpleMassTransitConsumers.Consumers.Products
{
    public sealed class CreateProductConsumer : IConsumer<ProductCreated>
    {
        private readonly ILogger<GetProductByIdConsumer> _logger;

        public CreateProductConsumer(ILogger<GetProductByIdConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<ProductCreated> context)
        {
            _logger.LogInformation("ProductCreated: {@Product}", context.Message.Name);

            return Task.CompletedTask;
        }
    }
}
