﻿using Core.Contracts.Controllers.Products;
using Core.Entities;
using MediatR;

namespace Core.Mediator.Commands.Products
{
    public sealed record CreateProductCommand(CreateProductRequest Product) : IRequest<Product>;
}
