using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Base.Notifications;
using TaskManager.Application.Features.TaskJobs.Requests;
using TaskManager.Application.Features.TaskJobs.Responses;
using TaskManager.Application.Features.TaskJobs.Services.Contracts;

namespace Acropolis.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/task-job")]
public class TaskJobController : ControllerBase
{
    private readonly ITaskJobAppService _taskJobAppService;

    public TaskJobController(
        ITaskJobAppService taskJobAppService)
    {
        _taskJobAppService = taskJobAppService;
    }

    /// <summary>
    /// Realiza o cadastro de uma nova tarefa
    /// </summary>
    /// <returns>Retorna a tarefa cadastrada</returns>
    /// <response code="201">Retorno padrão com dados</response>
    /// <response code="400">Retorno padrão informando erros nos parâmetros da requisição</response>
    /// <response code="422">Retorno padrão informando erros que aconteceram</response>
    [HttpPost]
    [ProducesResponseType(typeof(TaskJobResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(IEnumerable<Notification>), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(IEnumerable<Notification>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTaskJob(CreateTaskJobRequest request)
    {
        var (response, createdTaskJob) = await _taskJobAppService.CreateTaskJob(request);

        if (!response.IsValid() || createdTaskJob is null)
            return UnprocessableEntity(response.ToValidationErrors());

        return Created($"/{createdTaskJob.Id}", createdTaskJob);
    }

    /// <summary>
    /// Realiza a atualização dos de uma tarefa
    /// </summary>
    /// <response code="204">Retorno padrão sem dados</response>
    /// <response code="422">Retorno padrão informando erros que aconteceram</response>
    [HttpPut]
    [ProducesResponseType(typeof(TaskJobResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IEnumerable<Notification>), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(IEnumerable<Notification>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateTaskJob(UpdateTaskJobRequest request)
    {
        var (response, updateTaskJob) = await _taskJobAppService.UpdateTaskJob(request);

        if (!response.IsValid() || updateTaskJob is null)
            return UnprocessableEntity(response.ToValidationErrors());

        return NoContent();
    }

    /// <summary>
    /// Realiza a obtenção de todas as tarefas cadastradas
    /// </summary>
    /// <returns>Retorno padrão que contém lista das tarefas</returns>
    /// <response code="200">Retorno padrão com dados</response>
    /// <response code="404">Retorno padrão informando erros que aconteceram</response>
    [HttpGet]
    [ProducesResponseType(typeof(TaskJobResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllTaskJobBy()
    {
        var (response, taskJob) = await _taskJobAppService.GetAllTaskJob();

        if (!response.IsValid() || !taskJob.Any())
            return NotFound(new Notification("TaskJob", "TaskJob List Not Found"));

        return Ok(taskJob);
    }

    /// <summary>
    /// Realiza a obtenção de uma tarefa cadastrada tendo o identificador como parâmetro informado
    /// </summary>
    /// <returns>Retorno padrão que contém a tarefa</returns>
    /// <response code="200">Retorno padrão com dados</response>
    /// <response code="400">Retorno padrão informando erros nos parâmetros da requisição</response>
    /// <response code="404">Retorno padrão informando erros que aconteceram</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TaskJobResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTaskJobById(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest("Task Id Invalid");

        var (response, taskJob) = await _taskJobAppService.GetTaskJobById(id);

        if (!response.IsValid() || taskJob is null)
            return NotFound(new Notification("TaskJob", "TaskJob Not Found"));

        return Ok(taskJob);
    }

    /// <summary>
    /// Realiza a exclusão de uma tarefa tendo o identificador como parâmetro informado
    /// </summary>
    /// <response code="204">Retorno padrão sem dados</response>
    /// <response code="400">Retorno padrão informando erros nos parâmetros da requisição</response>
    /// <response code="422">Retorno padrão informando erros que aconteceram</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IEnumerable<Notification>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> RemoveTaskJobAsync(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest("Task Id Invalid");

        var response = await _taskJobAppService.RemoveTaskJob(id);

        if (!response.IsValid())
            return UnprocessableEntity(response.ToValidationErrors());

        return NoContent();
    }
}
