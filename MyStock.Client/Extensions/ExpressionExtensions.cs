using System.Linq.Expressions;

namespace MyStock.Extensions;

public static class ExpressionExtensions
{
    public static string ExtractPropertyName<T>(this Expression<Func<T>> propertyExpression)
    {
        var lambdaExpression = (LambdaExpression)propertyExpression;
        var body = lambdaExpression.Body;
        var unaryExpression = body as UnaryExpression;
        var memberExpression = unaryExpression == null
            ? (MemberExpression)body
            : (MemberExpression)unaryExpression.Operand;
        return memberExpression.Member.Name;
    }
}
