using Domain.Results.Keys;
using MediatR;

namespace Domain.Commands.Keys;

public record CheckKeyCommand(Guid KeyId) : IRequest<CheckKeyResult>;