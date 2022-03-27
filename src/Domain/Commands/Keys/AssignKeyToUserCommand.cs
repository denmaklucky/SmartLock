using Domain.Results.Keys;
using MediatR;

namespace Domain.Commands.Keys;

public record AssignKeyToUserCommand(Guid UserId, string KeyId, string AssignTo) : IRequest<AssignKeyToUserResult>;