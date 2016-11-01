using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonFrameWork.Dependency;
using CommonFrameWork.Domain.Repositories;
using Project.Domain.Core.OrderManager.PersistentObject;
using Project.Domain.Core.OrderManager.Repositories;

namespace Project.Domain.Core.OrderManager.Services
{
    public class OrderDomainService : IOrderDomainService
    {
        #region Private Fields
        private readonly IRepositoryContext _repositoryContext;
        private readonly IOrderMainRepository _orderMainRepository;
        private readonly IOrderMainRepository2 _orderMainRepository2;
        #endregion

        #region Ctor
        /// <summary>
        /// 初始化一个新的<c>DomainService</c>类型的实例。
        /// </summary>
        /// <param name="context">仓储上下文。</param>
        public OrderDomainService(IRepositoryContext context)
        {
            var parameter = new ObjectContainerParameter()
            {
                Name = "context",
                Value = context
            };

            _repositoryContext = context;
            _orderMainRepository = ObjectContainer.Resolve<IOrderMainRepository>(parameter);
            _orderMainRepository2 = ObjectContainer.Resolve<IOrderMainRepository2>(parameter);
        }
        #endregion

        public void Add()
        {
            _orderMainRepository.Add(new OrderMain() { ID = "6", OrderNo = "2222" });
            _orderMainRepository2.Add(new OrderMain2() { ID = "6", OrderNo = "2222" });

            _repositoryContext.Commit();
            _repositoryContext.Dispose();

            _orderMainRepository.Add(new OrderMain() { ID = "5", OrderNo = "2222" });
            _orderMainRepository2.Add(new OrderMain2() { ID = "5", OrderNo = "2222" });
            _repositoryContext.Commit();
        }




    }
}
