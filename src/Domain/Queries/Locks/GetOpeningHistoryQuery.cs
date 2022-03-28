using Domain.Results.Locks;
using MediatR;

namespace Domain.Queries.Locks;

public record GetOpeningHistoryQuery(Guid UserId) : IRequest<GetOpeningHistoryResult>;