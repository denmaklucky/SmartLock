using Domain.Results.Locks;
using MediatR;

namespace Domain.Queries.Locks;

public record GetLocksQuery(Guid UserId) : IRequest<GetLocksResult>;