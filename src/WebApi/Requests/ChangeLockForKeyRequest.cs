using System.ComponentModel.DataAnnotations;

namespace WebApi.Requests;

public record ChangeLockForKeyRequest([Required]string NewLockId);