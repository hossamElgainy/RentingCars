using Core.Dtos.Logs;

namespace Core.Interfaces.IServices.UsersIServices
{
    public interface ILogService
    {
        Task<LogDetailDto> GetByIdAsync(int id);
        Task<PagedResult<LogDto>> GetAllAsync(LogFilterDto filter, int pageNumber = 1, int pageSize = 10);
        Task<bool> MarkAsSolvedAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<int> CleanOldLogsAsync(int daysToKeep);
        Task<int> CountAsync(LogFilterDto filter = null);
    }
}
