﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CommonFrameWork.Domain.Entities;
using CommonFrameWork.Domain.Repositories;
using CommonFrameWork.Specifications;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;

namespace CommonFrameWork.Extensions.NHibernate
{
    /// <summary>
    /// Represents the extension method provider for IQueryable{T} interface.
    /// </summary>
    internal static class QueryableExtender
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IFutureValue<TResult> ToFutureValue<TSource, TResult>(this IQueryable<TSource> source,
            Expression<Func<IQueryable<TSource>, TResult>> selector)
            where TResult : struct
        {
            var provider = (INhQueryProvider)source.Provider;
            var method = ((MethodCallExpression)selector.Body).Method;
            var expression = Expression.Call(null, method, source.Expression);
            return (IFutureValue<TResult>)provider.ExecuteFuture(expression);
        }
    }

    /// <summary>
    /// Represents the repository context which supports NHibernate implementation.
    /// </summary>
    public class NHibernateContext : RepositoryContext, INHibernateContext
    {


        #region Private Fields
        //private readonly DatabaseSessionFactory databaseSessionFactory;
        private ISession session = null;
        private readonly ISessionFactory sessionFactory = null;
        private ITransaction transaction;
        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="NHibernateContext"/> class.
        /// </summary>
        /// <param name="sessionFactory">The session factory.</param>
        public NHibernateContext(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }
        #endregion

        #region Private Methods
        private void EnsureSession()
        {
            if (this.session == null || !this.session.IsOpen)
            {
                this.session = this.sessionFactory.OpenSession();
                this.transaction = this.session.BeginTransaction();
            }
            else
            {
                if (this.transaction == null || !this.transaction.IsActive)
                {
                    this.transaction = this.session.BeginTransaction();
                }
            }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">A <see cref="System.Boolean"/> value which indicates whether
        /// the object should be disposed explicitly.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (transaction != null)
                {
                    transaction.Dispose();
                    transaction = null;
                }
                if (session != null)
                {
                    session.Dispose();
                    session = null;
                }
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Registers a new object to the repository context.
        /// </summary>
        /// <param name="obj">The object to be registered.</param>
        public override void RegisterNew(object obj)
        {
            EnsureSession();
            session.Save(obj);
        }
        /// <summary>
        /// Registers a deleted object to the repository context.
        /// </summary>
        /// <param name="obj">The object to be registered.</param>
        public override void RegisterDeleted(object obj)
        {
            EnsureSession();
            session.Delete(obj);
        }
        /// <summary>
        /// Registers a modified object to the repository context.
        /// </summary>
        /// <param name="obj">The object to be registered.</param>
        public override void RegisterModified(object obj)
        {
            EnsureSession();
            session.Update(obj);
        }

        #endregion

        #region IUnitOfWork Members
        /// <summary>
        /// Gets a <see cref="System.Boolean"/> value which indicates
        /// whether the Unit of Work was successfully committed.
        /// </summary>
        public override bool Committed
        {
            get
            {
                return transaction != null &&
                    transaction.WasCommitted;
            }
            protected set { }
        }
        /// <summary>
        /// Commits the transaction.
        /// </summary>
        public override void Commit()
        {
            EnsureSession();
            transaction.Commit();
        }

        public override Task CommitAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(Commit, cancellationToken);
        }

        /// <summary>
        /// Rollback the transaction.
        /// </summary>
        public override void Rollback()
        {
            EnsureSession();
            transaction.Rollback();
        }

        #endregion


        #region INHibernateContext Members

        /// <summary>
        /// Gets the aggregate root instance from repository by a given key.
        /// </summary>
        /// <param name="key">The key of the aggregate root.</param>
        /// <returns>The instance of the aggregate root.</returns>
        public TAggregateRoot GetByKey<TKey, TAggregateRoot>(object key)
            where TAggregateRoot : class, IAggregateRoot<TKey>
        {
            EnsureSession();
            var result = (TAggregateRoot)this.session.Get(typeof(TAggregateRoot), key);
            Commit();
            return result;
        }

        /// <summary>
        /// Finds all the aggregate roots from repository.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <returns>The aggregate roots.</returns>
        public IQueryable<TAggregateRoot> FindAll<TKey, TAggregateRoot>(
            ISpecification<TAggregateRoot> specification,
            System.Linq.Expressions.Expression<Func<TAggregateRoot, dynamic>> sortPredicate,
            OrderBy sortOrder) where TAggregateRoot : class, IAggregateRoot<TKey>
        {
            EnsureSession();
            IQueryable<TAggregateRoot> result = null;

            var query = this.session.Query<TAggregateRoot>().Where(specification.GetExpression());
            switch (sortOrder)
            {
                case OrderBy.Ascending:
                    if (sortPredicate != null) result = query.OrderBy(sortPredicate);
                    break;
                case OrderBy.Descending:
                    if (sortPredicate != null) result = query.OrderByDescending(sortPredicate);
                    break;
                default:
                    result = query;
                    break;
            }

            Commit();
            return result;
        }

        /// <summary>
        /// Finds all the aggregate roots from repository.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="SortOrder"/> enumeration which specifies the sort order.</param>
        /// <param name="pageNumber">The number of objects per page.</param>
        /// <param name="pageSize">The number of objects per page.</param>
        /// <returns>The aggregate roots.</returns>
        public PagedResult<TAggregateRoot> FindAll<TKey, TAggregateRoot>(ISpecification<TAggregateRoot> specification, System.Linq.Expressions.Expression<Func<TAggregateRoot, dynamic>> sortPredicate, OrderBy sortOrder, int pageNumber, int pageSize) where TAggregateRoot : class, IAggregateRoot<TKey>
        {
            EnsureSession();
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "The pageNumber is one-based and should be larger than zero.");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "The pageSize is one-based and should be larger than zero.");
            if (sortPredicate == null)
                throw new ArgumentNullException("sortPredicate");

            var query = this.session.Query<TAggregateRoot>()
                .Where(specification.GetExpression());

            int skip = (pageNumber - 1) * pageSize;
            int take = pageSize;
            int totalCount = 0;
            int totalPages = 0;
            List<TAggregateRoot> pagedData = null;
            PagedResult<TAggregateRoot> result = null;

            switch (sortOrder)
            {
                case OrderBy.Ascending:
                    totalCount = query.ToFutureValue(x => x.Count()).Value;
                    totalPages = (totalCount + pageSize - 1) / pageSize;
                    pagedData = query.OrderBy(sortPredicate).Skip(skip).Take(take).ToFuture().ToList();
                    result = new PagedResult<TAggregateRoot>(totalCount, totalPages, pageSize, pageNumber, pagedData);
                    break;
                case OrderBy.Descending:
                    totalCount = query.ToFutureValue(x => x.Count()).Value;
                    totalPages = (totalCount + pageSize - 1) / pageSize;
                    pagedData = query.OrderByDescending(sortPredicate).Skip(skip).Take(take).ToFuture().ToList();
                    result = new PagedResult<TAggregateRoot>(totalCount, totalPages, pageSize, pageNumber, pagedData);
                    break;
                default:
                    break;

            }
            Commit();
            return result;
        }
        /// <summary>
        /// Finds a single aggregate root from the repository.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate root should match.</param>
        /// <returns>The instance of the aggregate root.</returns>
        public TAggregateRoot Find<TKey, TAggregateRoot>(ISpecification<TAggregateRoot> specification) where TAggregateRoot : class, IAggregateRoot<TKey>
        {
            EnsureSession();
            var result = this.session.Query<TAggregateRoot>().Where(specification.GetExpression()).FirstOrDefault();
            Commit();
            return result;
        }

        #endregion
    }
}
