﻿using Datalist.Tests.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Datalist.Tests.Unit
{
    public class MvcDatalistOfTTests
    {
        private Dictionary<String, String> row;
        private TestDatalist<TestModel> datalist;

        public MvcDatalistOfTTests()
        {
            row = new Dictionary<String, String>();
            datalist = new TestDatalist<TestModel>();
            datalist.Filter.Page = 0;

            for (Int32 i = 0; i < 200; i++)
                datalist.Models.Add(new TestModel
                {
                    Id = i + "I",
                    Count = i + 10,
                    Value = i + "V",
                    ParentId = "1000",
                    Date = new DateTime(2014, 12, 10).AddDays(i)
                });
        }

        #region AttributedProperties

        [Fact]
        public void AttributedProperties_GetsOrderedProperties()
        {
            IEnumerable<PropertyInfo> actual = datalist.AttributedProperties;
            IEnumerable<PropertyInfo> expected = typeof(TestModel).GetProperties()
                .Where(property => property.GetCustomAttribute<DatalistColumnAttribute>(false) != null)
                .OrderBy(property => property.GetCustomAttribute<DatalistColumnAttribute>(false).Position);

            Assert.Equal(expected, actual);
        }

        #endregion

        #region MvcDatalist()

        [Fact]
        public void MvcDatalist_AddsColumns()
        {
            List<DatalistColumn> columns = new List<DatalistColumn>();
            columns.Add(new DatalistColumn("Id", null) { Hidden = true });
            columns.Add(new DatalistColumn("Value", null) { Hidden = false });
            columns.Add(new DatalistColumn("Date", "Date") { Hidden = false });
            columns.Add(new DatalistColumn("Count", "Value") { Hidden = false });

            IEnumerator<DatalistColumn> expected = columns.GetEnumerator();
            IEnumerator<DatalistColumn> actual = datalist.Columns.GetEnumerator();

            while (expected.MoveNext() | actual.MoveNext())
            {
                Assert.Equal(expected.Current.Key, actual.Current.Key);
                Assert.Equal(expected.Current.Header, actual.Current.Header);
                Assert.Equal(expected.Current.Hidden, actual.Current.Hidden);
                Assert.Equal(expected.Current.CssClass, actual.Current.CssClass);
            }
        }

        #endregion

        #region GetColumnKey(PropertyInfo property)

        [Fact]
        public void GetColumnKey_NullProperty_Throws()
        {
            ArgumentNullException actual = Assert.Throws<ArgumentNullException>(() => datalist.GetColumnKey(null));

            Assert.Equal("property", actual.ParamName);
        }

        [Fact]
        public void GetColumnKey_ReturnsPropertyName()
        {
            PropertyInfo property = typeof(TestModel).GetProperty("Count");

            String actual = datalist.GetColumnKey(property);
            String expected = property.Name;

            Assert.Equal(expected, actual);
        }

        #endregion

        #region GetColumnHeader(PropertyInfo property)

        [Fact]
        public void GetColumnHeader_NullProperty_ReturnsNull()
        {
            Assert.Null(datalist.GetColumnHeader(null));
        }

        [Fact]
        public void GetColumnHeader_NoDisplayName_ReturnsNull()
        {
            PropertyInfo property = typeof(TestModel).GetProperty("Value");

            Assert.Null(datalist.GetColumnHeader(property));
        }

        [Fact]
        public void GetColumnHeader_ReturnsDisplayName()
        {
            PropertyInfo property = typeof(TestModel).GetProperty("Date");

            String expected = property.GetCustomAttribute<DisplayAttribute>().Name;
            String actual = datalist.GetColumnHeader(property);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetColumnHeader_ReturnsDisplayShortName()
        {
            PropertyInfo property = typeof(TestModel).GetProperty("Count");

            String expected = property.GetCustomAttribute<DisplayAttribute>().ShortName;
            String actual = datalist.GetColumnHeader(property);

            Assert.Equal(expected, actual);
        }

        #endregion

        #region GetColumnCssClass(PropertyInfo property)

        [Fact]
        public void GetColumnCssClass_ReturnsNull()
        {
            Assert.Null(datalist.GetColumnCssClass(null));
        }

        #endregion

        #region GetData()

        [Fact]
        public void GetData_FiltersByIds()
        {
            datalist.Filter.Ids.Add("9I");
            datalist.Filter.Ids.Add("15I");
            datalist.Filter.Sort = "Count";
            datalist.Filter.Search = "Term";
            datalist.Filter.Selected.Add("17I");
            datalist.Filter.AdditionalFilters.Add("Value", "5V");

            DatalistData actual = datalist.GetData();

            Assert.Equal(new DateTime(2014, 12, 19).ToString("d"), actual.Rows[0]["Date"]);
            Assert.Equal("9V", actual.Rows[0][MvcDatalist.AcKey]);
            Assert.Equal("9I", actual.Rows[0][MvcDatalist.IdKey]);
            Assert.Equal("9V", actual.Rows[0]["Value"]);
            Assert.Equal("19", actual.Rows[0]["Count"]);

            Assert.Equal(new DateTime(2014, 12, 25).ToString("d"), actual.Rows[1]["Date"]);
            Assert.Equal("15V", actual.Rows[1][MvcDatalist.AcKey]);
            Assert.Equal("15I", actual.Rows[1][MvcDatalist.IdKey]);
            Assert.Equal("15V", actual.Rows[1]["Value"]);
            Assert.Equal("25", actual.Rows[1]["Count"]);

            Assert.Equal(datalist.Columns, actual.Columns);
            Assert.Equal(2, actual.FilteredRows);
            Assert.Equal(2, actual.Rows.Count);
        }

        [Fact]
        public void GetData_FiltersByCheckIds()
        {
            datalist.Filter.Sort = "Count";
            datalist.Filter.CheckIds.Add("9I");
            datalist.Filter.CheckIds.Add("15I");
            datalist.Filter.AdditionalFilters.Add("ParentId", "1000");

            DatalistData actual = datalist.GetData();

            Assert.Equal(new DateTime(2014, 12, 19).ToString("d"), actual.Rows[0]["Date"]);
            Assert.Equal("9V", actual.Rows[0][MvcDatalist.AcKey]);
            Assert.Equal("9I", actual.Rows[0][MvcDatalist.IdKey]);
            Assert.Equal("9V", actual.Rows[0]["Value"]);
            Assert.Equal("19", actual.Rows[0]["Count"]);

            Assert.Equal(new DateTime(2014, 12, 25).ToString("d"), actual.Rows[1]["Date"]);
            Assert.Equal("15V", actual.Rows[1][MvcDatalist.AcKey]);
            Assert.Equal("15I", actual.Rows[1][MvcDatalist.IdKey]);
            Assert.Equal("15V", actual.Rows[1]["Value"]);
            Assert.Equal("25", actual.Rows[1]["Count"]);

            Assert.Equal(datalist.Columns, actual.Columns);
            Assert.Equal(2, actual.FilteredRows);
            Assert.Equal(2, actual.Rows.Count);
        }

        [Fact]
        public void GetData_FiltersByNotSelected()
        {
            datalist.Filter.Sort = "Count";
            datalist.Filter.Search = "133V";
            datalist.Filter.Selected.Add("15I");
            datalist.Filter.Selected.Add("125I");

            datalist.GetData();

            DatalistData actual = datalist.GetData();

            Assert.Equal(new DateTime(2014, 12, 25).ToString("d"), actual.Rows[0]["Date"]);
            Assert.Equal("15V", actual.Rows[0][MvcDatalist.AcKey]);
            Assert.Equal("15I", actual.Rows[0][MvcDatalist.IdKey]);
            Assert.Equal("15V", actual.Rows[0]["Value"]);
            Assert.Equal("25", actual.Rows[0]["Count"]);

            Assert.Equal(new DateTime(2015, 4, 14).ToString("d"), actual.Rows[1]["Date"]);
            Assert.Equal("125V", actual.Rows[1][MvcDatalist.AcKey]);
            Assert.Equal("125I", actual.Rows[1][MvcDatalist.IdKey]);
            Assert.Equal("125V", actual.Rows[1]["Value"]);
            Assert.Equal("135", actual.Rows[1]["Count"]);

            Assert.Equal(new DateTime(2015, 4, 22).ToString("d"), actual.Rows[2]["Date"]);
            Assert.Equal("133V", actual.Rows[2][MvcDatalist.AcKey]);
            Assert.Equal("133I", actual.Rows[2][MvcDatalist.IdKey]);
            Assert.Equal("133V", actual.Rows[2]["Value"]);
            Assert.Equal("143", actual.Rows[2]["Count"]);

            Assert.Equal(datalist.Columns, actual.Columns);
            Assert.Equal(1, actual.FilteredRows);
            Assert.Equal(3, actual.Rows.Count);
        }

        [Fact]
        public void GetData_FiltersByAdditionalFilters()
        {
            datalist.Filter.Search = "6V";
            datalist.Filter.AdditionalFilters.Add("Count", 16);

            datalist.GetData();

            DatalistData actual = datalist.GetData();

            Assert.Equal(new DateTime(2014, 12, 16).ToString("d"), actual.Rows[0]["Date"]);
            Assert.Equal("6V", actual.Rows[0][MvcDatalist.AcKey]);
            Assert.Equal("6I", actual.Rows[0][MvcDatalist.IdKey]);
            Assert.Equal("6V", actual.Rows[0]["Value"]);
            Assert.Equal("16", actual.Rows[0]["Count"]);

            Assert.Equal(datalist.Columns, actual.Columns);
            Assert.Equal(1, actual.FilteredRows);
            Assert.Single(actual.Rows);
        }

        [Fact]
        public void GetData_FiltersBySearch()
        {
            datalist.Filter.Search = "33V";
            datalist.Filter.Sort = "Count";

            datalist.GetData();

            DatalistData actual = datalist.GetData();

            Assert.Equal(new DateTime(2015, 1, 12).ToString("d"), actual.Rows[0]["Date"]);
            Assert.Equal("33V", actual.Rows[0][MvcDatalist.AcKey]);
            Assert.Equal("33I", actual.Rows[0][MvcDatalist.IdKey]);
            Assert.Equal("33V", actual.Rows[0]["Value"]);
            Assert.Equal("43", actual.Rows[0]["Count"]);

            Assert.Equal(new DateTime(2015, 4, 22).ToString("d"), actual.Rows[1]["Date"]);
            Assert.Equal("133V", actual.Rows[1][MvcDatalist.AcKey]);
            Assert.Equal("133I", actual.Rows[1][MvcDatalist.IdKey]);
            Assert.Equal("133V", actual.Rows[1]["Value"]);
            Assert.Equal("143", actual.Rows[1]["Count"]);

            Assert.Equal(datalist.Columns, actual.Columns);
            Assert.Equal(2, actual.FilteredRows);
            Assert.Equal(2, actual.Rows.Count);
        }

        [Fact]
        public void GetData_Sorts()
        {
            datalist.Filter.Order = DatalistSortOrder.Asc;
            datalist.Filter.Search = "55V";
            datalist.Filter.Sort = "Count";

            datalist.GetData();

            DatalistData actual = datalist.GetData();

            Assert.Equal(new DateTime(2015, 2, 3).ToString("d"), actual.Rows[0]["Date"]);
            Assert.Equal("55V", actual.Rows[0][MvcDatalist.AcKey]);
            Assert.Equal("55I", actual.Rows[0][MvcDatalist.IdKey]);
            Assert.Equal("55V", actual.Rows[0]["Value"]);
            Assert.Equal("65", actual.Rows[0]["Count"]);

            Assert.Equal(new DateTime(2015, 5, 14).ToString("d"), actual.Rows[1]["Date"]);
            Assert.Equal("155V", actual.Rows[1][MvcDatalist.AcKey]);
            Assert.Equal("155I", actual.Rows[1][MvcDatalist.IdKey]);
            Assert.Equal("155V", actual.Rows[1]["Value"]);
            Assert.Equal("165", actual.Rows[1]["Count"]);
        }

        #endregion

        #region FilterBySearch(IQueryable<T> models)

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void FilterBySearch_SkipsEmptySearch(String search)
        {
            datalist.Filter.Search = search;

            IQueryable<TestModel> actual = datalist.FilterBySearch(datalist.GetModels());
            IQueryable<TestModel> expected = datalist.GetModels();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FilterBySearch_DoesNotFilterNotExistingProperties()
        {
            datalist.Columns.Clear();
            datalist.Filter.Search = "1";
            datalist.Columns.Add(new DatalistColumn("Test", "Test"));

            IQueryable<TestModel> actual = datalist.FilterBySearch(datalist.GetModels());
            IQueryable<TestModel> expected = datalist.GetModels();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FilterBySearch_UsesContainsSearch()
        {
            datalist.Filter.Search = "1";

            IQueryable<TestModel> expected = datalist.GetModels().Where(model => model.Id.Contains("1"));
            IQueryable<TestModel> actual = datalist.FilterBySearch(datalist.GetModels());

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FilterBySearch_DoesNotFilterNonStringProperties()
        {
            datalist.Columns.Clear();
            datalist.Filter.Search = "1";
            datalist.Columns.Add(new DatalistColumn("Count", null));

            IQueryable<TestModel> actual = datalist.FilterBySearch(datalist.GetModels());
            IQueryable<TestModel> expected = datalist.GetModels();

            Assert.Equal(expected, actual);
        }

        #endregion

        #region FilterByAdditionalFilters(IQueryable<T> models)

        [Fact]
        public void FilterByAdditionalFilters_SkipsNullValues()
        {
            datalist.Filter.AdditionalFilters.Add("Id", null);

            IQueryable<TestModel> actual = datalist.FilterByAdditionalFilters(datalist.GetModels());
            IQueryable<TestModel> expected = datalist.GetModels();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FilterByAdditionalFilters_Filters()
        {
            datalist.Filter.AdditionalFilters.Add("Id", "9I");
            datalist.Filter.AdditionalFilters.Add("Count", new[] { 19, 30 });
            datalist.Filter.AdditionalFilters.Add("Date", new DateTime(2014, 12, 19));

            IQueryable<TestModel> actual = datalist.FilterByAdditionalFilters(datalist.GetModels());
            IQueryable<TestModel> expected = datalist.GetModels().Where(model =>
                model.Id == "9I" && model.Count == 19 && model.Date == new DateTime(2014, 12, 19));

            Assert.Equal(expected, actual);
        }

        #endregion

        #region FilterByIds(IQueryable<T> models, IList<String> ids)

        [Fact]
        public void FilterByIds_NoIdProperty_Throws()
        {
            TestDatalist<NoIdModel> testDatalist = new TestDatalist<NoIdModel>();

            DatalistException exception = Assert.Throws<DatalistException>(() => testDatalist.FilterByIds(null, null));

            String expected = $"'{typeof(NoIdModel).Name}' type does not have key or property named 'Id', required for automatic id filtering.";
            String actual = exception.Message;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FilterByIds_String()
        {
            List<String> ids = new List<String> { "9I", "10I" };

            IQueryable<TestModel> actual = datalist.FilterByIds(datalist.GetModels(), ids);
            IQueryable<TestModel> expected = datalist.GetModels().Where(model => ids.Contains(model.Id));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FilterByIds_Int32Key()
        {
            TestDatalist<Int32Model> testDatalist = new TestDatalist<Int32Model>();
            for (Int32 i = 0; i < 20; i++) testDatalist.Models.Add(new Int32Model { Value = i });

            IQueryable<Int32Model> actual = testDatalist.FilterByIds(testDatalist.GetModels(), new List<String> { "9", "10" });
            IQueryable<Int32Model> expected = testDatalist.GetModels().Where(model => new[] { 9, 10 }.Contains(model.Value));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FilterByIds_Int64Key()
        {
            TestDatalist<Int64Model> testDatalist = new TestDatalist<Int64Model>();
            for (Int64 i = 0; i < 20; i++) testDatalist.Models.Add(new Int64Model { Value = i });

            IQueryable<Int64Model> actual = testDatalist.FilterByIds(testDatalist.GetModels(), new List<String> { "9", "10" });
            IQueryable<Int64Model> expected = testDatalist.GetModels().Where(model => new[] { 9L, 10L }.Contains(model.Value));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FilterByIds_NotSupportedIdType_Throws()
        {
            DatalistException exception = Assert.Throws<DatalistException>(() => new TestDatalist<GuidModel>().FilterByIds(null, new String[0]));

            String expected = $"'{typeof(GuidModel).Name}.Id' property type has to be a string, int or a long.";
            String actual = exception.Message;

            Assert.Equal(expected, actual);
        }

        #endregion

        #region FilterByNotIds(IQueryable<T> models, IList<String> ids)

        [Fact]
        public void FilterByNotIds_NoIdProperty_Throws()
        {
            TestDatalist<NoIdModel> testDatalist = new TestDatalist<NoIdModel>();

            DatalistException exception = Assert.Throws<DatalistException>(() => testDatalist.FilterByNotIds(null, null));

            String expected = $"'{typeof(NoIdModel).Name}' type does not have key or property named 'Id', required for automatic id filtering.";
            String actual = exception.Message;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FilterByNotIds_String()
        {
            List<String> ids = new List<String> { "9I", "10I" };

            IQueryable<TestModel> actual = datalist.FilterByNotIds(datalist.GetModels(), ids);
            IQueryable<TestModel> expected = datalist.GetModels().Where(model => !ids.Contains(model.Id));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FilterByNotIds_Int32Key()
        {
            TestDatalist<Int32Model> testDatalist = new TestDatalist<Int32Model>();
            for (Int32 i = 0; i < 20; i++) testDatalist.Models.Add(new Int32Model { Value = i });

            IQueryable<Int32Model> actual = testDatalist.FilterByNotIds(testDatalist.GetModels(), new List<String> { "9", "10" });
            IQueryable<Int32Model> expected = testDatalist.GetModels().Where(model => !new[] { 9, 10 }.Contains(model.Value));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FilterByNotIds_Int64Key()
        {
            TestDatalist<Int64Model> testDatalist = new TestDatalist<Int64Model>();
            for (Int64 i = Int32.MaxValue; i < 20; i++) testDatalist.Models.Add(new Int64Model { Value = i });

            IQueryable<Int64Model> actual = testDatalist.FilterByNotIds(testDatalist.GetModels(), new List<String> { "9", "10" });
            IQueryable<Int64Model> expected = testDatalist.GetModels().Where(model => !new[] { 9L, 10L }.Contains(model.Value));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FilterByNotIds_NotSupportedIdType_Throws()
        {
            DatalistException exception = Assert.Throws<DatalistException>(() => new TestDatalist<GuidModel>().FilterByNotIds(null, new String[0]));

            String expected = $"'{typeof(GuidModel).Name}.Id' property type has to be a string, int or a long.";
            String actual = exception.Message;

            Assert.Equal(expected, actual);
        }

        #endregion

        #region Sort(IQueryable<T> models)

        [Fact]
        public void Sort_ByColumn()
        {
            datalist.Filter.Sort = "Count";

            IQueryable<TestModel> expected = datalist.GetModels().OrderBy(model => model.Count);
            IQueryable<TestModel> actual = datalist.Sort(datalist.GetModels());

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Sort_NoSortColumns(String column)
        {
            datalist.Columns.Clear();
            datalist.Filter.Sort = column;

            IQueryable<TestModel> expected = datalist.GetModels();
            IQueryable<TestModel> actual = datalist.Sort(datalist.GetModels());

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Sort_NoSortOrderedColumns(String column)
        {
            datalist.Columns.Clear();
            datalist.Filter.Sort = column;

            IQueryable<TestModel> expected = datalist.GetModels().OrderByDescending(model => model.Id);
            IQueryable<TestModel> actual = datalist.Sort(datalist.GetModels().OrderByDescending(model => model.Id));

            Assert.Equal(expected, actual);
        }

        #endregion

        #region Page(IQueryable<T> models)

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(0, 1, 1)]
        [InlineData(0, 5, 5)]
        [InlineData(1, 0, 0)]
        [InlineData(1, 1, 1)]
        [InlineData(1, 5, 5)]
        [InlineData(19, 0, 0)]
        [InlineData(19, 1, 1)]
        [InlineData(20, 0, 0)]
        [InlineData(20, 1, 1)]
        [InlineData(20, 5, 5)]
        [InlineData(199, 0, 0)]
        [InlineData(199, 1, 1)]
        [InlineData(199, 5, 5)]
        [InlineData(200, 0, 0)]
        [InlineData(0, 98, 98)]
        [InlineData(0, 99, 99)]
        [InlineData(0, 100, 99)]
        [InlineData(0, 150, 99)]
        public void Page_Rows(Int32 page, Int32 rows, Int32 takeRows)
        {
            datalist.Filter.Page = page;
            datalist.Filter.Rows = rows;

            IQueryable<TestModel> expected = datalist.GetModels().Skip(page * rows).Take(takeRows);
            IQueryable<TestModel> actual = datalist.Page(datalist.GetModels());

            Assert.Equal(expected, actual);
        }

        #endregion

        #region FormDatalistData(IQueryable<T> filtered, IQueryable<T> selected, IQueryable<T> notSelected)

        [Fact]
        public void FormDatalistData_FilteredRows()
        {
            Int32 actual = datalist.FormDatalistData(datalist.GetModels(), new[] { new TestModel() }.AsQueryable(), datalist.GetModels().Take(1)).FilteredRows;
            Int32 expected = datalist.GetModels().Count();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FormDatalistData_Columns()
        {
            Object actual = datalist.FormDatalistData(datalist.GetModels(), new[] { new TestModel() }.AsQueryable(), datalist.GetModels()).Columns;
            Object expected = datalist.Columns;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void FormDatalistData_Rows()
        {
            IQueryable<TestModel> selected = new TestModel[0].AsQueryable();
            IQueryable<TestModel> notSelected = datalist.GetModels().Skip(6).Take(3);

            IEnumerator<Dictionary<String, String>> actual = datalist.FormDatalistData(datalist.GetModels(), selected, notSelected).Rows.GetEnumerator();
            IEnumerator<Dictionary<String, String>> expected = new List<Dictionary<String, String>>
            {
                new Dictionary<String, String>
                {
                    [MvcDatalist.IdKey] = "6I",
                    [MvcDatalist.AcKey] = "6V",
                    ["Id"] = "6I",
                    ["Value"] = "6V",
                    ["Date"] = new DateTime(2014, 12, 16).ToString("d"),
                    ["Count"] = "16"
                },
                new Dictionary<String, String>
                {
                    [MvcDatalist.IdKey] = "7I",
                    [MvcDatalist.AcKey] = "7V",
                    ["Id"] = "7I",
                    ["Value"] = "7V",
                    ["Date"] = new DateTime(2014, 12, 17).ToString("d"),
                    ["Count"] = "17"
                },
                new Dictionary<String, String>
                {
                    [MvcDatalist.IdKey] = "8I",
                    [MvcDatalist.AcKey] = "8V",
                    ["Id"] = "8I",
                    ["Value"] = "8V",
                    ["Date"] = new DateTime(2014, 12, 18).ToString("d"),
                    ["Count"] = "18"
                }
            }.GetEnumerator();

            while (expected.MoveNext() | actual.MoveNext())
            {
                Assert.Equal(expected.Current.Values, actual.Current.Values);
                Assert.Equal(expected.Current.Keys, actual.Current.Keys);
            }
        }

        #endregion

        #region AddId(Dictionary<String, String> row, T model)

        [Fact]
        public void AddId_FromFunction()
        {
            TestDatalist<NoIdModel> testDatalist = new TestDatalist<NoIdModel>();
            testDatalist.Id = (model) => "1";

            testDatalist.AddId(row, new NoIdModel());

            KeyValuePair<String, String> actual = row.Single();

            Assert.Equal(MvcDatalist.IdKey, actual.Key);
            Assert.Equal("1", actual.Value);
        }

        [Fact]
        public void AddId_EmptyValues()
        {
            TestDatalist<NoIdModel> testDatalist = new TestDatalist<NoIdModel>();

            testDatalist.AddId(row, new NoIdModel());

            KeyValuePair<String, String> actual = row.Single();

            Assert.Equal(MvcDatalist.IdKey, actual.Key);
            Assert.Null(actual.Value);
        }

        [Fact]
        public void AddId_Values()
        {
            datalist.AddId(row, new TestModel { Id = "Test" });

            KeyValuePair<String, String> actual = row.Single();

            Assert.Equal(MvcDatalist.IdKey, actual.Key);
            Assert.Equal("Test", actual.Value);
        }

        #endregion

        #region AddAutocomplete(Dictionary<String, String> row, T model)

        [Fact]
        public void AddAutocomplete_FromFunction()
        {
            datalist.Autocomplete = (model) => "Auto";

            datalist.AddAutocomplete(row, new TestModel { Value = "Test" });

            KeyValuePair<String, String> actual = row.Single();

            Assert.Equal(MvcDatalist.AcKey, actual.Key);
            Assert.Equal("Auto", actual.Value);
        }

        [Fact]
        public void AddAutocomplete_EmptyValues()
        {
            datalist.Columns.Clear();

            datalist.AddAutocomplete(row, new TestModel());

            KeyValuePair<String, String> actual = row.Single();

            Assert.Equal(MvcDatalist.AcKey, actual.Key);
            Assert.Null(actual.Value);
        }

        [Fact]
        public void AddAutocomplete_Values()
        {
            datalist.AddAutocomplete(row, new TestModel { Value = "Test" });

            KeyValuePair<String, String> actual = row.Single();

            Assert.Equal(MvcDatalist.AcKey, actual.Key);
            Assert.Equal("Test", actual.Value);
        }

        #endregion

        #region AddData(Dictionary<String, String> row, T model)

        [Fact]
        public void AddData_EmptyValues()
        {
            datalist.Columns.Clear();
            datalist.Columns.Add(new DatalistColumn("Test", null));

            datalist.AddData(row, new TestModel { Value = "Test", Date = DateTime.Now.Date, Count = 4 });

            Assert.Equal(new String[] { null }, row.Values);
            Assert.Equal(new[] { "Test" }, row.Keys);
        }

        [Fact]
        public void AddData_Values()
        {
            datalist.AddData(row, new TestModel { Value = "Test", Date = DateTime.Now.Date, Count = 4 });

            Assert.Equal(new[] { null, "Test", DateTime.Now.Date.ToString("d"), "4" }, row.Values);
            Assert.Equal(datalist.Columns.Select(column => column.Key), row.Keys);
        }

        #endregion
    }
}
