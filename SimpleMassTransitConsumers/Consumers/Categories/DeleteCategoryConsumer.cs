using Core.Contracts.Consumers.Categories;
using MassTransit;

namespace SimpleMassTransitConsumers.Consumers.Categories
{
    public sealed class DeleteCategoryConsumer : IConsumer<CategoryDeleted>
    {
        private readonly ILogger<DeleteCategoryConsumer> _logger;

        public DeleteCategoryConsumer(ILogger<DeleteCategoryConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<CategoryDeleted> context)
        {
            _logger.LogInformation("CategoryDeleted: {@Category}", context.Message.Name);

            return Task.CompletedTask;
        }
    }
}
