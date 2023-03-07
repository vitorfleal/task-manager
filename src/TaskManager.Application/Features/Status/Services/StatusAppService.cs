using AutoMapper;
using TaskManager.Application.Base.Models;
using TaskManager.Application.Base.Persistence;
using TaskManager.Application.Base.Services;
using TaskManager.Application.Features.Status.Repositories;
using TaskManager.Application.Features.Status.Responses;
using TaskManager.Application.Features.Status.Services.Contracts;

namespace TaskManager.Application.Features.Status.Services;

public class StatusAppService : AppService, IStatusAppService
{
    private readonly IMapper _mapper;
    private readonly IStatusRepository _statusRepository;

    public StatusAppService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IStatusRepository statusRepository) : base(unitOfWork)
    {
        _mapper = mapper;
        _statusRepository = statusRepository;
    }

    public async Task<(Response, IEnumerable<StatusResponse?>)> GetAllStatus() => (Response.Valid(), _mapper.Map<IEnumerable<StatusResponse?>>(await _statusRepository.GetAllAsync()));
}