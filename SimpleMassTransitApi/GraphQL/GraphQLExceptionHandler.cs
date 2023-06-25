namespace SimpleMassTransitApi.GraphQL
{
    public sealed class GraphQLExceptionHandler : IErrorFilter
    {
        private readonly ILogger<GraphQLExceptionHandler> _logger;

        public GraphQLExceptionHandler(ILogger<GraphQLExceptionHandler> logger)
        {
            _logger = logger;
        }

        public IError OnError(IError error)
        {
            _logger.LogError(error.Exception?.Message);

            return ErrorBuilder.New()
                .SetMessage("Internal server error.")
                .SetCode(error.Exception?.Message)
                .Build();
        }
    }
}
