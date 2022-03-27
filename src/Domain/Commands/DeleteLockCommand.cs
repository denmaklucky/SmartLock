using Domain.Results;
using MediatR;

namespace Domain.Commands;

public record DeleteLockCommand(Guid LockId, Guid UserId) : IRequest<DeleteLockResult>;