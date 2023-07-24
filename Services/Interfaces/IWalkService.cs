using WalksInfoViberBot.Models.DTOs;

namespace WalksInfoViberBot.Services.Interfaces
{
    public interface IWalkService
    {
        Task<IEnumerable<WalkDTO>> Get10LongestWalksAsync(string imei);
        Task<WalksGeneralInfoDTO> GetWalksGeneralInfoAsync(string imei);
    }
}
