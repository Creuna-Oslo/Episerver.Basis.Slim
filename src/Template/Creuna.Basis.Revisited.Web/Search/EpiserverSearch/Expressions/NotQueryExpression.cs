using EPiServer.Search.Queries;
using System;

namespace Creuna.Basis.Revisited.Web.Search.EpiserverSearch.Expressions
{
    public class NotQueryExpression : IQueryExpression
    {
        public IQueryExpression InnerExpression { get; private set; }

        public NotQueryExpression(IQueryExpression innerExpression)
        {
            if (innerExpression == null)
                throw new ArgumentNullException("innerExpression");

            InnerExpression = innerExpression;
        }

        public string GetQueryExpression()
            => "*:* AND NOT " + InnerExpression.GetQueryExpression();
    }
}