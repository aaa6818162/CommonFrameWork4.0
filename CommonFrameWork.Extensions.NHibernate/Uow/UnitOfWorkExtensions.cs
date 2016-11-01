using System;
using CommonFrameWork.Domain.Uow;
using CommonFrameWork.Exception;
using NHibernate;

namespace CommonFrameWork.Extensions.NHibernate.Uow
{
    internal static class UnitOfWorkExtensions
    {
        public static ISession GetSession(this IActiveUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }

            if (!(unitOfWork is NhUnitOfWork))
            {
                throw new UowException("unitOfWork is not type of " + typeof(NhUnitOfWork).FullName, "unitOfWork");
            }

            return (unitOfWork as NhUnitOfWork).Session;
        }
    }
}