using System.Collections.Generic;
using System.Threading.Tasks;
using HealthAPI.Model;

namespace HealthAPI.Repositories
{
    public interface ILapRecordRepository
    {
        Task<IList<LapRecord>> GetAll();
        Task Save(LapRecord record);
    }
}
