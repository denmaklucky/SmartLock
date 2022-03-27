using System.ComponentModel.DataAnnotations;

namespace WebApi.Requests;

public record OpenLockRequest([Required]string KeyId);