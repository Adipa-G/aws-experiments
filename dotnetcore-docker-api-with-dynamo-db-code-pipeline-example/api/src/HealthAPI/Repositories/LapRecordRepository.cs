using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using HealthAPI.Model;

namespace HealthAPI.Repositories
{
    public class LapRecordRepository : ILapRecordRepository
    {
        private readonly IDynamoDBContext _context;

        public LapRecordRepository(IDynamoDBContext context)
        {
            _context = context;
        }

        public async Task<IList<LapRecord>> GetAll()
        {
            return await _context.ScanAsync<LapRecord>(new List<ScanCondition>())
                .GetNextSetAsync();
        }

        public Task Save(LapRecord record)
        {
            return _context.SaveAsync(record);
        }
    }
}
