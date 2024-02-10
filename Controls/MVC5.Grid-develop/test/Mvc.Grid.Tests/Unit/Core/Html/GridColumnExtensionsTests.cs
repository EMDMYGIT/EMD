﻿using NSubstitute;
using System;
using System.Web;
using Xunit;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    public class GridColumnExtensionsTests
    {
        private BaseGridColumn<GridModel, String> column;

        public GridColumnExtensionsTests()
        {
            column = Substitute.ForPartsOf<BaseGridColumn<GridModel, String>>();
        }

        #region RenderedAs<TModel>(this IGridColumn<TModel> column, Func<TModel, Object> value)

        [Fact]
        public void RenderedAs_SetsRenderValue()
        {
            Func<GridModel, Object> expected = (model) => model.Name;
            Func<GridModel, Object> actual = column.RenderedAs(expected).RenderValue;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void RenderedAs_ReturnsColumn()
        {
            IGridColumn actual = column.RenderedAs(model => model.Name);
            IGridColumn expected = column;

            Assert.Same(expected, actual);
        }

        #endregion

        #region MultiFilterable<T>(this T column, Boolean isMultiple)

        [Fact]
        public void MultiFilterable_SetsIsMultiFilterable()
        {
            Boolean? actual = column.MultiFilterable(true).IsMultiFilterable;
            Boolean? expected = true;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MultiFilterable_ReturnsColumn()
        {
            IGridColumn actual = column.MultiFilterable(true);
            IGridColumn expected = column;

            Assert.Same(expected, actual);
        }

        #endregion

        #region Filterable<T>(this T column, Boolean isFilterable)

        [Fact]
        public void Filterable_SetsIsFilterable()
        {
            Boolean? actual = column.Filterable(true).IsFilterable;
            Boolean? expected = true;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filterable_ReturnsColumn()
        {
            IGridColumn actual = column.Filterable(true);
            IGridColumn expected = column;

            Assert.Same(expected, actual);
        }

        #endregion

        #region FilteredAs<T>(this T column, String filterName)

        [Fact]
        public void FilteredAs_SetsFilterName()
        {
            String actual = column.FilteredAs("Numeric").FilterName;
            String expected = "Numeric";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FilteredAs_ReturnsColumn()
        {
            IGridColumn actual = column.FilteredAs("Numeric");
            IGridColumn expected = column;

            Assert.Same(expected, actual);
        }

        #endregion

        #region InitialSort<T>(this T column, GridSortOrder order)

        [Fact]
        public void InitialSort_SetsInitialSortOrder()
        {
            GridSortOrder? actual = column.InitialSort(GridSortOrder.Desc).InitialSortOrder;
            GridSortOrder? expected = GridSortOrder.Desc;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InitialSort_ReturnsColumn()
        {
            IGridColumn actual = column.InitialSort(GridSortOrder.Desc);
            IGridColumn expected = column;

            Assert.Same(expected, actual);
        }

        #endregion

        #region FirstSort<T>(this T column, GridSortOrder order)

        [Fact]
        public void FirstSort_SetsFirstOrder()
        {
            GridSortOrder? actual = column.FirstSort(GridSortOrder.Desc).FirstSortOrder;
            GridSortOrder? expected = GridSortOrder.Desc;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FirstSort_ReturnsColumn()
        {
            IGridColumn actual = column.FirstSort(GridSortOrder.Desc);
            IGridColumn expected = column;

            Assert.Same(expected, actual);
        }

        #endregion

        #region Sortable<T>(this T column, Boolean isSortable)

        [Fact]
        public void Sortable_SetsIsSortable()
        {
            Boolean? actual = column.Sortable(true).IsSortable;
            Boolean? expected = true;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Sortable_ReturnsColumn()
        {
            IGridColumn actual = column.Sortable(true);
            IGridColumn expected = column;

            Assert.Same(expected, actual);
        }

        #endregion

        #region Encoded<T>(this T column, Boolean isEncoded)

        [Fact]
        public void Encoded_SetsIsEncoded()
        {
            Assert.True(column.Encoded(true).IsEncoded);
        }

        [Fact]
        public void Encoded_ReturnsColumn()
        {
            IGridColumn actual = column.Encoded(true);
            IGridColumn expected = column;

            Assert.Same(expected, actual);
        }

        #endregion

        #region Formatted<T>(this T column, String format)

        [Fact]
        public void Formatted_SetsFormat()
        {
            String actual = column.Formatted("Format").Format;
            String expected = "Format";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Formatted_ReturnsColumn()
        {
            IGridColumn actual = column.Formatted("Format");
            IGridColumn expected = column;

            Assert.Same(expected, actual);
        }

        #endregion

        #region Css<T>(this T column, String cssClasses)

        [Fact]
        public void Css_SetsCssClasses()
        {
            String actual = column.Css("column-class").CssClasses;
            String expected = "column-class";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Css_ReturnsColumn()
        {
            IGridColumn actual = column.Css("column-class");
            IGridColumn expected = column;

            Assert.Same(expected, actual);
        }

        #endregion

        #region Titled<T>(this T column, Object value)

        [Fact]
        public void Titled_SetsHtmlContentTitle()
        {
            IHtmlString expected = new HtmlString("HtmlContent Title");
            IHtmlString actual = column.Titled(expected).Title;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void Titled_SetsObjectTitle()
        {
            String actual = column.Titled(new Object()).Title.ToString();
            String expected = new Object().ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Titled_SetsNullTitle()
        {
            Assert.Null(column.Titled(null).Title.ToString());
        }

        [Fact]
        public void Titled_SetsTitle()
        {
            String actual = column.Titled("Title").Title.ToString();
            String expected = "Title";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Titled_ReturnsColumn()
        {
            IGridColumn actual = column.Titled("Title");
            IGridColumn expected = column;

            Assert.Same(expected, actual);
        }

        #endregion

        #region Named<T>(this T column, String name)

        [Fact]
        public void Named_SetsName()
        {
            String actual = column.Named("Name").Name;
            String expected = "Name";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Named_ReturnsColumn()
        {
            IGridColumn actual = column.Named("Name");
            IGridColumn expected = column;

            Assert.Same(expected, actual);
        }

        #endregion
    }
}
