﻿using NSubstitute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Xunit;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    public class GridColumnTests : IDisposable
    {
        private IGrid<GridModel> grid;
        private static IGridFilters oldFilters;
        private GridColumn<GridModel, Object> column;

        static GridColumnTests()
        {
            oldFilters = MvcGrid.Filters;
        }
        public GridColumnTests()
        {
            MvcGrid.Filters = Substitute.For<IGridFilters>();

            grid = new Grid<GridModel>(new GridModel[0]);
            column = new GridColumn<GridModel, Object>(grid, model => model.Name);
        }
        public void Dispose()
        {
            MvcGrid.Filters = oldFilters;
        }

        #region SortOrder

        [Fact]
        public void SortOrder_Set_Caches()
        {
            grid.Query = HttpUtility.ParseQueryString("Sort=Name&Order=Asc");

            column.SortOrder = null;

            Assert.Null(column.SortOrder);
        }

        [Theory]
        [InlineData("", "Sort=Name&Order=", "Name", GridSortOrder.Desc, null)]
        [InlineData("", "Order=Desc", null, GridSortOrder.Asc, GridSortOrder.Desc)]
        [InlineData("", "Sort=Name&Order=Asc", "Name", GridSortOrder.Desc, GridSortOrder.Asc)]
        [InlineData("", "Sort=Name&Order=Desc", "Name", GridSortOrder.Asc, GridSortOrder.Desc)]
        [InlineData(null, "Sort=Name&Order=", "Name", GridSortOrder.Desc, null)]
        [InlineData(null, "Order=Desc", null, GridSortOrder.Asc, GridSortOrder.Desc)]
        [InlineData(null, "Sort=Name&Order=Asc", "Name", GridSortOrder.Desc, GridSortOrder.Asc)]
        [InlineData(null, "Sort=Name&Order=Desc", "Name", GridSortOrder.Asc, GridSortOrder.Desc)]
        [InlineData("Grid", "Grid-Sort=Name&Grid-Order=", "Name", GridSortOrder.Desc, null)]
        [InlineData("Grid", "Grid-Order=Desc", null, GridSortOrder.Asc, GridSortOrder.Desc)]
        [InlineData("Grid", "Grid-Sort=Name&Grid-Order=Asc", "Name", GridSortOrder.Desc, GridSortOrder.Asc)]
        [InlineData("Grid", "Grid-Sort=Name&Grid-Order=Desc", "Name", GridSortOrder.Asc, GridSortOrder.Desc)]
        public void SortOrder_ReturnsFromQuery(String gridName, String query, String name, GridSortOrder? initialOrder, GridSortOrder? order)
        {
            grid.Query = HttpUtility.ParseQueryString(query);
            column.InitialSortOrder = initialOrder;
            grid.Name = gridName;
            column.Name = name;

            GridSortOrder? actual = column.SortOrder;
            GridSortOrder? expected = order;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", "Sort=Name&Order=", "Sort=Name&Order=Desc")]
        [InlineData("", "Sort=Name&Order=Asc", "Sort=Name&Order=Desc")]
        [InlineData("", "Sort=Name&Order=Desc", "Sort=Name&Order=Asc")]
        [InlineData(null, "Sort=Name&Order=", "Sort=Name&Order=Desc")]
        [InlineData(null, "Sort=Name&Order=Asc", "Sort=Name&Order=Desc")]
        [InlineData(null, "Sort=Name&Order=Desc", "Sort=Name&Order=Asc")]
        [InlineData("Grid", "Grid-Sort=Name&Grid-Order=", "Grid-Sort=Name&Grid-Order=Desc")]
        [InlineData("Grid", "Grid-Sort=Name&Grid-Order=Asc", "Grid-Sort=Name&Grid-Order=Desc")]
        [InlineData("Grid", "Grid-Sort=Name&Grid-Order=Desc", "Grid-Sort=Name&Grid-Order=Asc")]
        public void SortOrder_Get_Caches(String gridName, String initialQuery, String changedQuery)
        {
            grid.Query = HttpUtility.ParseQueryString(initialQuery);
            grid.Name = gridName;

            GridSortOrder? order = column.SortOrder;

            grid.Query = HttpUtility.ParseQueryString(changedQuery);

            GridSortOrder? actual = column.SortOrder;
            GridSortOrder? expected = order;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", "-Order=Desc", "", GridSortOrder.Asc)]
        [InlineData("", "e-Sort=Name&Grid-Order=Asc", "Name", GridSortOrder.Asc)]
        [InlineData("", "R-Sort=Name&Grid-Order=Desc", "Name", GridSortOrder.Desc)]
        [InlineData(null, "-Order=Desc", "", GridSortOrder.Asc)]
        [InlineData(null, "e-Sort=Name&Grid-Order=Asc", "Name", GridSortOrder.Asc)]
        [InlineData(null, "R-Sort=Name&Grid-Order=Desc", "Name", GridSortOrder.Desc)]
        [InlineData("Grid", "Grid-Order=Desc", "", GridSortOrder.Asc)]
        [InlineData("Grid", "Gride-Sort=Name&Grid-Order=Asc", "Name", GridSortOrder.Asc)]
        [InlineData("Grid", "RGrid-Sort=Name&Grid-Order=Desc", "Name", GridSortOrder.Desc)]
        public void SortOrder_NotFound_ReturnsInitialSortOrder(String gridName, String query, String name, GridSortOrder? initialOrder)
        {
            grid.Query = HttpUtility.ParseQueryString(query);
            column.InitialSortOrder = initialOrder;
            grid.Name = gridName;
            column.Name = name;

            GridSortOrder? actual = column.SortOrder;
            GridSortOrder? expected = initialOrder;

            Assert.Equal(expected, actual);
        }

        #endregion

        #region Filter

        [Fact]
        public void Filter_ReturnsFromGridFilters()
        {
            GridColumnFilter<GridModel> filter = new GridColumnFilter<GridModel>();
            MvcGrid.Filters.GetFilter(column).Returns(filter);

            Object actual = column.Filter;
            Object expected = filter;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void Filter_Get_Caches()
        {
            GridColumnFilter<GridModel> filter = new GridColumnFilter<GridModel>();
            MvcGrid.Filters.GetFilter(column).Returns(filter);

            IGridColumnFilter<GridModel> cachedFilter = column.Filter;
            filter = new GridColumnFilter<GridModel>();

            MvcGrid.Filters.GetFilter(column).Returns(filter);

            Object expected = cachedFilter;
            Object actual = column.Filter;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void Filter_Set_Caches()
        {
            GridColumnFilter<GridModel> filter = new GridColumnFilter<GridModel>();

            column.Filter = new GridColumnFilter<GridModel>();
            column.Filter = filter;

            IGridColumnFilter<GridModel> actual = column.Filter;
            IGridColumnFilter<GridModel> expected = filter;

            Assert.Same(expected, actual);
        }

        #endregion

        #region GridColumn(IGrid<T> grid, Expression<Func<T, TValue>> expression)

        [Fact]
        public void GridColumn_SetsGrid()
        {
            IGrid actual = new GridColumn<GridModel, Object>(grid, model => model.Name).Grid;
            IGrid expected = grid;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void GridColumn_SetsIsEncoded()
        {
            Assert.True(new GridColumn<GridModel, Object>(grid, model => model.Name).IsEncoded);
        }

        [Fact]
        public void GridColumn_SetsExpression()
        {
            Expression<Func<GridModel, String>> expected = (model) => model.Name;
            Expression<Func<GridModel, String>> actual = new GridColumn<GridModel, String>(grid, expected).Expression;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void GridColumn_NotMemberExpression_SetsNullTitle()
        {
            Assert.Null(new GridColumn<GridModel, Object>(grid, model => model.ToString()).Title.ToString());
        }

        [Fact]
        public void GridColumn_NoDisplayAttribute_SetsNullTitle()
        {
            Assert.Null(new GridColumn<GridModel, Object>(grid, model => model.Name).Title.ToString());
        }

        [Fact]
        public void GridColumn_DisplayAttribute_SetsTitleFromDisplayName()
        {
            DisplayAttribute display = typeof(GridModel).GetProperty("Text").GetCustomAttribute<DisplayAttribute>();
            column = new GridColumn<GridModel, Object>(grid, model => model.Text);

            String actual = column.Title.ToString();
            String expected = display.GetName();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridColumn_DisplayAttribute_SetsTitleFromDisplayShortName()
        {
            DisplayAttribute display = typeof(GridModel).GetProperty("ShortText").GetCustomAttribute<DisplayAttribute>();
            column = new GridColumn<GridModel, Object>(grid, model => model.ShortText);

            String expected = display.GetShortName();
            String actual = column.Title.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddProperty_SetsFilterNameForEnum()
        {
            AssertFilterNameFor(model => model.EnumField, null);
        }

        [Fact]
        public void AddProperty_SetsFilterNameForSByte()
        {
            AssertFilterNameFor(model => model.SByteField, "Number");
        }

        [Fact]
        public void AddProperty_SetsFilterNameForByte()
        {
            AssertFilterNameFor(model => model.ByteField, "Number");
        }

        [Fact]
        public void AddProperty_SetsFilterNameForInt16()
        {
            AssertFilterNameFor(model => model.Int16Field, "Number");
        }

        [Fact]
        public void AddProperty_SetsFilterNameForUInt16()
        {
            AssertFilterNameFor(model => model.UInt16Field, "Number");
        }

        [Fact]
        public void AddProperty_SetsFilterNameForInt32()
        {
            AssertFilterNameFor(model => model.Int32Field, "Number");
        }

        [Fact]
        public void AddProperty_SetsFilterNameForUInt32()
        {
            AssertFilterNameFor(model => model.UInt32Field, "Number");
        }

        [Fact]
        public void AddProperty_SetsFilterNameForInt64()
        {
            AssertFilterNameFor(model => model.Int64Field, "Number");
        }

        [Fact]
        public void AddProperty_SetsFilterNameForUInt64()
        {
            AssertFilterNameFor(model => model.UInt64Field, "Number");
        }

        [Fact]
        public void AddProperty_SetsFilterNameForSingle()
        {
            AssertFilterNameFor(model => model.SingleField, "Number");
        }

        [Fact]
        public void AddProperty_SetsFilterNameForDouble()
        {
            AssertFilterNameFor(model => model.DoubleField, "Number");
        }

        [Fact]
        public void AddProperty_SetsFilterNameForDecimal()
        {
            AssertFilterNameFor(model => model.DecimalField, "Number");
        }

        [Fact]
        public void AddProperty_SetsFilterNameForString()
        {
            AssertFilterNameFor(model => model.StringField, "Text");
        }

        [Fact]
        public void AddProperty_SetsFilterNameForBoolean()
        {
            AssertFilterNameFor(model => model.BooleanField, "Boolean");
        }

        [Fact]
        public void AddProperty_SetsFilterNameForDateTime()
        {
            AssertFilterNameFor(model => model.DateTimeField, "Date");
        }

        [Fact]
        public void AddProperty_SetsFilterNameForNullableEnum()
        {
            AssertFilterNameFor(model => model.NullableEnumField, null);
        }

        [Fact]
        public void AddProperty_SetsFilterNameForNullableSByte()
        {
            AssertFilterNameFor(model => model.NullableSByteField, "Number");
        }

        [Fact]
        public void AddProperty_SetsFilterNameForNullableByte()
        {
            AssertFilterNameFor(model => model.NullableByteField, "Number");
        }

        [Fact]
        public void AddProperty_SetsFilterNameForNullableInt16()
        {
            AssertFilterNameFor(model => model.NullableInt16Field, "Number");
        }

        [Fact]
        public void AddProperty_SetsFilterNameForNullableUInt16()
        {
            AssertFilterNameFor(model => model.NullableUInt16Field, "Number");
        }

        [Fact]
        public void AddProperty_SetsFilterNameForNullableInt32()
        {
            AssertFilterNameFor(model => model.NullableInt32Field, "Number");
        }

        [Fact]
        public void AddProperty_SetsFilterNameForNullableUInt32()
        {
            AssertFilterNameFor(model => model.NullableUInt32Field, "Number");
        }

        [Fact]
        public void AddProperty_SetsFilterNameForNullableInt64()
        {
            AssertFilterNameFor(model => model.NullableInt64Field, "Number");
        }

        [Fact]
        public void AddProperty_SetsFilterNameForNullableUInt64()
        {
            AssertFilterNameFor(model => model.NullableUInt64Field, "Number");
        }

        [Fact]
        public void AddProperty_SetsFilterNameForNullableSingle()
        {
            AssertFilterNameFor(model => model.NullableSingleField, "Number");
        }

        [Fact]
        public void AddProperty_SetsFilterNameForNullableDouble()
        {
            AssertFilterNameFor(model => model.NullableDoubleField, "Number");
        }

        [Fact]
        public void AddProperty_SetsFilterNameForNullableDecimal()
        {
            AssertFilterNameFor(model => model.NullableDecimalField, "Number");
        }

        [Fact]
        public void AddProperty_SetsFilterNameForNullableBoolean()
        {
            AssertFilterNameFor(model => model.NullableBooleanField, "Boolean");
        }

        [Fact]
        public void AddProperty_SetsFilterNameForNullableDateTime()
        {
            AssertFilterNameFor(model => model.NullableDateTimeField, "Date");
        }

        [Fact]
        public void AddProperty_SetsFilterNameForOtherTypes()
        {
            AssertFilterNameFor(model => model, null);
        }

        [Fact]
        public void GridColumn_SetsExpressionValueAsCompiledExpression()
        {
            GridModel model = new GridModel { Name = "TestName" };

            String actual = column.ExpressionValue(model) as String;
            String expected = "TestName";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridColumn_SetsProcessorTypeAsPreProcessor()
        {
            GridProcessorType actual = new GridColumn<GridModel, Object>(grid, model => model.Name).ProcessorType;
            GridProcessorType expected = GridProcessorType.Pre;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridColumn_NotNonMemberExpression_IsNotSortable()
        {
            Assert.False(new GridColumn<GridModel, String>(grid, model => model.ToString()).IsSortable);
        }

        [Fact]
        public void GridColumn_NotNonMemberExpression_IsSortableIsNull()
        {
            Assert.Null(new GridColumn<GridModel, Int32>(grid, model => model.Sum).IsSortable);
        }

        [Fact]
        public void GridColumn_NotMemberExpression_IsNotFilterable()
        {
            Assert.False(new GridColumn<GridModel, String>(grid, model => model.ToString()).IsFilterable);
        }

        [Fact]
        public void GridColumn_MemberExpression_IsFilterableIsNull()
        {
            Assert.Null(new GridColumn<GridModel, Int32>(grid, model => model.Sum).IsFilterable);
        }

        [Fact]
        public void GridColumn_SetsNameFromExpression()
        {
            Expression<Func<GridModel, String>> expression = (model) => model.Name;

            String actual = new GridColumn<GridModel, String>(grid, expression).Name;
            String expected = ExpressionHelper.GetExpressionText(expression);

            Assert.Equal(expected, actual);
        }

        #endregion

        #region Process(IQueryable<T> items)

        [Fact]
        public void Process_NoFilter_ReturnsSameItems()
        {
            column.Filter = null;
            column.IsSortable = false;
            column.IsFilterable = true;
            column.SortOrder = GridSortOrder.Desc;

            IQueryable<GridModel> expected = new GridModel[2].AsQueryable();
            IQueryable<GridModel> actual = column.Process(expected);

            Assert.Same(expected, actual);
        }

        [Fact]
        public void Process_NotFilterable_ReturnsSameItems()
        {
            column.IsSortable = false;
            column.IsFilterable = false;
            column.SortOrder = GridSortOrder.Desc;
            column.Filter = Substitute.For<IGridColumnFilter<GridModel>>();

            IQueryable<GridModel> expected = new GridModel[2].AsQueryable();
            IQueryable<GridModel> actual = column.Process(expected);

            Assert.Same(expected, actual);
        }

        [Fact]
        public void Process_ReturnsFilteredItems()
        {
            column.IsSortable = false;
            column.IsFilterable = true;
            column.SortOrder = GridSortOrder.Desc;
            column.Filter = Substitute.For<IGridColumnFilter<GridModel>>();

            IQueryable<GridModel> filteredItems = new GridModel[2].AsQueryable();
            IQueryable<GridModel> items = new GridModel[2].AsQueryable();
            column.Filter.Process(items).Returns(filteredItems);

            IQueryable<GridModel> actual = column.Process(items);
            IQueryable<GridModel> expected = filteredItems;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void Process_NoSortOrder_ReturnsSameItems()
        {
            column.IsFilterable = false;
            column.IsSortable = true;
            column.SortOrder = null;

            IQueryable<GridModel> expected = new GridModel[2].AsQueryable();
            IQueryable<GridModel> actual = column.Process(expected);

            Assert.Same(expected, actual);
        }

        [Fact]
        public void Process_NotSortable_ReturnsSameItems()
        {
            column.IsSortable = false;
            column.IsFilterable = false;
            column.SortOrder = GridSortOrder.Desc;

            IQueryable<GridModel> expected = new GridModel[2].AsQueryable();
            IQueryable<GridModel> actual = column.Process(expected);

            Assert.Same(expected, actual);
        }

        [Fact]
        public void Process_ReturnsItemsSortedInAscendingOrder()
        {
            column.IsSortable = true;
            column.IsFilterable = false;
            column.SortOrder = GridSortOrder.Asc;
            GridModel[] items = { new GridModel { Name = "B" }, new GridModel { Name = "A" }};

            IEnumerable<GridModel> expected = items.OrderBy(model => model.Name);
            IEnumerable<GridModel> actual = column.Process(items.AsQueryable());

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Process_ReturnsItemsSortedInDescendingOrder()
        {
            column.IsSortable = true;
            column.IsFilterable = false;
            column.SortOrder = GridSortOrder.Desc;
            GridModel[] items = { new GridModel { Name = "A" }, new GridModel { Name = "B" } };

            IEnumerable<GridModel> expected = items.OrderByDescending(model => model.Name);
            IEnumerable<GridModel> actual = column.Process(items.AsQueryable());

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Process_ReturnsFilteredAndSortedItems()
        {
            column.IsSortable = true;
            column.IsFilterable = true;
            column.SortOrder = GridSortOrder.Desc;
            column.Filter = Substitute.For<IGridColumnFilter<GridModel>>();
            IQueryable<GridModel> items = new [] { new GridModel { Name = "A" }, new GridModel { Name = "B" }, new GridModel { Name = "C" } }.AsQueryable();
            column.Filter.Process(items).Returns(items.Where(model => model.Name != "A").ToList().AsQueryable());

            IEnumerable<GridModel> expected = items.Where(model => model.Name != "A").OrderByDescending(model => model.Name);
            IEnumerable<GridModel> actual = column.Process(items);

            Assert.Equal(expected, actual);
        }

        #endregion

        #region ValueFor(IGridRow<Object> row)

        [Fact]
        public void ValueFor_NullReferenceInExpressionValue_ReturnsEmpty()
        {
            column.ExpressionValue = (model) => model.Name;

            String actual = column.ValueFor(new GridRow<Object>(null)).ToString();

            Assert.Empty(actual);
        }

        [Fact]
        public void ValueFor_NullReferenceInRenderValue_ReturnsEmpty()
        {
            column.RenderValue = (model) => model.Name;

            String actual = column.ValueFor(new GridRow<Object>(null)).ToString();

            Assert.Empty(actual);
        }

        [Fact]
        public void ValueFor_ExpressionValue_ThrowsNotNullReferenceException()
        {
            column.ExpressionValue = (model) => Int32.Parse("Zero");

            Assert.Throws<FormatException>(() => column.ValueFor(new GridRow<Object>(null)));
        }

        [Fact]
        public void ValueFor_RenderValue_ThrowsNotNullReferenceException()
        {
            column.RenderValue = (model) => Int32.Parse("Zero");

            Assert.Throws<FormatException>(() => column.ValueFor(new GridRow<Object>(null)));
        }

        [Theory]
        [InlineData(null, "For {0}", true, "")]
        [InlineData(null, "For {0}", false, "")]
        [InlineData("<name>", null, true, "<name>")]
        [InlineData("<name>", null, false, "<name>")]
        [InlineData("<name>", "For <{0}>", true, "<name>")]
        [InlineData("<name>", "For <{0}>", false, "<name>")]
        public void ValueFor_RenderValue_Html(String value, String format, Boolean isEncoded, String renderedValue)
        {
            IGridRow<GridModel> row = new GridRow<GridModel>(new GridModel { Content = value == null ? null : new HtmlString(value) });
            column.RenderValue = (model) => model.Content;
            column.ExpressionValue = null;
            column.IsEncoded = isEncoded;
            column.Format = format;

            String actual = column.ValueFor(row).ToString();
            String expected = renderedValue;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null, "For {0}", true, "")]
        [InlineData(null, "For {0}", false, "")]
        [InlineData("<name>", null, true, "<name>")]
        [InlineData("<name>", null, false, "<name>")]
        [InlineData("<name>", "For <{0}>", true, "<name>")]
        [InlineData("<name>", "For <{0}>", false, "<name>")]
        public void ValueFor_ExpressionValue_Html(String value, String format, Boolean isEncoded, String expressionValue)
        {
            IGridRow<GridModel> row = new GridRow<GridModel>(new GridModel { Content = value == null ? null : new HtmlString(value) });
            column = new GridColumn<GridModel, Object>(grid, model => model.Content);
            column.IsEncoded = isEncoded;
            column.Format = format;

            String actual = column.ValueFor(row).ToString();
            String expected = expressionValue;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null, "For {0}", true, "")]
        [InlineData(null, "For {0}", false, "")]
        [InlineData("<name>", null, false, "<name>")]
        [InlineData("<name>", null, true, "&lt;name&gt;")]
        [InlineData("<name>", "For <{0}>", false, "For <<name>>")]
        [InlineData("<name>", "For <{0}>", true, "For &lt;&lt;name&gt;&gt;")]
        public void ValueFor_RenderValue(String value, String format, Boolean isEncoded, String renderedValue)
        {
            IGridRow<GridModel> row = new GridRow<GridModel>(new GridModel { Name = value });
            column.RenderValue = (model) => model.Name;
            column.ExpressionValue = null;
            column.IsEncoded = isEncoded;
            column.Format = format;

            String actual = column.ValueFor(row).ToString();
            String expected = renderedValue;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null, "For {0}", true, "")]
        [InlineData(null, "For {0}", false, "")]
        [InlineData("<name>", null, false, "<name>")]
        [InlineData("<name>", null, true, "&lt;name&gt;")]
        [InlineData("<name>", "For <{0}>", false, "For <<name>>")]
        [InlineData("<name>", "For <{0}>", true, "For &lt;&lt;name&gt;&gt;")]
        public void ValueFor_ExpressionValue(String value, String format, Boolean isEncoded, String expressionValue)
        {
            IGridRow<GridModel> row = new GridRow<GridModel>(new GridModel { Name = value });
            column.IsEncoded = isEncoded;
            column.Format = format;

            String actual = column.ValueFor(row).ToString();
            String expected = expressionValue;

            Assert.Equal(expected, actual);
        }

        #endregion

        #region Test helpers

        private void AssertFilterNameFor<TValue>(Expression<Func<AllTypesModel, TValue>> property, String name)
        {
            String actual = new GridColumn<AllTypesModel, TValue>(null, property).FilterName;
            String expected = name;

            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
