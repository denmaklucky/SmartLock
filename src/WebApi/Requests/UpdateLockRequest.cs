using Model.Enums;

namespace WebApi.Requests;

public record UpdateLockRequest(string Title, LockModeEnum? Mode, TimeOnly? StartOpenTime, TimeOnly? EndOpenTime);