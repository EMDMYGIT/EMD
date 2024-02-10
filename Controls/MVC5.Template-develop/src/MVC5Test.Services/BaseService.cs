﻿using MVC5Test.Data.Core;
using System;

namespace MVC5Test.Services
{
    public abstract class BaseService : IService
    {
        protected IUnitOfWork UnitOfWork { get; }
        public Int32 CurrentAccountId { get; set; }

        protected BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
