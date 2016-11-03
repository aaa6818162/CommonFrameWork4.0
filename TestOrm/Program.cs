using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CommonFrameWork.Application;
using CommonFrameWork.CommUtils;
using CommonFrameWork.Config;
using CommonFrameWork.Dependency;
using CommonFrameWork.Domain.Services;
using CommonFrameWork.Extensions.NHibernate;
using CommonFrameWork.Serialization;
using Project.Domain.Core.OrderManager.PersistentObject;
using Project.Domain.Core.OrderManager.Repositories;
using Project.Domain.Core.OrderManager.Services;

namespace TestOrm
{
    class Program
    {
        static void Main(string[] args)
        {
            //  new SessionFactoryProvider().GetSessionFactory();


            IConfigSource configSource = new DefaultConfig();
            configSource.Config.Application.AppProvider = "CommonFrameWork.Application.DefaultApp,CommonFrameWork";
            configSource.Config.Application.ObjectContainer = "CommonFrameWork.Extensions.Autofac.AutofacObjectContainer,CommonFrameWork.Extensions.Autofac";

            //configSource.Config.Application.SerializationProvider = "CommonFrameWork.Extensions.NewTonSoft.NewTonSoftSerializer,CommonFrameWork.Extensions.NewTonSoft";

            var list = new List<Assembly>();
            list.AddRange(Utils.GetAllAssemblies("Project.Domain.Core.Nhibernate"));
            list.AddRange(Utils.GetAllAssemblies("Project.Domain.Core"));
            list.AddRange(Utils.GetAllAssemblies("CommonFrameWork.Extensions.NHibernate"));
            configSource.Config.Application.Assemblies = list;

            configSource.Config.Application.LogProvider = "CommonFrameWork.Extensions.Log4Net.Log4NetLoggerFactory,CommonFrameWork.Extensions.Log4Net";


            var application = AppRuntime.Create(configSource).ConfigMessageDispatcher();

            //application.Starting += Add;
            //application.Started += Add2;
            //application.Stopping += Add3;

            application.Start();

            //var res3 = ObjectContainer.Resolve<IOrderMainRepository2>();
            ////var t3 = res3.Context;
            ////var entity3 = res3.GetByKey("1");

            //res3.Add(new OrderMain2() {ID = "3",OrderNo = "2222"});

            //res3.Context.Commit();

            //var res = ObjectContainer.Resolve<IOrderMainRepository>();
            //var t = res3.Context;
            //var entity = res.GetByKey("1");


            var res3 = ObjectContainer.Resolve<IOrderDomainService>();
            res3.Add();


            Console.Read();
        }
    }
}
