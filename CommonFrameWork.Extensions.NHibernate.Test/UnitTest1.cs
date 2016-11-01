using System;
using CommonFrameWork.Application;
using CommonFrameWork.CommUtils;
using CommonFrameWork.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonFrameWork.Extensions.NHibernate.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            IConfigSource configSource = new DefaultConfig();
            configSource.Config.Application.AppProvider = "CommonFrameWork.Application.DefaultApp,CommonFrameWork";
            configSource.Config.Application.ObjectContainer = "CommonFrameWork.Extensions.Autofac.AutofacObjectContainer,CommonFrameWork.Extensions.Autofac";

            //configSource.Config.Application.SerializationProvider = "CommonFrameWork.Extensions.NewTonSoft.NewTonSoftSerializer,CommonFrameWork.Extensions.NewTonSoft";

            configSource.Config.Application.Assemblies = Utils.GetAllAssemblies("Project.Domain.ModuleManager");


            configSource.Config.Application.LogProvider = "CommonFrameWork.Extensions.Log4Net.Log4NetLoggerFactory,CommonFrameWork.Extensions.Log4Net";


            var application = AppRuntime.Create(configSource);
            application.Starting += Add2;
            //application.Started += Add2;
            //application.Stopping += Add3;

            application.Start();
        }


        public void Add2(object sender, ApplicationStartingEventArgs e)
        {

        }

    }
}
