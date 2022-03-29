using Domain.Results.Keys;
using MediatR;

namespace Domain.Commands.Keys;

public record DeleteKeyCommand(Guid DeletedBy, string KeyId) : IRequest<DeleteKeyResult>;