using Core.Entities;
using Core.Mediator.Queries.Products;
using Infrastructure.Services.Interfaces;
using MassTransit;
using MediatR;

namespace Infrastructure.Mediator.Handlers.Products
{
    public sealed class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product?>
    {
        private readonly IProductService _productService;
        private readonly IBus _bus;

        public GetProductByIdHandler(IProductService productService, IBus bus)
        {
            _productService = productService;
            _bus = bus;
        }

        public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productService.GetProduct(request.Id);

            if (product != null)
            {
                await _bus.Publish(product);
            }

            return product;
        }
    }
}
