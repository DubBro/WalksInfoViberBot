using WalksInfoViberBot.Models.DTOs;
using WalksInfoViberBot.Repositories.Interfaces;
using WalksInfoViberBot.Services.Interfaces;

namespace WalksInfoViberBot.Services
{
    public class WalkService : IWalkService
    {
        private readonly IWalkRepository _walkRepository;
        private readonly ILogger<WalkService> _logger;

        public WalkService(IWalkRepository walkRepository, ILogger<WalkService> logger)
        {
            _walkRepository = walkRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<WalkDTO>> Get10LongestWalksAsync(string imei)
        {
            ValidationService.ValidateImei(imei);

            var data = await _walkRepository.Get10LongestWalksAsync(imei);

            var result = new List<WalkDTO>();
            int i = 1;
            foreach (var item in data)
            {
                result.Add(new WalkDTO
                {
                    Number = i,
                    Distance = item.Distance,
                    Duration = item.Duration,
                });

                i++;
            }

            return result;
        }

        public async Task<WalksGeneralInfoDTO> GetWalksGeneralInfoAsync(string imei)
        {
            ValidationService.ValidateImei(imei);

            var data = await _walkRepository.GetWalksGeneralInfoAsync(imei);

            return new WalksGeneralInfoDTO()
            {
                TotalCount = data.TotalCount,
                TotalDistance = data.TotalDistance,
                TotalDuration = data.TotalDuration,
            };
        }
    }
}
