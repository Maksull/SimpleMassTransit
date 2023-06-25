using Core.Entities;
using Core.Mediator.Queries.Products;
using Infrastructure.Services.Interfaces;
using MediatR;

namespace Infrastructure.Mediator.Handlers.Products
{
    public sealed class GetQueryableProductsHandler : IRequestHandler<GetQueryableProductsQuery, IQueryable<Product>>
    {
        private readonly IProductService _productService;

        public GetQueryableProductsHandler(IProductService productService)
        {
            _productService = productService;
        }

        public Task<IQueryable<Product>> Handle(GetQueryableProductsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_productService.GetQueryableProducts());
        }
    }
}
