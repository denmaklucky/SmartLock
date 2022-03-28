using System.ComponentModel.DataAnnotations;
using Model.Enums;

namespace WebApi.Requests;

public record AdmitLockRequest([Required]string AccessId, [Required]AccessTypeEnum Type);