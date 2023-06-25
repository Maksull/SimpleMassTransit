using Core.Entities;
using Core.Mediator.Commands.Products;
using Infrastructure.Services.Interfaces;
using MassTransit;
using MediatR;

namespace Infrastructure.Mediator.Handlers.Products
{
    public sealed class CreateProductHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IProductService _productService;
        private readonly IBus _bus;

        public CreateProductHandler(IProductService productService, IBus bus)
        {
            _productService = productService;
            _bus = bus;
        }

        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productService.CreateProduct(request.Product);

            var productCreated = ProductMapper.ProductToProductCreated(product);

            await _bus.Publish(productCreated);

            return product;
        }
    }
}
