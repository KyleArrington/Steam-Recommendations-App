using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SteamAccessPort.Application.Options;

namespace SteamAccessPort.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SteamConfigController : ControllerBase
{
    private readonly SteamOptions _steamOptions;

    public SteamConfigController(IOptions<SteamOptions> steamOptions)
    {
        _steamOptions = steamOptions.Value;
    }

    [HttpGet("check")]
    public ActionResult Check()
    {
        return Ok(new
        {
            apiKeyConfigured = !string.IsNullOrWhiteSpace(_steamOptions.ApiKey),
            domainNameConfigured = !string.IsNullOrWhiteSpace(_steamOptions.DomainName),
            apiKeyLength = _steamOptions.ApiKey.Length
        });
    }
}