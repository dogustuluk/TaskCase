using MediatR;
using TaskCase.Application.Common.GenericObjects;
using TaskCase.Application.Services;

namespace TaskCase.Application.Features.Queries.Order.GetAllOrder;
public sealed class GetOrdersQueryHandler
    : IRequestHandler<GetOrdersQueryRequest, OptResult<List<TaskCase.Domain.Entities.Order>>>
{
    private readonly IOrderService _orderService;

    public GetOrdersQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<OptResult<List<TaskCase.Domain.Entities.Order>>> Handle(
        GetOrdersQueryRequest request,
        CancellationToken cancellationToken)
    {
        var list = await _orderService
            .GetAllOrderAsync(o => o.UserId == 1, "");
        return await OptResult<List<TaskCase.Domain.Entities.Order>>.SuccessAsync(list);
    }
}
