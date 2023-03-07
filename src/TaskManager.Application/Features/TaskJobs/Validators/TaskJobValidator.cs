using FluentValidation;
using System.Linq.Expressions;
using TaskManager.Application.Features.TaskJobs.Requests;

namespace TaskManager.Application.Features.TaskJobs.Validators;

public class TaskJobValidator<T> : AbstractValidator<T> where T : CreateTaskJobRequest
{
    public TaskJobValidator()
    {
        RuleRequiredFor(taskRequest => taskRequest.Name, "Nome");
        RuleRequiredFor(taskRequest => taskRequest.Description, "Descrição");
        RuleRequiredFor(taskRequest => taskRequest.DeliveryDate, "Data para entrega");

        RuleFor(taskRequest => taskRequest.EstimateHours)
            .GreaterThan(0).WithMessage("A estimativa em horas da tarefa deve ser maior que zero.");

        RuleFor(taskRequest => taskRequest.DeliveryDate)
                    .Must(deliveryDate => deliveryDate >= DateTime.Today).WithMessage("A data para entrega da tarefa não pode ser menor do que a data atual.");
    }

    public void RuleRequiredFor<TProperty>(Expression<Func<T, TProperty>> expression, string label)
    {
        RuleFor(expression)
            .NotEmpty().WithMessage($"{label} da tarefa é obrigatório.");
    }
}