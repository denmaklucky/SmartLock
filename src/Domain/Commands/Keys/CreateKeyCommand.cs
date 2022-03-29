using Domain.Results.Keys;
using MediatR;
using Model.Enums;

namespace Domain.Commands.Keys;

public record CreateKeyCommand(Guid CreatedBy, string LockId, string UserId, KeyTypeEnum Type) : IRequest<CreateKeyResult>;