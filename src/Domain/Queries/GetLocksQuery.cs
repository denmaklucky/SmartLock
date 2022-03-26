using Domain.Results;
using MediatR;

namespace Domain.Queries;

public record GetLocksQuery(Guid UserId) : IRequest<GetLocksResult>;