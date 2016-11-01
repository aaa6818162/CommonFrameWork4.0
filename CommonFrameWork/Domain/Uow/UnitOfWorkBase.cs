using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonFrameWork.Domain.Uow
{
    /// <summary>
    /// Base for all Unit Of Work classes.
    /// </summary>
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        public string Id { get; private set; }

        public IUnitOfWork Outer { get; set; }

        /// <inheritdoc/>
        public event EventHandler Completed;

        /// <inheritdoc/>
        public event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        /// <inheritdoc/>
        public event EventHandler Disposed;

        /// <inheritdoc/>
        public UnitOfWorkOptions Options { get; private set; }

        /// <inheritdoc/>
        //public IReadOnlyList<DataFilterConfiguration> Filters
        //{
        //    get { return _filters.ToImmutableList(); }
        //}
        //private readonly List<DataFilterConfiguration> _filters;

        /// <summary>
        /// Gets default UOW options.
        /// </summary>
      //  protected IUnitOfWorkDefaultOptions DefaultOptions { get; private set; }

        /// <summary>
        /// Gets a value indicates that this unit of work is disposed or not.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Reference to current ABP session.
        /// </summary>
      //  public IAbpSession AbpSession { private get; set; }

        /// <summary>
        /// Is <see cref="Begin"/> method called before?
        /// </summary>
        private bool _isBeginCalledBefore;

        /// <summary>
        /// Is <see cref="Complete"/> method called before?
        /// </summary>
        private bool _isCompleteCalledBefore;

        /// <summary>
        /// Is this unit of work successfully completed.
        /// </summary>
        private bool _succeed;

        /// <summary>
        /// A reference to the exception if this unit of work failed.
        /// </summary>
        private System.Exception _exception;

        /// <summary>
        /// Constructor.
        /// </summary>
        //protected UnitOfWorkBase(IUnitOfWorkDefaultOptions defaultOptions)
        //{
        //    DefaultOptions = defaultOptions;

        //    Id = Guid.NewGuid().ToString("N");
        //    _filters = defaultOptions.Filters.ToList();
        //    AbpSession = NullAbpSession.Instance;
        //}

        /// <summary>
        /// ��ʼUnitOfWork��һЩ���ã�������ĳ�ʼ��
        /// </summary>
        /// <param name="options"></param>
        public void Begin(UnitOfWorkOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            PreventMultipleBegin();
            Options = options; //TODO: Do not set options like that, instead make a copy?

           // SetFilters(options.FilterOverrides);

            BeginUow();
        }

        /// <inheritdoc/>
        public abstract void SaveChanges();

        /// <inheritdoc/>
        public abstract Task SaveChangesAsync();

        /// <summary>
        /// Disables one or more data filters.
        /// Does nothing for a filter if it's already disabled. 
        /// Use this method in a using statement to re-enable filters if needed.
        /// </summary>
        /// <param name="filterNames">One or more filter names. <see cref="AbpDataFilters"/> for standard filters.</param>
        /// <returns>A <see cref="IDisposable"/> handle to take back the disable effect.</returns>
        public IDisposable DisableFilter(params string[] filterNames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Enables one or more data filters.
        /// Does nothing for a filter if it's already enabled.
        /// Use this method in a using statement to re-disable filters if needed.
        /// </summary>
        /// <param name="filterNames">One or more filter names. <see cref="AbpDataFilters"/> for standard filters.</param>
        /// <returns>A <see cref="IDisposable"/> handle to take back the enable effect.</returns>
        public IDisposable EnableFilter(params string[] filterNames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if a filter is enabled or not.
        /// </summary>
        /// <param name="filterName">Name of the filter. <see cref="AbpDataFilters"/> for standard filters.</param>
        public bool IsFilterEnabled(string filterName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets (overrides) value of a filter parameter.
        /// </summary>
        /// <param name="filterName">Name of the filter</param>
        /// <param name="parameterName">Parameter's name</param>
        /// <param name="value">Value of the parameter to be set</param>
        public IDisposable SetFilterParameter(string filterName, string parameterName, object value)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        //public IDisposable DisableFilter(params string[] filterNames)
        //{
        //    //TODO: Check if filters exists?

        //    var disabledFilters = new List<string>();

        //    foreach (var filterName in filterNames)
        //    {
        //        var filterIndex = GetFilterIndex(filterName);
        //        if (_filters[filterIndex].IsEnabled)
        //        {
        //            disabledFilters.Add(filterName);
        //            _filters[filterIndex] = new DataFilterConfiguration(filterName, false);
        //        }
        //    }

        //    disabledFilters.ForEach(ApplyDisableFilter);

        //    return new DisposeAction(() => EnableFilter(disabledFilters.ToArray()));
        //}

        /// <inheritdoc/>
        //public IDisposable EnableFilter(params string[] filterNames)
        //{
        //    //TODO: Check if filters exists?

        //    var enabledFilters = new List<string>();

        //    foreach (var filterName in filterNames)
        //    {
        //        var filterIndex = GetFilterIndex(filterName);
        //        if (!_filters[filterIndex].IsEnabled)
        //        {
        //            enabledFilters.Add(filterName);
        //            _filters[filterIndex] = new DataFilterConfiguration(filterName, true);
        //        }
        //    }

        //    enabledFilters.ForEach(ApplyEnableFilter);

        //    return new DisposeAction(() => DisableFilter(enabledFilters.ToArray()));
        //}

        /// <inheritdoc/>
        //public bool IsFilterEnabled(string filterName)
        //{
        //    return GetFilter(filterName).IsEnabled;
        //}

        /// <inheritdoc/>
        //public IDisposable SetFilterParameter(string filterName, string parameterName, object value)
        //{
        //    var filterIndex = GetFilterIndex(filterName);

        //    var newfilter = new DataFilterConfiguration(_filters[filterIndex]);

        //    //Store old value
        //    object oldValue = null;
        //    var hasOldValue = newfilter.FilterParameters.ContainsKey(filterName);
        //    if (hasOldValue)
        //    {
        //        oldValue = newfilter.FilterParameters[filterName];
        //    }

        //    newfilter.FilterParameters[parameterName] = value;

        //    _filters[filterIndex] = newfilter;

        //    ApplyFilterParameterValue(filterName, parameterName, value);

        //    return new DisposeAction(() =>
        //    {
        //        //Restore old value
        //        if (hasOldValue)
        //        {
        //            SetFilterParameter(filterName, parameterName, oldValue);
        //        }
        //    });
        //}

        /// <summary>
        /// ������ύ���쳣�Ĳ���
        /// </summary>
        public void Complete()
        {
            PreventMultipleComplete();
            try
            {
                CompleteUow();
                _succeed = true;
                OnCompleted();
            }
            catch (System.Exception ex)
            {
                _exception = ex;
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task CompleteAsync()
        {
            PreventMultipleComplete();
            try
            {
                await CompleteUowAsync();
                _succeed = true;
                OnCompleted();
            }
            catch (System.Exception ex)
            {
                _exception = ex;
                throw;
            }
        }

        /// <summary>
        /// ��������ʧ�ܾͻع�
        /// </summary>
        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            if (!_succeed)
            {
                OnFailed(_exception);
            }

            DisposeUow();
            OnDisposed();
        }

        /// <summary>
        /// Should be implemented by derived classes to start UOW.
        /// </summary>
        protected abstract void BeginUow();

        /// <summary>
        /// Should be implemented by derived classes to complete UOW.
        /// </summary>
        protected abstract void CompleteUow();

        /// <summary>
        /// Should be implemented by derived classes to complete UOW.
        /// </summary>
        protected abstract Task CompleteUowAsync();

        /// <summary>
        /// Should be implemented by derived classes to dispose UOW.
        /// </summary>
        protected abstract void DisposeUow();

        /// <summary>
        /// Concrete Unit of work classes should implement this
        /// method in order to disable a filter.
        /// Should not call base method since it throws <see cref="NotImplementedException"/>.
        /// </summary>
        /// <param name="filterName">Filter name</param>
        protected virtual void ApplyDisableFilter(string filterName)
        {
            throw new NotImplementedException("DisableFilter is not implemented for " + GetType().FullName);
        }

        /// <summary>
        /// Concrete Unit of work classes should implement this
        /// method in order to enable a filter.
        /// Should not call base method since it throws <see cref="NotImplementedException"/>.
        /// </summary>
        /// <param name="filterName">Filter name</param>
        protected virtual void ApplyEnableFilter(string filterName)
        {
            throw new NotImplementedException("EnableFilter is not implemented for " + GetType().FullName);
        }


        /// <summary>
        /// Concrete Unit of work classes should implement this
        /// method in order to set a parameter's value.
        /// Should not call base method since it throws <see cref="NotImplementedException"/>.
        /// </summary>
        /// <param name="filterName">Filter name</param>
        protected virtual void ApplyFilterParameterValue(string filterName, string parameterName, object value)
        {
            throw new NotImplementedException("SetFilterParameterValue is not implemented for " + GetType().FullName);
        }

        /// <summary>
        /// Called to trigger <see cref="Completed"/> event.
        /// </summary>
        protected virtual void OnCompleted()
        {
            //Completed.InvokeSafely(this);
        }

        /// <summary>
        /// Called to trigger <see cref="Failed"/> event.
        /// </summary>
        /// <param name="exception">Exception that cause failure</param>
        protected virtual void OnFailed(System.Exception exception)
        {
          //  Failed.InvokeSafely(this, new UnitOfWorkFailedEventArgs(exception));
        }

        /// <summary>
        /// Called to trigger <see cref="Disposed"/> event.
        /// </summary>
        protected virtual void OnDisposed()
        {
           // Disposed.InvokeSafely(this);
        }

        /// <summary>
        ///  /��ֹBegin����ε���
        /// </summary>
        private void PreventMultipleBegin()
        {
            if (_isBeginCalledBefore)
            {
               // throw new AbpException("This unit of work has started before. Can not call Start method more than once.");
            }

            _isBeginCalledBefore = true;
        }

        private void PreventMultipleComplete()
        {
            if (_isCompleteCalledBefore)
            {
               // throw new AbpException("Complete is called before!");
            }

            _isCompleteCalledBefore = true;
        }

        //private void SetFilters(List<DataFilterConfiguration> filterOverrides)
        //{
        //    for (var i = 0; i < _filters.Count; i++)
        //    {
        //        var filterOverride = filterOverrides.FirstOrDefault(f => f.FilterName == _filters[i].FilterName);
        //        if (filterOverride != null)
        //        {
        //            _filters[i] = filterOverride;
        //        }
        //    }

        //    if (!AbpSession.UserId.HasValue || AbpSession.MultiTenancySide == MultiTenancySides.Host)
        //    {
        //        ChangeFilterIsEnabledIfNotOverrided(filterOverrides, AbpDataFilters.MustHaveTenant, false);
        //    }
        //}

        //private void ChangeFilterIsEnabledIfNotOverrided(List<DataFilterConfiguration> filterOverrides, string filterName, bool isEnabled)
        //{
        //    if (filterOverrides.Any(f => f.FilterName == filterName))
        //    {
        //        return;
        //    }

        //    var index = _filters.FindIndex(f => f.FilterName == filterName);
        //    if (index < 0)
        //    {
        //        return;
        //    }

        //    if (_filters[index].IsEnabled == isEnabled)
        //    {
        //        return;
        //    }

        //    _filters[index] = new DataFilterConfiguration(filterName, isEnabled);
        //}

        //private DataFilterConfiguration GetFilter(string filterName)
        //{
        //    var filter = _filters.FirstOrDefault(f => f.FilterName == filterName);
        //    if (filter == null)
        //    {
        //        throw new AbpException("Unknown filter name: " + filterName + ". Be sure this filter is registered before.");
        //    }

        //    return filter;
        //}

        //private int GetFilterIndex(string filterName)
        //{
        //    var filterIndex = _filters.FindIndex(f => f.FilterName == filterName);
        //    if (filterIndex < 0)
        //    {
        //        throw new AbpException("Unknown filter name: " + filterName + ". Be sure this filter is registered before.");
        //    }

        //    return filterIndex;
        //}
    }
}