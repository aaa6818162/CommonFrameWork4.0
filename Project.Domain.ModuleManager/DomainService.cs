using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonFrameWork.Exception;

namespace Project.Domain.ModuleManager
{
    internal class DomainService : IDomainService
    {
        public void Test()
        {
            throw new DomainException("测试autoinit");
        }
    }
}
