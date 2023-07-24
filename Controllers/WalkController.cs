using System.Net;
using Microsoft.AspNetCore.Mvc;
using WalksInfoViberBot.Exceptions;
using WalksInfoViberBot.Models.DTOs;
using WalksInfoViberBot.Services.Interfaces;

namespace WalksInfoViberBot.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WalkController : ControllerBase
    {
        private readonly IWalkService _walkService;
        private readonly ILogger<WalkController> _logger;

        public WalkController(IWalkService walkService, ILogger<WalkController> logger)
        {
            _walkService = walkService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(WalksGeneralInfoDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetWalksGeneralInfo(string imei)
        {
            try
            {
                var result = await _walkService.GetWalksGeneralInfoAsync(imei);
                return Ok(result);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("System error.");
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<WalkDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get10LongestWalks(string imei)
        {
            try
            {
                var result = await _walkService.Get10LongestWalksAsync(imei);
                return Ok(result);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("System error.");
            }
        }
    }
}