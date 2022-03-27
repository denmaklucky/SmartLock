using Domain.Results.Keys;
using MediatR;
using Model.Enums;

namespace Domain.Commands.Keys;

public record CreateKeyCommand(Guid UserId, string LockId, KeyTypeEnum Type, DateTime? ExpiredAt) : IRequest<CreateKeyResult>;