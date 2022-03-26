using Domain.Dto;
using MediatR;

namespace Domain.Queries;

public record GetLocksQuery() : IRequest<List<LockDto>>;