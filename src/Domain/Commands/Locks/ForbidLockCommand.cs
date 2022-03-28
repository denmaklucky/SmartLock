using Domain.Results.Locks;
using MediatR;

namespace Domain.Commands.Locks;

public record ForbidLockCommand(Guid ForbiddenBy, string LockId, string AccessId): IRequest<ForbidLockResult>;