using Domain.Results.Locks;
using MediatR;
using Model.Enums;

namespace Domain.Commands.Locks;

public record UpdateLockCommand(Guid UserId, string LockId, string Title, LockModeEnum? Mode, TimeOnly? StartOpenTime, TimeOnly? EndOpenTime) : IRequest<UpdateLockResult>;