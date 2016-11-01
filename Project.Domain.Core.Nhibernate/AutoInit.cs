﻿using System;
using System.Collections.Generic;
using System.Linq;
using CommonFrameWork.Application;
using CommonFrameWork.Dependency;

namespace Project.Domain.Core.Nhibernate
{
    internal class AutoInit : IAutoInitializer
    {
        public void Init(IEnumerable<System.Reflection.Assembly> assemblies)
        {
            Type type = typeof(AutoInit);
            var nowAssemblie = assemblies.FirstOrDefault(P => P.FullName.StartsWith(type.Assembly.FullName));
            ObjectContainer.RegisterByAssembly(nowAssemblie);

        }
    }
}
