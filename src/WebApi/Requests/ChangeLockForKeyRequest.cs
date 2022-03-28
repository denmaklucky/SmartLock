using System.ComponentModel.DataAnnotations;

namespace WebApi.Requests;

public record ChangeLockForKeyRequest([Required]string NewLockId, [Required]string OldNewLockId);