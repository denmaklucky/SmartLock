using Domain.Results;
using MediatR;

namespace Domain.Queries;

public record GetUserQuery(Guid UserId) : IRequest<GetUserResult>;