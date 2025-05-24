using System.Linq.Expressions;

namespace Core.Interfaces.Specification
{
    public interface ISpecification<T>
    {
        public List<Expression<Func<T, bool>>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; }
        public List< string> IncludeStrings { get; }
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
         Expression<Func<T, object>>? GroupBy { get; set; } // Initialized!

        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
