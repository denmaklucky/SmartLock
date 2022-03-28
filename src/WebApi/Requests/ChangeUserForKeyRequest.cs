using System.ComponentModel.DataAnnotations;

namespace WebApi.Requests;

public record ChangeUserForKeyRequest([Required]string NewUserId);