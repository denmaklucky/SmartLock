using Domain.Results;
using MediatR;

namespace Domain.Commands;

public record SignInCommand(string Login, string ProvidedPassword) : IRequest<SignInResult>;