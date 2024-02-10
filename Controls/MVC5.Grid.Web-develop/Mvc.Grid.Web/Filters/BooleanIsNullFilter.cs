using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid.Web.Filters
{
    public class BooleanIsNullFilter : BaseGridFilter
    {
        public override Expression Apply(Expression expression)
        {
            return Expression.Equal(expression, Expression.Constant(null));
        }
    }
}
