using System.ComponentModel.DataAnnotations;

namespace WebApi.Requests;

public record CreateLockRequest(string Title, [Required]string ActivationKey);