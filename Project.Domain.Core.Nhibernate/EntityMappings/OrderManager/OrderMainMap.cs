using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonFrameWork.Extensions.NHibernate.EntityMappings;
using FluentNHibernate.Mapping;
using Project.Domain.Core.OrderManager.PersistentObject;

namespace Project.Domain.Core.Nhibernate.EntityMappings.OrderManager
{
    public  class OrderMainMap : ClassMap<OrderMain>
    {
        protected OrderMainMap()
        {

            Table("TEST_ORDERMAIN");
            Id(p => p.ID);
            Map(p => p.OrderNo);
          

        }

    }
}
