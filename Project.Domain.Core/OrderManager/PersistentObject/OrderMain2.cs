using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonFrameWork.Domain.Entities;

namespace Project.Domain.Core.OrderManager.PersistentObject
{
   public class OrderMain2: IAggregateRoot<String>
    {
       #region Implementation of IEntity<Guid>

       /// <summary>
       /// Gets or sets the identifier of the entity.
       /// </summary>
       /// <value>
       /// The identifier of the entity.
       /// </value>
       public virtual string ID { get; set; }

       public virtual string OrderNo { get; set; }

       #endregion
    }
}
