using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Composable.Hypermedia.Linq.Expressions
{
    public static class ExpressionValueExtractor
    {
        public static object ExtractValue(this Expression @this)
        {
            switch (@this.NodeType)
            {
                case ExpressionType.Constant:
                    return ((ConstantExpression)@this).Value;
                case ExpressionType.MemberAccess:
                    {
                        var me = (MemberExpression)@this;
                        var obj = (me.Expression != null ? ExtractValue(me.Expression) : null);
                        var fieldInfo = me.Member as FieldInfo;
                        if (fieldInfo != null)
                            return fieldInfo.GetValue(obj);
                        var propertyInfo = me.Member as PropertyInfo;
                        if (propertyInfo != null)
                            return propertyInfo.GetValue(obj, null);
                        throw new UnsupportedExpressionException();
                    }
                case ExpressionType.Convert:
                    {
                        var ue = (UnaryExpression)@this;
                        var operand = ExtractValue(ue.Operand);
                        if (ue.Type.IsInstanceOfType(operand))
                        {
                            return operand;
                        }
                        throw new UnsupportedExpressionException();
                    }
                default:
                    throw new UnsupportedExpressionException();
            }
        } 
    }

    internal class UnsupportedExpressionException : Exception {}
}