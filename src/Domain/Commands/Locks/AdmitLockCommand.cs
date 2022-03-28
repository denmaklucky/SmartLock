using Domain.Results.Locks;
using MediatR;
using Model.Enums;

namespace Domain.Commands.Locks;

public record AdmitLockCommand(Guid AdmittedBy, string LockId, string AccessId, AccessTypeEnum Type) : IRequest<AdmitLockResult>;