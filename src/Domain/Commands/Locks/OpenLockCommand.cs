using Domain.Results.Locks;
using MediatR;
using Model.Enums;

namespace Domain.Commands.Locks;

public record OpenLockCommand(string LockId, string KeyId, AccessTypeEnum AccessType, Guid OpenedBy) : IRequest<OpenLockResult>;