using Core.Entities;
using Core.Mediator.Commands.Products;
using Infrastructure.Services.Interfaces;
using MassTransit;
using MediatR;

namespace Infrastructure.Mediator.Handlers.Products
{
    public sealed class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Product?>
    {
        private readonly IProductService _productService;
        private readonly IBus _bus;

        public UpdateProductHandler(IProductService productService, IBus bus)
        {
            _productService = productService;
            _bus = bus;
        }

        public async Task<Product?> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productService.UpdateProduct(request.Product);

            if (product != null)
            {
                var productUpdated = ProductMapper.ProductToProductUpdated(product);

                await _bus.Publish(productUpdated);
            }

            return product;
        }
    }
}
