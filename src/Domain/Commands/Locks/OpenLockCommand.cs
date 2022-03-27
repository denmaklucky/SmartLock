using Domain.Results.Locks;
using MediatR;

namespace Domain.Commands.Locks;

public record OpenLockCommand(string LockId, string KeyId, Guid OpenedBy) : IRequest<OpenLockResult>;