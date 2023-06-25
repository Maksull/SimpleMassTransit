using Core.Contracts.Consumers.Categories;
using MassTransit;

namespace SimpleMassTransitConsumers.Consumers.Categories
{
    public sealed class UpdateCategoryConsumer : IConsumer<CategoryUpdated>
    {
        private readonly ILogger<UpdateCategoryConsumer> _logger;

        public UpdateCategoryConsumer(ILogger<UpdateCategoryConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<CategoryUpdated> context)
        {
            _logger.LogInformation("CategoryUpdated: {@Category}", context.Message.Name);

            return Task.CompletedTask;
        }
    }
}
