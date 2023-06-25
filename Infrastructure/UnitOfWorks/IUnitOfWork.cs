﻿using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
    }
}
