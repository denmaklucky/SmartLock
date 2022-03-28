using Domain.Results.Keys;
using MediatR;

namespace Domain.Commands.Keys;

public record CanOpenLockByKeyCommand(Guid KeyId, Guid LockId) : IRequest<CanOpenLockByKeyResult>;