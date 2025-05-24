
using Core.Dtos.Logs;
using Core.Interfaces.IServices.UsersIServices;
using Core.Interfaces.Repositories;
using Core.Interfaces.Specifications;
using Domain.DomaimModels;

namespace Infrastructure.Services.SystemServices
{
    public class LogService : ILogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LogDetailDto> GetByIdAsync(int id)
        {
            var log = await _unitOfWork.Repository<LogModel>().GetByIdAsync(id);
            if (log == null)
            {
                return null;
            }

            return new LogDetailDto
            {
                Exception = log.Exception,
                Id = log.Id,
                Level = log.Level,
                Message = log.Message,
                Solved = log.Solved,
                Timestamp = log.TimeStamp
            };


        }

        public async Task<PagedResult<LogDto>> GetAllAsync(LogFilterDto filter, int pageNumber = 1, int pageSize = 10)
        {
            var logs = await _unitOfWork.Repository<LogModel>().GetAllWithSpecAsync(new GetAllLogsSpec(filter, pageNumber, pageSize));


            // Paginate
            var totalCount = await _unitOfWork.Repository<LogModel>().CountAsync();

            var mappedLogs = logs.Select(z => new LogDto
            {
                Id = z.Id,
                Message = z.Message,
                Exception = z.Exception,
                Level = z.Level,
                Timestamp = z.TimeStamp,
                Solved = z.Solved,
            }).ToList(); ;
            return new PagedResult<LogDto>(mappedLogs,totalCount,pageNumber,pageSize);
         
        }


        public async Task<bool> MarkAsSolvedAsync(int id)
        {
            var log = await _unitOfWork.Repository<LogModel>().GetByIdAsync(id);
            if (log == null)
            {
                return false;
            }

            log.Solved = true;


            await _unitOfWork.Complete();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var log = await _unitOfWork.Repository<LogModel>().GetByIdAsync(id);
            if (log == null)
            {
                return false;
            }

            _unitOfWork.Repository<LogModel>().Delete(log);
            await _unitOfWork.Complete();
            return true;
        }

        public async Task<int> CleanOldLogsAsync(int daysToKeep)
        {
            var cutoffDate = DateTime.UtcNow.AddDays(-daysToKeep);
            var oldLogs = await _unitOfWork.Repository<LogModel>().GetAllByProAsync(l => l.TimeStamp < cutoffDate && l.Solved);

            var count = oldLogs.Count;

            if (count > 0)
            {
                _unitOfWork.Repository<LogModel>().DeleteRange(oldLogs);
                await _unitOfWork.Complete();
            }

            return count;
        }

        public async Task<int> CountAsync(LogFilterDto filter = null)
        {
            var logs = await _unitOfWork.Repository<LogModel>().GetAllWithSpecAsync(new GetAllLogsSpec(filter, 1, 1000000));


            return logs.Count();
        }
    }
}