using System.ComponentModel.DataAnnotations;

namespace WebApi.Requests;

public record SignInRequest([Required]string Login, [Required]string Password);