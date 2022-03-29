using Domain.Results.Keys;
using MediatR;

namespace Domain.Queries.Keys;

public record GetKeysQuery(Guid CreatedBy) : IRequest<GetKeysResult>;