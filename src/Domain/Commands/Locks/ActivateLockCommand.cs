using Domain.Results.Locks;
using MediatR;

namespace Domain.Commands.Locks;

public record ActivateLockCommand(string Title, string ActivationKey, Guid ActivatedBy) : IRequest<CreateLockResult>;