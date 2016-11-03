using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonFrameWork.Bus.Local;
using Project.Domain.Core.Events;

namespace Project.Domain.Core.OrderManager.Events.Handlers
{
    public class GetCustomerEventHandler : ILocalHandler<GetCustomerEvent>
    {
        public Task<string> HandleAsync(GetCustomerEvent msg)
        {
            throw new NotImplementedException();
        }
    }
}
