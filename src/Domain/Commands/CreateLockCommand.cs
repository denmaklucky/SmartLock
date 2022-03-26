using Domain.Results;
using MediatR;

namespace Domain.Commands;

public record CreateLockCommand(string Title, string ActivationKey, Guid UserId) : IRequest<CreateLockResult>;