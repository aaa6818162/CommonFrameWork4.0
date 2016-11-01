using CommonFrameWork.Domain.Uow;
using NHibernate;

namespace CommonFrameWork.Extensions.NHibernate.Uow
{
    public class UnitOfWorkSessionProvider : ISessionProvider
    {
        public ISession Session
        {
            get { return _unitOfWorkProvider.Current.GetSession(); }
        }
        
        private readonly ICurrentUnitOfWorkProvider _unitOfWorkProvider;

        public UnitOfWorkSessionProvider(ICurrentUnitOfWorkProvider unitOfWorkProvider)
        {
            _unitOfWorkProvider = unitOfWorkProvider;
        }
    }
}