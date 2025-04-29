using MediatR;
using TaskCase.Application.Common.GenericObjects;

namespace TaskCase.Application.Features.Commands.Order.CreateOrder;
public sealed class CreateOrderCommandRequest
    : IRequest<OptResult<CreateOrderCommandResponse>>
{
    public List<OrderLineDto> Items { get; init; } = new();
}

public sealed class OrderLineDto
{
    public int ProductId { get; init; }
    public int Quantity { get; init; }
}
