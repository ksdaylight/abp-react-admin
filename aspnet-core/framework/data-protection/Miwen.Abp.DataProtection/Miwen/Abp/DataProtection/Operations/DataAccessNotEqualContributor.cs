using System.Linq.Expressions;

namespace Miwen.Abp.DataProtection.Operations;
public class DataAccessNotEqualContributor : IDataAccessOperateContributor
{
    public DataAccessFilterOperate Operate => DataAccessFilterOperate.NotEqual;

    public Expression BuildExpression(Expression left, Expression right)
    {
        return Expression.NotEqual(left, right);
    }
}
