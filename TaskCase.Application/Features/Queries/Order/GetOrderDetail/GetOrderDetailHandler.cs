using MediatR;
using TaskCase.Application.Common.GenericObjects;
using TaskCase.Application.Services;

namespace TaskCase.Application.Features.Queries.Order.GetOrderDetail;

public sealed class GetOrderDetailQueryRequest
    : IRequest<OptResult<TaskCase.Domain.Entities.Order>>
{
    public int OrderId { get; init; }
}

public sealed class GetOrderDetailQueryHandler
    : IRequestHandler<GetOrderDetailQueryRequest, OptResult<TaskCase.Domain.Entities.Order>>
{
    private readonly IOrderService _orderService;

    public GetOrderDetailQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public Task<OptResult<TaskCase.Domain.Entities.Order>> Handle(
        GetOrderDetailQueryRequest request,
        CancellationToken cancellationToken)
        => _orderService.GetByIdOrGuid(request.OrderId);
}
