using Domain.Results.Locks;
using MediatR;

namespace Domain.Commands.Locks;

public record DeleteLockCommand(string LockId, Guid UserId) : IRequest<DeleteLockResult>;