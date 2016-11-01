using System;
using CommonFrameWork.Application;
using CommonFrameWork.CommUtils;
using CommonFrameWork.Config;
using CommonFrameWork.Dependency;
using CommonFrameWork.Extensions.Autofac;
using CommonFrameWork.Extensions.MassTransit;
using CommonFrameWork.Logging;
using CommonFrameWork.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Domain.ModuleManager;

namespace CommonFrameWorkUnitTestProject
{
    [TestClass]
    public class TestDefaultApp
    {
        public TestDefaultApp()
        {
            //  new TestInit();

            new TestInit("xxxxxxxxx");
        }

        [TestMethod]
        public void JsonTest()
        {
            // new TestInit(); 原始
            //  ObjectContainer.Resolve<ILoggerFactory>().Create("").Debug("1111");

            var str = ObjectContainer.Resolve<IObjectSerializer>().Serialize(new A()
                 {
                     File1 = "11111"
                 });

            var o = ObjectContainer.Resolve<IObjectSerializer>().Deserialize<A>(str);

        }


        [TestMethod]
        public void LogTest()
        {
            ObjectContainer.Resolve<ILoggerFactory>().Create("RollingLogFileAppender").Debug("1111");
            // ObjectContainer


        }


    }


    public class A
    {
        public string File1 { get; set; }
    }
}
