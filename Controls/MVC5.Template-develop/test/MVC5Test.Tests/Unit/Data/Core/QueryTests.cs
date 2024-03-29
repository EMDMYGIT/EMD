﻿using AutoMapper.QueryableExtensions;
using MVC5Test.Data.Core;
using MVC5Test.Tests.Data;
using MVC5Test.Tests.Objects;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace MVC5Test.Tests.Unit.Data.Core
{
    public class QueryTests : IDisposable
    {
        private TestingContext context;
        private Query<TestModel> select;

        public QueryTests()
        {
            context = new TestingContext();
            select = new Query<TestModel>(context.Set<TestModel>());

            context.Set<TestModel>().RemoveRange(context.Set<TestModel>());
            context.Set<TestModel>().Add(ObjectFactory.CreateTestModel());
            context.SaveChanges();
        }
        public void Dispose()
        {
            context.Dispose();
        }

        #region ElementType

        [Fact]
        public void ElementType_IsModelType()
        {
            Object actual = (select as IQueryable).ElementType;
            Object expected = typeof(TestModel);

            Assert.Same(expected, actual);
        }

        #endregion

        #region Expression

        [Fact]
        public void Expression_IsSetsExpression()
        {
            DbSet<TestModel> set = Substitute.For<DbSet<TestModel>, IQueryable>();
            TestingContext testingContext = Substitute.For<TestingContext>();
            ((IQueryable)set).Expression.Returns(Expression.Empty());
            testingContext.Set<TestModel>().Returns(set);

            select = new Query<TestModel>(testingContext.Set<TestModel>());

            Object actual = ((IQueryable)select).Expression;
            Object expected = ((IQueryable)set).Expression;

            Assert.Same(expected, actual);
        }

        #endregion

        #region Provider

        [Fact]
        public void Provider_IsSetsProvider()
        {
            Object expected = (context.Set<TestModel>() as IQueryable).Provider;
            Object actual = (select as IQueryable).Provider;

            Assert.Same(expected, actual);
        }

        #endregion

        #region Select<TResult>(Expression<Func<TModel, TResult>> selector)

        [Fact]
        public void Select_Selects()
        {
            IEnumerable<Int32> expected = context.Set<TestModel>().Select(model => model.Id);
            IEnumerable<Int32> actual = select.Select(model => model.Id);

            Assert.Equal(expected, actual);
        }

        #endregion

        #region Where(Expression<Func<TModel, Boolean>> predicate)

        [Fact]
        public void Where_Filters()
        {
            IEnumerable<TestModel> actual = select.Where(model => true);
            IEnumerable<TestModel> expected = context.Set<TestModel>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Where_ReturnsItself()
        {
            Object actual = select.Where(model => true);
            Object expected = select;

            Assert.Same(expected, actual);
        }

        #endregion

        #region To<TView>()

        [Fact]
        public void To_ProjectsSet()
        {
            IEnumerable<Int32> expected = context.Set<TestModel>().ProjectTo<TestView>().Select(view => view.Id).ToArray();
            IEnumerable<Int32> actual = select.To<TestView>().Select(view => view.Id).ToArray();

            Assert.Equal(expected, actual);
        }

        #endregion

        #region GetEnumerator()

        [Fact]
        public void GetEnumerator_ReturnsSetEnumerator()
        {
            IEnumerable<TestModel> expected = context.Set<TestModel>();
            IEnumerable<TestModel> actual = select.ToArray();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetEnumerator_ReturnsSameEnumerator()
        {
            IEnumerable<TestModel> expected = context.Set<TestModel>();
            IEnumerable<TestModel> actual = select;

            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
