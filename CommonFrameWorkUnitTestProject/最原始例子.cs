using System;
using CommonFrameWork.Application;
using CommonFrameWork.CommUtils;
using CommonFrameWork.Config;
using CommonFrameWork.Dependency;
using CommonFrameWork.Extensions.Autofac;
using CommonFrameWork.Extensions.MassTransit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Domain.ModuleManager;

namespace CommonFrameWorkUnitTestProject
{
    [TestClass]
    public class DefaultApp2
    {
        [TestMethod]
        public void TestMethod1()
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

          //  ObjectContainer.Resolve<IDomainService>().Test();




            //ObjectContainer.Register<IAClass, AClass>();
            //ObjectContainer.Resolve<IAClass>().Add();

        }


        //public void Add2(object sender, EngineStartedEventArgs e)
        //{

        //}

        //public void Add(object sender, EngineStartingEventArgs e)
        //{

        //}


        //public void Add3(object sender, EventArgs e)
        //{

        //}



    }
}
