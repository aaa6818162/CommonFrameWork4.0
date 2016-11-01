using System;
using CommonFrameWork.Domain.Entities;
using CommonFrameWork.Domain.Repositories;
using Project.Domain.Core.OrderManager.PersistentObject;

namespace Project.Domain.Core.OrderManager.Repositories
{

    public interface IOrderMainRepository : IRepository<String,OrderMain>
    {

    }
}
