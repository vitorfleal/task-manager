using Microsoft.AspNetCore.Mvc;
using Sentry;
using TaskManager.Application.Base.Notifications;
using TaskManager.Application.Features.Status.Services.Contracts;
using TaskManager.Application.Features.TaskJobs.Responses;

namespace TaskManager.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/status")]
public class StatusController : ControllerBase
{
    private readonly IStatusAppService _statusAppService;

    public StatusController(IStatusAppService statusAppService)
    {
        _statusAppService = statusAppService;
    }

    /// <summary>
    /// Realiza a obtenção de todos os status cadastrados
    /// </summary>
    /// <returns>Retorno padrão que contém lista dos status</returns>
    /// <response code="200">Retorno padrão com dados</response>
    /// <response code="404">Retorno padrão informando erros que aconteceram</response>
    [HttpGet]
    [ProducesResponseType(typeof(TaskJobResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllStatus()
    {
        var (response, status) = await _statusAppService.GetAllStatus();

        if (!response.IsValid() || status is null)
            return NotFound(new Notification("Status", "Status Not Found"));

        return Ok(status);
    }
}