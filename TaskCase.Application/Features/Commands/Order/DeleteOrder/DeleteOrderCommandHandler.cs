using MediatR;
using TaskCase.Application.Common.GenericObjects;
using TaskCase.Application.Services;

namespace TaskCase.Application.Features.Commands.Order.DeleteOrder;

public sealed class DeleteOrderCommandRequest
    : IRequest<OptResult<TaskCase.Domain.Entities.Order>>
{
    public int OrderId { get; init; }
}

public sealed class DeleteOrderCommandHandler
    : IRequestHandler<DeleteOrderCommandRequest, OptResult<Domain.Entities.Order>>
{
    private readonly IOrderService _orderService;

    public DeleteOrderCommandHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public Task<OptResult<TaskCase.Domain.Entities.Order>> Handle(
        DeleteOrderCommandRequest request,
        CancellationToken cancellationToken)
        => _orderService.DeleteOrderAsync(request.OrderId, deleteType: 2);
}
