using Core.Entities;
using Core.Mediator.Commands.Products;
using Infrastructure.Services.Interfaces;
using MassTransit;
using MediatR;

namespace Infrastructure.Mediator.Handlers.Products
{
    public sealed class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Product?>
    {
        private readonly IProductService _productService;
        private readonly IBus _bus;

        public DeleteProductHandler(IProductService productService, IBus bus)
        {
            _productService = productService;
            _bus = bus;
        }

        public async Task<Product?> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productService.DeleteProduct(request.Id);

            if (product != null)
            {
                var productDeleted = ProductMapper.ProductToProductDeleted(product);

                await _bus.Publish(productDeleted);
            }

            return product;
        }
    }
}
