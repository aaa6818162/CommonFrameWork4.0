using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonFrameWork.Dependency
{
    public static class ObjectContainer
    {
        #region -  Field(s)  -

        private static readonly object _locker = new object();
        private static volatile IObjectContainer _current;

        #endregion

        #region -  Property(ies)  -

        public static object InnerContainer
        { get { return _current.InnerContainer; } }

        #endregion

        #region -  SetContainer  -

        public static void SetContainer(IObjectContainer container)
        {
            if (null == _current)
            {
                lock (_locker)
                {
                    if (null == _current)
                    {
                        _current = container;
                    }
                }
            }
        }

        #endregion

        #region -  Register  -

        public static void Register(Type implType)
        {
            _current.Register(implType);
        }

        public static void Register(Type serviceType, Type implType)
        {
            _current.Register( serviceType,implType);
        }


        public static void Register(Type serviceType, Type implType, List<ObjectContainerParameter> parameter)
        {
            _current.Register(serviceType, implType, parameter);
        }


        public static void Register(IEnumerable<Type> implTypes)
        {
            foreach (var implType in implTypes)
            {
                _current.Register(implType);
            }
        }

        public static void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _current.Register<TService, TImplementation>();
        }


        public static void Register<TService, TImplementation>(List<ObjectContainerParameter> parameter)
       where TService : class
       where TImplementation : class, TService
        {
            _current.Register<TService, TImplementation>(parameter);
        }


        public static void RegisterByAssembly(Assembly assembly)
        {
            if (assembly != null)
            {
                _current.Register(assembly);
  //              var registrations =
  //from type in assembly.GetExportedTypes()
  //where type.GetInterfaces().Any(p => p.Name.EndsWith("Service"))
  //select new { Service = type.GetInterfaces().Single(), Implementation = type };
  //              foreach (var reg in registrations)
  //              {
  //                  _current.Register(reg.Service, reg.Implementation);
  //              }
            }
        }
        #endregion

        #region -  Resolve  -

        public static TService Resolve<TService>() where TService : class
        {
            return _current.Resolve<TService>();
        }

        public static object Resolve(Type serviceType)
        {
            return _current.Resolve(serviceType);
        }



        public static TService Resolve<TService>(List<ObjectContainerParameter> parameters) where TService : class
        {
            return _current.Resolve<TService>(parameters);
        }

        public static object Resolve(Type serviceType, List<ObjectContainerParameter> parameters)
        {
            return _current.Resolve(serviceType, parameters);
        }

        public static TService Resolve<TService>(ObjectContainerParameter parameter) where TService : class
        {
            return _current.Resolve<TService>(new List<ObjectContainerParameter>() { parameter });
        }

        public static object Resolve(Type serviceType, ObjectContainerParameter parameter)
        {
            return _current.Resolve(serviceType, new List<ObjectContainerParameter>() { parameter });
        }

        #endregion


    }
}
