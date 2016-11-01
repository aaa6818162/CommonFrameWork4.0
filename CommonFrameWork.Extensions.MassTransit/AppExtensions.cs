using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonFrameWork.Application;
using CommonFrameWork.Dependency;

namespace CommonFrameWork.Extensions.MassTransit
{
    public static class AppExtensions
    {
        public static IApp UseMassTransit(this IApp step)
        {
            step.Starting += (o, e) =>
            {

               // ObjectContainer.Register<IDistributedMessageBus, MassTransitBus>(LifeCycleEnum.Singleton);
            };
            return step;
        }
    }
}
