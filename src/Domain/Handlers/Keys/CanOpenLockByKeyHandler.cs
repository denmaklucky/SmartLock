using Domain.Commands.Keys;
using Domain.Results.Keys;
using MediatR;

namespace Domain.Handlers.Keys;

public class CanOpenLockByKeyHandler : IRequestHandler<CanOpenLockByKeyCommand, CanOpenLockByKeyResult>
{
    public Task<CanOpenLockByKeyResult> Handle(CanOpenLockByKeyCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}