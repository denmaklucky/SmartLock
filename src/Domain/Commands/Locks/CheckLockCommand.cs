using Domain.Results.Locks;
using MediatR;

namespace Domain.Commands.Locks;

public record CheckLockCommand(Guid LockId) : IRequest<CheckLockResult>;