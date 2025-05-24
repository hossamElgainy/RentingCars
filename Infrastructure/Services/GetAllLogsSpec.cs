using Core.Dtos.Logs;
using Core.Interfaces.Specification;
using Domain.DomaimModels;
using Infrastructure.Data.Specification.Base;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Services
{
    public class GetAllLogsSpec : BaseSpecification<LogModel>
    {
        public GetAllLogsSpec(LogFilterDto filter, int pageNumber, int pageSize)
        {
            // Apply filters
            if (!string.IsNullOrEmpty(filter.Level))
            {
                Criteria.Add(l => l.Level == filter.Level);
            }
            if (filter.Solved.HasValue)
            {
                Criteria.Add(l => l.Solved == filter.Solved.Value);
            }
            if (filter.FromDate.HasValue)
            {
                Criteria.Add(l => l.TimeStamp >= filter.FromDate.Value);
            }
            if (filter.ToDate.HasValue)
            {
                Criteria.Add(l => l.TimeStamp <= filter.ToDate.Value);
            }

            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                Criteria.Add(l =>
                    l.Message.Contains(filter.SearchTerm) ||
                    (l.Exception != null && l.Exception.Contains(filter.SearchTerm)));
            }

            // Order by most recent first
            OrderByDesc = l => l.TimeStamp;
            ApplyPagination(pageNumber, pageSize);
        }
    }
}