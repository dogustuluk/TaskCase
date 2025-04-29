using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskCase.Application.Common.GenericObjects;
using TaskCase.Application.Features.Commands.Order.CreateOrder;
using TaskCase.Application.Features.Commands.Order.DeleteOrder;
using TaskCase.Application.Features.Queries.Order.GetAllOrder;
using TaskCase.Application.Features.Queries.Order.GetOrderDetail;

namespace TaskCase.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("CreateOrder")]
    public async Task<IActionResult> CreateAnnouncement(CreateOrderCommandRequest request)
    {
        OptResult<CreateOrderCommandResponse> response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpGet("GetOrders")]
    public async Task<IActionResult> GetOrders()
    {
        var res = await _mediator.Send(new GetOrdersQueryRequest());
        return Ok(res);
    }

    [HttpGet("GetOrder/{id:int}")]
    public async Task<IActionResult> GetOrder(int id)
    {
        var res = await _mediator.Send(
            new GetOrderDetailQueryRequest { OrderId = id });
        return Ok(res);
    }

    [HttpDelete("DeleteOrder/{id:int}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var res = await _mediator.Send(
            new DeleteOrderCommandRequest { OrderId = id });
        return Ok(res);
    }
}
