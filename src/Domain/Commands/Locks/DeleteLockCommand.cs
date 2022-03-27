using Domain.Results.Locks;
using MediatR;

namespace Domain.Commands.Locks;

public record DeleteLockCommand(Guid LockId, Guid UserId) : IRequest<DeleteLockResult>;