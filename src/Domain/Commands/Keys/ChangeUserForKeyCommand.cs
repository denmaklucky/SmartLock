using Domain.Results.Keys;
using MediatR;

namespace Domain.Commands.Keys;

public record ChangeUserForKeyCommand(Guid UpdatedBy, string KeyId, string NewUserId) : IRequest<ChangeUserForKeyResult>;