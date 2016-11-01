using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonFrameWork.Application;
using CommonFrameWork.CommUtils;
using CommonFrameWork.Config;
using CommonFrameWork.Extensions.MassTransit;

namespace CommonFrameWorkUnitTestProject
{
    public class TestInit
    {
        public TestInit()
        {
            IConfigSource configSource = new DefaultConfig();
            configSource.Config.Application.AppProvider = "CommonFrameWork.Application.DefaultApp,CommonFrameWork";
            configSource.Config.Application.ObjectContainer = "CommonFrameWork.Extensions.Autofac.AutofacObjectContainer,CommonFrameWork.Extensions.Autofac";
            configSource.Config.Application.Assemblies = Utils.GetAllAssemblies("Project.Domain.ModuleManager");


            var application = AppRuntime.Create(configSource).UseMassTransit();

            //application.Starting += Add;
            //application.Started += Add2;
            //application.Stopping += Add3;

            application.Start();
        }


        public TestInit(string ddd)
        {
            if (ddd == "")
            {
            }

            IConfigSource configSource = new DefaultConfig();
            configSource.Config.Application.AppProvider = "CommonFrameWork.Application.DefaultApp,CommonFrameWork";
            configSource.Config.Application.ObjectContainer = "CommonFrameWork.Extensions.Autofac.AutofacObjectContainer,CommonFrameWork.Extensions.Autofac";

            //configSource.Config.Application.SerializationProvider = "CommonFrameWork.Extensions.NewTonSoft.NewTonSoftSerializer,CommonFrameWork.Extensions.NewTonSoft";

            configSource.Config.Application.Assemblies = Utils.GetAllAssemblies("Project.Domain.ModuleManager");


            configSource.Config.Application.LogProvider = "CommonFrameWork.Extensions.Log4Net.Log4NetLoggerFactory,CommonFrameWork.Extensions.Log4Net";


            var application = AppRuntime.Create(configSource).UseMassTransit();

            //application.Starting += Add;
            //application.Started += Add2;
            //application.Stopping += Add3;

            application.Start();
        }
    }
}
