using Domain.Results.Keys;
using MediatR;

namespace Domain.Commands.Keys;

public record ChangeLockForKeyCommand(Guid UpdatedBy, string KeyId, string NewLockId, string OldLockId) : IRequest<ChangeLockForKeyResult>;