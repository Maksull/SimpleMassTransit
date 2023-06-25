using Core.Entities;
using MassTransit;

namespace SimpleMassTransitConsumers.Consumers.Categories
{
    public sealed class GetCategoryByIdConsumer : IConsumer<Category>
    {
        private readonly ILogger<GetCategoryByIdConsumer> _logger;

        public GetCategoryByIdConsumer(ILogger<GetCategoryByIdConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Category> context)
        {
            _logger.LogInformation("GetCategoryById: {Category}", context.Message.Name);

            return Task.CompletedTask;
        }
    }
}
