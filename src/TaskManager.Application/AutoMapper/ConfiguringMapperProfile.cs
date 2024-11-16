using AutoMapper;
using TaskManager.Application.Features.TaskJobs;
using TaskManager.Application.Features.TaskJobs.Requests;
using TaskManager.Application.Features.TaskJobs.Responses;

namespace TaskManager.Application.AutoMapper;

public class ConfiguringMapperProfile : Profile
{
    public ConfiguringMapperProfile()
    {
        CreateMap<TaskJobResponse, TaskJob>()
            .ReverseMap();

        CreateMap<UpdateTaskJobRequest, TaskJob>()
           .ReverseMap();

    }
}