using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core.Registration;
using CommonFrameWork.Dependency;

namespace CommonFrameWork.Extensions.Autofac
{
    internal class AutofacObjectContainer : IObjectContainer
    {
        private IContainer _IContainer;

        private IContainer IContainer
        {
            get
            {
                if (_IContainer == null)
                {
                    _IContainer = Holder.ContainerBuilder.Build();
                }

                return _IContainer;
            }

        }

        #region -  Property(ies)  -

        public object InnerContainer
        {
            get { return Holder.ContainerBuilder; }
        }



        #endregion

        #region -  Register  -

        public void Register(Type serviceType, Type implType)
        {
            Holder.ContainerBuilder.RegisterType(implType).As(serviceType);
        }


        public void Register(Type serviceType, Type implType, List<ObjectContainerParameter> parameters)
        {
            var autofacparametersList = new List<NamedParameter>();
            parameters.ForEach(p =>
            {
                autofacparametersList.Add(new NamedParameter(p.Name, p.Value));
            });

            Holder.ContainerBuilder.RegisterType(implType).As(serviceType).WithParameters(autofacparametersList);
        }


        public void Register(Assembly assembly)
        {
            Holder.ContainerBuilder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();
        }


        public void Register(Assembly assembly, string prefix, string suffix)
        {
            if (!string.IsNullOrWhiteSpace(prefix))
            {
              var list=  Holder.ContainerBuilder.RegisterAssemblyTypes(assembly)
                    .Where(p => p.Name.StartsWith(prefix));

                Holder.ContainerBuilder.RegisterAssemblyTypes(assembly)
                    .Where(p => p.Name.StartsWith(prefix))
                    .AsImplementedInterfaces();
            }

            if (!string.IsNullOrWhiteSpace(suffix))
            {

                var list = Holder.ContainerBuilder.RegisterAssemblyTypes(assembly).Where(p => p.Name.EndsWith(suffix));

                Holder.ContainerBuilder.RegisterAssemblyTypes(assembly).Where(p => p.Name.EndsWith(suffix)).AsImplementedInterfaces();
            }

        }


        public void RegisterGeneric(Type serviceType, Type implType)
        {
            Holder.ContainerBuilder.RegisterGeneric(implType).As(serviceType);

        }

        public void Register(Type serviceType)
        {
            Holder.ContainerBuilder.RegisterType(serviceType);
        }

        public void Register<TService, TImplementation>(List<ObjectContainerParameter> parameters)
            where TService : class
            where TImplementation : class, TService
        {
            var autofacparametersList = new List<NamedParameter>();
            parameters.ForEach(p =>
            {
                autofacparametersList.Add(new NamedParameter(p.Name, p.Value));
            });

            Holder.ContainerBuilder.RegisterType<TImplementation>().As<TService>().WithParameters(autofacparametersList);
        }


        public void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            Holder.ContainerBuilder.RegisterType<TImplementation>().As<TService>();
        }
        #endregion

        #region -  Resolve  -

        public TService Resolve<TService>() where TService : class
        {
#if DEBUG

            var registerlist = IContainer
                    .ComponentRegistry.Registrations.ToList()
                    .Select(p => p.Activator.LimitType.AssemblyQualifiedName)
                    .ToList();
#endif

            return IContainer.Resolve<TService>();
        }

        public object Resolve(Type serviceType)
        {
            return IContainer.Resolve(serviceType);
        }


        public TService Resolve<TService>(List<ObjectContainerParameter> parameters)
           where TService : class
        {
            var autofacparametersList = new List<NamedParameter>();
            parameters.ForEach(p =>
            {
                autofacparametersList.Add(new NamedParameter(p.Name, p.Value));
            });

            return IContainer.Resolve<TService>(autofacparametersList);
        }


        public object Resolve(Type serviceType, List<ObjectContainerParameter> parameters)
        {
            var autofacparametersList = new List<NamedParameter>();
            parameters.ForEach(p =>
            {
                autofacparametersList.Add(new NamedParameter(p.Name, p.Value));
            });
            return IContainer.Resolve(serviceType, autofacparametersList);
        }

        #endregion



    }
}