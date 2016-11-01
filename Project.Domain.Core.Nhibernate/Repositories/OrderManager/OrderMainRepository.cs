using System;
using CommonFrameWork.Domain.Entities;
using CommonFrameWork.Domain.Repositories;
using CommonFrameWork.Extensions.NHibernate;
using Project.Domain.Core.OrderManager.PersistentObject;
using Project.Domain.Core.OrderManager.Repositories;

namespace Project.Domain.Core.Nhibernate.Repositories.OrderManager
{
    public class OrderMainRepository: NHibernateRepository<String,OrderMain>, IOrderMainRepository
    {
        public OrderMainRepository(IRepositoryContext context)
            : base(context)
        {
            
        }
    }
}
