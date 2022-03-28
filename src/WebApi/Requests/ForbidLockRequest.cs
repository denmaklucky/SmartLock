using System.ComponentModel.DataAnnotations;

namespace WebApi.Requests;

public record ForbidLockRequest([Required]string AccessId);