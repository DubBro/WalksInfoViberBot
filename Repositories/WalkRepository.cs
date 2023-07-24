using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WalksInfoViberBot.Data;
using WalksInfoViberBot.Data.Entities;
using WalksInfoViberBot.Exceptions;
using WalksInfoViberBot.Repositories.Interfaces;

namespace WalksInfoViberBot.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public WalkRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<WalkEntity>> Get10LongestWalksAsync(string imei)
        {
            var result = await _dbContext.Walks.FromSqlInterpolated($"EXECUTE dbo.Select10LongestWalks {imei}").ToListAsync();

            if (result.IsNullOrEmpty())
            {
                throw new BusinessException($"No information by IMEI {imei} was received.");
            }

            return result;
        }

        public async Task<WalksGeneralInfoEntity> GetWalksGeneralInfoAsync(string imei)
        {
            var result = await _dbContext.WalksGeneralInfos.FromSqlInterpolated($"EXECUTE dbo.SelectWalksGeneralInfo {imei}").ToListAsync();

            if (result.IsNullOrEmpty() || result[0].TotalCount == 0)
            {
                throw new BusinessException($"No information by IMEI {imei} was received.");
            }

            return result.Single();
        }
    }
}
