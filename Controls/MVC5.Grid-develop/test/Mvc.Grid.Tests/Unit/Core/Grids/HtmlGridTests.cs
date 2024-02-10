﻿using NSubstitute;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xunit;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    public class HtmlGridTests
    {
        private HtmlHelper html;
        private IGrid<GridModel> grid;
        private HtmlGrid<GridModel> htmlGrid;

        public HtmlGridTests()
        {
            html = HtmlHelperFactory.CreateHtmlHelper("id=3&name=jim");
            grid = new Grid<GridModel>(new GridModel[8]);

            htmlGrid = new HtmlGrid<GridModel>(html, grid);
            grid.Columns.Add(model => model.Name);
            grid.Columns.Add(model => model.Sum);
        }

        #region HtmlGrid(HtmlHelper html, IGrid<T> grid)

        [Fact]
        public void HtmlGrid_DoesNotChangeQuery()
        {
            NameValueCollection query = grid.Query = new NameValueCollection();

            Object actual = new HtmlGrid<GridModel>(html, grid).Grid.Query;
            Object expected = query;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void HtmlGrid_SetsGridQuery()
        {
            grid.Query = null;

            NameValueCollection expected = html.ViewContext.HttpContext.Request.QueryString;
            NameValueCollection actual = new HtmlGrid<GridModel>(html, grid).Grid.Query;

            foreach (String key in expected)
                Assert.Equal(expected[key], actual[key]);

            Assert.Equal(expected.AllKeys, actual.AllKeys);
            Assert.NotSame(expected, actual);
        }

        [Fact]
        public void HtmlGrid_DoesNotChangeHttpContext()
        {
            HttpContextBase httpContext = grid.HttpContext = HttpContextFactory.CreateHttpContextBase("");

            Object actual = new HtmlGrid<GridModel>(html, grid).Grid.HttpContext;
            Object expected = httpContext;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void HtmlGrid_SetsHttpContext()
        {
            grid.HttpContext = null;

            HttpContextBase actual = new HtmlGrid<GridModel>(html, grid).Grid.HttpContext;
            HttpContextBase expected = html.ViewContext.HttpContext;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void HtmlGrid_SetsPartialViewName()
        {
            String actual = new HtmlGrid<GridModel>(null, grid).PartialViewName;
            String expected = "MvcGrid/_Grid";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void HtmlGrid_SetsHtml()
        {
            HtmlHelper actual = new HtmlGrid<GridModel>(html, grid).Html;
            HtmlHelper expected = html;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void HtmlGrid_SetsGrid()
        {
            IGrid<GridModel> actual = new HtmlGrid<GridModel>(null, grid).Grid;
            IGrid<GridModel> expected = grid;

            Assert.Same(expected, actual);
        }

        #endregion

        #region Build(Action<IGridColumnsOf<T>> builder)

        [Fact]
        public void Build_Columns()
        {
            Action<IGridColumnsOf<GridModel>> columns = Substitute.For<Action<IGridColumnsOf<GridModel>>>();

            htmlGrid.Build(columns);

            columns.Received()(htmlGrid.Grid.Columns);
        }

        [Fact]
        public void Build_ReturnsItself()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.Build(columns => { });
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.Same(expected, actual);
        }

        #endregion

        #region ProcessWith(IGridProcessor<T> processor)

        [Fact]
        public void ProcessWith_AddsProcessorToGrid()
        {
            IGridProcessor<GridModel> processor = Substitute.For<IGridProcessor<GridModel>>();
            htmlGrid.Grid.Processors.Clear();
            htmlGrid.ProcessWith(processor);

            IGridProcessor<GridModel> actual = htmlGrid.Grid.Processors.Single();
            IGridProcessor<GridModel> expected = processor;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void ProcessWith_ReturnsItself()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.ProcessWith(null);
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.Same(expected, actual);
        }

        #endregion

        #region Filterable(Boolean isFilterable)

        [Theory]
        [InlineData(true, true, true)]
        [InlineData(true, false, true)]
        [InlineData(false, true, false)]
        [InlineData(false, false, false)]
        [InlineData(null, true, true)]
        [InlineData(null, false, false)]
        public void Filterable_Set_IsFilterable(Boolean? isColumnFilterable, Boolean isGridFilterable, Boolean? filterable)
        {
            foreach (IGridColumn column in htmlGrid.Grid.Columns)
                column.IsFilterable = isColumnFilterable;

            htmlGrid.Filterable(isGridFilterable);

            foreach (IGridColumn actual in htmlGrid.Grid.Columns)
                Assert.Equal(filterable, actual.IsFilterable);
        }

        [Fact]
        public void Filterable_Set_ReturnsItself()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.Filterable(true);
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.Same(expected, actual);
        }

        #endregion

        #region MultiFilterable()

        [Theory]
        [InlineData(null, true)]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void MultiFilterable_SetsIsMultiFilterable(Boolean? multi, Boolean? filterable)
        {
            foreach (IGridColumn column in htmlGrid.Grid.Columns)
                column.IsMultiFilterable = multi;

            htmlGrid.MultiFilterable();

            foreach (IGridColumn actual in htmlGrid.Grid.Columns)
                Assert.Equal(filterable, actual.IsMultiFilterable);
        }

        [Fact]
        public void MultiFilterable_ReturnsItself()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.MultiFilterable();
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.Same(expected, actual);
        }

        #endregion

        #region Filterable()

        [Theory]
        [InlineData(null, true)]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void Filterable_SetsIsFilterable(Boolean? isFilterable, Boolean? filterable)
        {
            foreach (IGridColumn column in htmlGrid.Grid.Columns)
                column.IsFilterable = isFilterable;

            htmlGrid.Filterable();

            foreach (IGridColumn actual in htmlGrid.Grid.Columns)
                Assert.Equal(filterable, actual.IsFilterable);
        }

        [Fact]
        public void Filterable_ReturnsItself()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.Filterable();
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.Same(expected, actual);
        }

        #endregion

        #region Sortable(Boolean isSortable)

        [Theory]
        [InlineData(true, true, true)]
        [InlineData(true, false, true)]
        [InlineData(false, true, false)]
        [InlineData(false, false, false)]
        [InlineData(null, true, true)]
        [InlineData(null, false, false)]
        public void Sortable_Set_IsSortable(Boolean? isColumnSortable, Boolean isGridSortable, Boolean? sortable)
        {
            foreach (IGridColumn column in htmlGrid.Grid.Columns)
                column.IsSortable = isColumnSortable;

            htmlGrid.Sortable(isGridSortable);

            foreach (IGridColumn actual in htmlGrid.Grid.Columns)
                Assert.Equal(sortable, actual.IsSortable);
        }

        [Fact]
        public void Sortable_Set_ReturnsItself()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.Sortable(true);
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.Same(expected, actual);
        }

        #endregion

        #region Sortable()

        [Theory]
        [InlineData(null, true)]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void Sortable_SetsIsSortableToTrue(Boolean? isSortable, Boolean? sortable)
        {
            foreach (IGridColumn column in htmlGrid.Grid.Columns)
                column.IsSortable = isSortable;

            htmlGrid.Sortable();

            foreach (IGridColumn actual in htmlGrid.Grid.Columns)
                Assert.Equal(sortable, actual.IsSortable);
        }

        [Fact]
        public void Sortable_ReturnsItself()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.Sortable();
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.Same(expected, actual);
        }

        #endregion

        #region RowCss(Func<T, String> cssClasses)

        [Fact]
        public void RowCss_SetsRowsCssClasses()
        {
            Func<GridModel, String> expected = (model) => "";
            Func<GridModel, String> actual = htmlGrid.RowCss(expected).Grid.Rows.CssClasses;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void RowCss_ReturnsItself()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.RowCss(null);
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.Same(expected, actual);
        }

        #endregion

        #region Attributed(Object htmlAttributes)

        [Fact]
        public void Attributed_SetsAttributes()
        {
            KeyValuePair<String, Object> actual = htmlGrid.Attributed(new { width = 1 }).Grid.Attributes.Single();
            KeyValuePair<String, Object> expected = new KeyValuePair<String, Object>("width", 1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Attributed_ReturnsItself()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.Attributed(new { width = 1 });
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.Same(expected, actual);
        }

        #endregion

        #region Css(String cssClasses)

        [Fact]
        public void Css_SetsCssClasses()
        {
            String actual = htmlGrid.Css("table").Grid.CssClasses;
            String expected = "table";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Css_ReturnsItself()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.Css("table");
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.Same(expected, actual);
        }

        #endregion

        #region Empty(String text)

        [Fact]
        public void Empty_SetsEmptyText()
        {
            String actual = htmlGrid.Empty("Text").Grid.EmptyText;
            String expected = "Text";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Empty_ReturnsItself()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.Empty("Text");
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.Same(expected, actual);
        }

        #endregion

        #region Named(String name)

        [Fact]
        public void Named_SetsName()
        {
            String actual = htmlGrid.Named("Name").Grid.Name;
            String expected = "Name";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Named_ReturnsItself()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.Named("Name");
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.Same(expected, actual);
        }

        #endregion

        #region WithFooter(String partialViewName)

        [Fact]
        public void WithFooter_SetsFooterPartialViewName()
        {
            String actual = htmlGrid.WithFooter("Partial").Grid.FooterPartialViewName;
            String expected = "Partial";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WithFooter_ReturnsItself()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.WithFooter("Partial");
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.Same(expected, actual);
        }

        #endregion

        #region Pageable(Action<IGridPager<T>> builder)

        [Fact]
        public void Pageable_Builder_DoesNotChangePager()
        {
            IGridPager<GridModel> pager = new GridPager<GridModel>(htmlGrid.Grid);
            htmlGrid.Grid.Pager = pager;

            htmlGrid.Pageable(gridPager => { });

            IGridPager actual = htmlGrid.Grid.Pager;
            IGridPager expected = pager;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void Pageable_Builder_CreatesGridPager()
        {
            htmlGrid.Grid.Pager = null;

            htmlGrid.Pageable(pager => { });

            IGridPager<GridModel> expected = new GridPager<GridModel>(htmlGrid.Grid);
            IGridPager<GridModel> actual = htmlGrid.Grid.Pager;

            Assert.Equal(expected.FirstDisplayPage, actual.FirstDisplayPage);
            Assert.Equal(expected.PartialViewName, actual.PartialViewName);
            Assert.Equal(expected.PagesToDisplay, actual.PagesToDisplay);
            Assert.Equal(expected.ProcessorType, actual.ProcessorType);
            Assert.Equal(expected.CurrentPage, actual.CurrentPage);
            Assert.Equal(expected.RowsPerPage, actual.RowsPerPage);
            Assert.Equal(expected.TotalPages, actual.TotalPages);
            Assert.Equal(expected.TotalRows, actual.TotalRows);
            Assert.Same(expected.Grid, actual.Grid);
        }

        [Fact]
        public void Pageable_Builder_Pager()
        {
            htmlGrid.Grid.Pager = new GridPager<GridModel>(htmlGrid.Grid);
            IGridPager expected = htmlGrid.Grid.Pager;
            Boolean builderCalled = false;

            htmlGrid.Pageable(actual =>
            {
                Assert.Same(expected, actual);
                builderCalled = true;
            });

            Assert.True(builderCalled);
        }

        [Fact]
        public void Pageable_Builder_AddsGridProcessor()
        {
            htmlGrid.Grid.Processors.Clear();

            htmlGrid.Pageable(pager => { });

            Object actual = htmlGrid.Grid.Processors.Single();
            Object expected = htmlGrid.Grid.Pager;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void Pageable_Builder_DoesNotReadGridProcessor()
        {
            htmlGrid.Grid.Processors.Clear();

            htmlGrid.Pageable(pager => { });
            htmlGrid.Pageable(pager => { });

            Object actual = htmlGrid.Grid.Processors.Single();
            Object expected = htmlGrid.Grid.Pager;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void Pageable_Builder_ReturnsItself()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.Pageable(gridPager => { });
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.Same(expected, actual);
        }

        #endregion

        #region Pageable()

        [Fact]
        public void Pageable_DoesNotChangeExistingPager()
        {
            IGridPager<GridModel> pager = new GridPager<GridModel>(htmlGrid.Grid);
            htmlGrid.Grid.Pager = pager;

            htmlGrid.Pageable();

            IGridPager actual = htmlGrid.Grid.Pager;
            IGridPager expected = pager;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void Pageable_CreatesGridPager()
        {
            htmlGrid.Grid.Pager = null;

            htmlGrid.Pageable();

            IGridPager<GridModel> expected = new GridPager<GridModel>(htmlGrid.Grid);
            IGridPager<GridModel> actual = htmlGrid.Grid.Pager;

            Assert.Equal(expected.FirstDisplayPage, actual.FirstDisplayPage);
            Assert.Equal(expected.PartialViewName, actual.PartialViewName);
            Assert.Equal(expected.PagesToDisplay, actual.PagesToDisplay);
            Assert.Equal(expected.ProcessorType, actual.ProcessorType);
            Assert.Equal(expected.CurrentPage, actual.CurrentPage);
            Assert.Equal(expected.RowsPerPage, actual.RowsPerPage);
            Assert.Equal(expected.TotalPages, actual.TotalPages);
            Assert.Equal(expected.TotalRows, actual.TotalRows);
            Assert.Same(expected.Grid, actual.Grid);
        }

        [Fact]
        public void Pageable_AddsGridPagerProcessor()
        {
            htmlGrid.Grid.Processors.Clear();

            htmlGrid.Pageable();

            Object actual = htmlGrid.Grid.Processors.Single();
            Object expected = htmlGrid.Grid.Pager;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void Pageable_DoesNotReaddGridProcessor()
        {
            htmlGrid.Grid.Processors.Clear();

            htmlGrid.Pageable();
            htmlGrid.Pageable();

            Object actual = htmlGrid.Grid.Processors.Single();
            Object expected = htmlGrid.Grid.Pager;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void Pageable_ReturnsItself()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.Pageable();
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.Same(expected, actual);
        }

        #endregion

        #region ToHtmlString()

        [Fact]
        public void ToHtmlString_RendersPartialView()
        {
            IView view = Substitute.For<IView>();
            IViewEngine engine = Substitute.For<IViewEngine>();
            ViewEngineResult result = Substitute.For<ViewEngineResult>(view, engine);
            engine.FindPartialView(Arg.Any<ControllerContext>(), htmlGrid.PartialViewName, Arg.Any<Boolean>()).Returns(result);
            view.When(sub => sub.Render(Arg.Any<ViewContext>(), Arg.Any<TextWriter>())).Do(sub =>
            {
                Assert.Equal(htmlGrid.Grid, sub.Arg<ViewContext>().ViewData.Model);
                sub.Arg<TextWriter>().Write("Rendered");
            });

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(engine);

            String actual = htmlGrid.ToHtmlString();
            String expected = "Rendered";

            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
