using Core.Contracts.Consumers.Products;
using MassTransit;

namespace SimpleMassTransitConsumers.Consumers.Products
{
    public sealed class DeleteProductConsumer : IConsumer<ProductDeleted>
    {
        private readonly ILogger<GetProductByIdConsumer> _logger;

        public DeleteProductConsumer(ILogger<GetProductByIdConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<ProductDeleted> context)
        {
            _logger.LogInformation("ProductDeleted: {@Product}", context.Message.Name);

            return Task.CompletedTask;
        }
    }
}
