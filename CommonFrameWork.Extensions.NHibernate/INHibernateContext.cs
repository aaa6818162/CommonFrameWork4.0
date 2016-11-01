

using System;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using CommonFrameWork.Domain.Entities;
using CommonFrameWork.Domain.Repositories;
using CommonFrameWork.Specifications;

namespace CommonFrameWork.Extensions.NHibernate
{
    /// <summary>
    /// Represents that the implemented classes are NHibernate contexts.
    /// </summary>
    public interface INHibernateContext : IRepositoryContext
    {
        #region Methods

        /// <summary>
        /// Gets the aggregate root instance from repository by a given key.
        /// </summary>
        /// <param name="key">The key of the aggregate root.</param>
        /// <returns>The instance of the aggregate root.</returns>
        TAggregateRoot GetByKey<TKey, TAggregateRoot>(object key) where TAggregateRoot : class, IAggregateRoot<TKey>;
        /// <summary>
        /// Finds all the aggregate roots from repository.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <returns>The aggregate roots.</returns>
        IQueryable<TAggregateRoot> FindAll<TKey, TAggregateRoot>(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, OrderBy sortOrder)
            where TAggregateRoot : class, IAggregateRoot<TKey>;
        /// <summary>
        /// Finds all the aggregate roots from repository.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <param name="pageSize">The number of objects per page.</param>
        /// <param name="pageSize">The number of objects per page.</param>
        /// <returns>The aggregate roots.</returns>
        PagedResult<TAggregateRoot> FindAll<TKey, TAggregateRoot>(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, OrderBy sortOrder, int pageNumber, int pageSize)
            where TAggregateRoot : class, IAggregateRoot<TKey>;
        /// <summary>
        /// Finds a single aggregate root from the repository.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate root should match.</param>
        /// <returns>The instance of the aggregate root.</returns>
        TAggregateRoot Find<TKey, TAggregateRoot>(ISpecification<TAggregateRoot> specification)
            where TAggregateRoot : class, IAggregateRoot<TKey>;

        #endregion
    }
}
