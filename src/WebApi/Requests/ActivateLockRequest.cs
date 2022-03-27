using System.ComponentModel.DataAnnotations;

namespace WebApi.Requests;

public record ActivateLockRequest(string Title, [Required]string ActivationKey);