using Core.Contracts.Consumers.Categories;
using MassTransit;

namespace SimpleMassTransitConsumers.Consumers.Categories
{
    public sealed class CreateCategoryConsumer : IConsumer<CategoryCreated>
    {
        private readonly ILogger<CreateCategoryConsumer> _logger;

        public CreateCategoryConsumer(ILogger<CreateCategoryConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<CategoryCreated> context)
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 3);

            //if(randomNumber == 1)
            //{
                ////fails
                //_logger.LogInformation("LOGGIN ERROR DELETE CATEGORY REQUIRED: {Category}", context.Message.CategoryId);

                //throw new Exception("LOGGIN ERROR DELETE CATEGORY REQUIRED");
            //}

            _logger.LogInformation("CategoryCreated: {Category}", context.Message.CategoryId);

            return Task.CompletedTask;
        }
    }
}
