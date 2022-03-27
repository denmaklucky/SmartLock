using Domain.Results;
using MediatR;

namespace Domain.Commands;

public record OpenLockCommand(string LockId, string KeyId, Guid UserId) : IRequest<OpenLockResult>;