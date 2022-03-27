using Domain.Results.Keys;
using MediatR;

namespace Domain.Commands.Keys;

public record CheckKeyCommand(Guid KeyId, Guid LockId) : IRequest<CheckKeyResult>;