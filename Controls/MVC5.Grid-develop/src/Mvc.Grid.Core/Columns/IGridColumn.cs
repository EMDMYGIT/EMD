using System;
using System.Linq.Expressions;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public interface IGridColumn : IFilterableColumn, ISortableColumn
    {
        String Name { get; set; }
        String Format { get; set; }
        String CssClasses { get; set; }
        Boolean IsEncoded { get; set; }
        IHtmlString Title { get; set; }

        IHtmlString ValueFor(IGridRow<Object> row);
    }

    public interface IGridColumn<T> : IFilterableColumn<T>, ISortableColumn<T>, IGridColumn
    {
        IGrid<T> Grid { get; }
        LambdaExpression Expression { get; }

        Func<T, Object> RenderValue { get; set; }
    }
}
