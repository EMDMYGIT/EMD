﻿using System;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    public class GridColumnsTests
    {
        private GridColumns<GridModel> columns;

        public GridColumnsTests()
        {
            columns = new GridColumns<GridModel>(new Grid<GridModel>(new GridModel[0]));
            columns.Grid.Query = new NameValueCollection();
        }

        #region GridColumns(IGrid<T> grid)

        [Fact]
        public void GridColumns_SetsGrid()
        {
            IGrid actual = new GridColumns<GridModel>(columns.Grid).Grid;
            IGrid expected = columns.Grid;

            Assert.Same(expected, actual);
        }

        #endregion

        #region Add<TValue>(Expression<Func<T, TValue>> expression)

        #region Add()

        [Fact]
        public void Add_EmptyGridColumn()
        {
            columns.Add();

            GridColumn<GridModel, Object> expected = new GridColumn<GridModel, Object>(columns.Grid, model => null);
            GridColumn<GridModel, Object> actual = columns.Single() as GridColumn<GridModel, Object>;

            Assert.Equal(expected.Title.ToString(), actual.Title.ToString());
            Assert.Equal(expected.ProcessorType, actual.ProcessorType);
            Assert.Equal(expected.IsFilterable, actual.IsFilterable);
            Assert.Null(actual.Expression.Compile().Invoke(null));
            Assert.Equal(expected.FilterName, actual.FilterName);
            Assert.Equal(expected.CssClasses, actual.CssClasses);
            Assert.Equal(expected.IsSortable, actual.IsSortable);
            Assert.Equal(expected.SortOrder, actual.SortOrder);
            Assert.Equal(expected.IsEncoded, actual.IsEncoded);
            Assert.Equal(expected.Format, actual.Format);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Grid, actual.Grid);
        }

        #endregion

        [Fact]
        public void Add_GridColumn()
        {
            Expression<Func<GridModel, String>> expression = (model) => model.Name;
            columns.Add(expression);

            GridColumn<GridModel, String> expected = new GridColumn<GridModel, String>(columns.Grid, expression);
            GridColumn<GridModel, String> actual = columns.Single() as GridColumn<GridModel, String>;

            Assert.Equal(expected.Title.ToString(), actual.Title.ToString());
            Assert.Equal(expected.ProcessorType, actual.ProcessorType);
            Assert.Equal(expected.IsFilterable, actual.IsFilterable);
            Assert.Equal(expected.FilterName, actual.FilterName);
            Assert.Equal(expected.Expression, actual.Expression);
            Assert.Equal(expected.CssClasses, actual.CssClasses);
            Assert.Equal(expected.IsSortable, actual.IsSortable);
            Assert.Equal(expected.SortOrder, actual.SortOrder);
            Assert.Equal(expected.IsEncoded, actual.IsEncoded);
            Assert.Equal(expected.Format, actual.Format);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Grid, actual.Grid);
        }

        [Fact]
        public void Add_GridColumnProcessor()
        {
            columns.Add(model => model.Name);

            Object actual = columns.Grid.Processors.Single();
            Object expected = columns.Single();

            Assert.Same(expected, actual);
        }

        [Fact]
        public void Add_ReturnsAddedColumn()
        {
            IGridColumn actual = columns.Add(model => model.Name);
            IGridColumn expected = columns.Single();

            Assert.Same(expected, actual);
        }

        #endregion

        #region Insert<TValue>(Int32 index, Expression<Func<T, TValue>> expression)

        #region Insert(Int32 index)

        [Fact]
        public void Insert_EmptyGridColumn()
        {
            columns.Add(model => model.Name);
            columns.Insert(0);

            GridColumn<GridModel, Object> expected = new GridColumn<GridModel, Object>(columns.Grid, model => null);
            GridColumn<GridModel, Object> actual = columns.First() as GridColumn<GridModel, Object>;

            Assert.Equal(expected.Title.ToString(), actual.Title.ToString());
            Assert.Equal(expected.ProcessorType, actual.ProcessorType);
            Assert.Equal(expected.IsFilterable, actual.IsFilterable);
            Assert.Null(actual.Expression.Compile().Invoke(null));
            Assert.Equal(expected.FilterName, actual.FilterName);
            Assert.Equal(expected.CssClasses, actual.CssClasses);
            Assert.Equal(expected.IsSortable, actual.IsSortable);
            Assert.Equal(expected.SortOrder, actual.SortOrder);
            Assert.Equal(expected.IsEncoded, actual.IsEncoded);
            Assert.Equal(expected.Format, actual.Format);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Grid, actual.Grid);
        }

        #endregion

        [Fact]
        public void Insert_GridColumn()
        {
            Expression<Func<GridModel, Int32>> expression = (model) => model.Sum;
            columns.Add(model => model.Name);
            columns.Insert(0, expression);

            GridColumn<GridModel, Int32> expected = new GridColumn<GridModel, Int32>(columns.Grid, expression);
            GridColumn<GridModel, Int32> actual = columns.First() as GridColumn<GridModel, Int32>;

            Assert.Equal(expected.Title.ToString(), actual.Title.ToString());
            Assert.Equal(expected.ProcessorType, actual.ProcessorType);
            Assert.Equal(expected.IsFilterable, actual.IsFilterable);
            Assert.Equal(expected.FilterName, actual.FilterName);
            Assert.Equal(expected.Expression, actual.Expression);
            Assert.Equal(expected.CssClasses, actual.CssClasses);
            Assert.Equal(expected.IsSortable, actual.IsSortable);
            Assert.Equal(expected.SortOrder, actual.SortOrder);
            Assert.Equal(expected.IsEncoded, actual.IsEncoded);
            Assert.Equal(expected.Format, actual.Format);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Grid, actual.Grid);
        }

        [Fact]
        public void Insert_GridColumnProcessor()
        {
            columns.Insert(0, model => model.Name);

            Object actual = columns.Grid.Processors.Single();
            Object expected = columns.Single();

            Assert.Same(expected, actual);
        }

        [Fact]
        public void Insert_ReturnsInsertedColumn()
        {
            IGridColumn actual = columns.Insert(0, model => model.Name);
            IGridColumn expected = columns.Single();

            Assert.Same(expected, actual);
        }

        #endregion
    }
}
