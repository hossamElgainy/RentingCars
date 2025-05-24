using Core.Interfaces.Specification;
using System.Linq.Expressions;
namespace Infrastructure.Data.Specification.Base
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public List<Expression<Func<T, bool>>> Criteria { get; set; } = new List<Expression<Func<T, bool>>> {e=>true }; // Initialized!
        public List<Expression<Func<T, object>>> Includes { get; set; } = new();
        public List<string> IncludeStrings { get; set; } = new();
        public Expression<Func<T, object>>? OrderBy { get; set; }=null;
        public Expression<Func<T, object>>? GroupBy { get; set; }=null;
        public Expression<Func<T, object>>? OrderByDesc { get; set; } = null; // Initialized!
        public int Skip { get; set; }
        public int Take { get; set; }

        public BaseSpecification() { } // Parameterless constructor

        public BaseSpecification(List<Expression<Func<T, bool>>> criteria)
        {
            Criteria = criteria;
        }
        public BaseSpecification(Expression<Func<T, object>> groupBy)
        {
            GroupBy = groupBy;
        }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        public void AddOrderBy(Expression<Func<T, object>> exp)
        {
            OrderBy = exp;
        }

        public void AddOrderByDsc(Expression<Func<T, object>> exp)
        {
            OrderByDesc = exp;
        }
        public void ApplyPagination(int skip, int take)
        {
            if(skip>0 && take>0)
            {
                Skip = skip;
                Take = take;
            }
        }
    }
}
