namespace Domain.Commands.Keys;

public record ChangeLockForKeyCommand(Guid UpdatedBy, string NewLockId);