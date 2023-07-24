using WalksInfoViberBot.Data.Entities;

namespace WalksInfoViberBot.Repositories.Interfaces
{
    public interface IWalkRepository
    {
        Task<IEnumerable<WalkEntity>> Get10LongestWalksAsync(string imei);
        Task<WalksGeneralInfoEntity> GetWalksGeneralInfoAsync(string imei);
    }
}
