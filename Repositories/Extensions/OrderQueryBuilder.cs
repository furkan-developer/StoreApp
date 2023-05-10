using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Extensions
{
    public static class OrderQueryBuilder
    {
        public static string CreateOrderQuery(string orderByPattern)
        {
            PropertyInfo[] propInfos = typeof(Book).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var orderQueryStringBuilder = new StringBuilder();

            string[] orderByExpressions = orderByPattern.Trim().Split(',');

            foreach (string orderByExpression in orderByExpressions)
            {
                if (string.IsNullOrWhiteSpace(orderByExpression))
                    continue;

                string propNameFromExpression = orderByExpression.Trim().Split(" ")[0];

                PropertyInfo propInfo = propInfos.SingleOrDefault(propInfos => propInfos.Name.Equals(propNameFromExpression, StringComparison.InvariantCultureIgnoreCase));

                if (propInfo is null)
                    continue;

                string orderByKeyword = orderByExpression.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryStringBuilder.Append($"{propInfo.Name.ToString()} {orderByKeyword},");
            }

            // Example: Title descending,Price ascending
            string orderByQuery = orderQueryStringBuilder.ToString().TrimEnd(',');

            return orderByQuery;
        }
    }
}
