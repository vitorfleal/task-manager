using TaskManager.Application.Base.Models;
using TaskManager.Application.Features.Status.Responses;

namespace TaskManager.Application.Features.Status.Services.Contracts;

public interface IStatusAppService
{
    Task<(Response, IEnumerable<StatusResponse?>)> GetAllStatus();
}