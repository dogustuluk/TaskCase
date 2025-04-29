using AutoMapper;
using MediatR;
using TaskCase.Application.Common.Extensions;
using TaskCase.Application.Common.GenericObjects;
using TaskCase.Application.Services;

namespace TaskCase.Application.Features.Commands.Order.CreateOrder;
public sealed class CreateOrderCommandHandler
    : IRequestHandler<CreateOrderCommandRequest, OptResult<CreateOrderCommandResponse>>
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    public async Task<OptResult<CreateOrderCommandResponse>> Handle(
        CreateOrderCommandRequest request,
        CancellationToken cancellationToken)
    {
        return await ExceptionHandler.HandleOptResultAsync(async () =>
        {
            var dto = _mapper.Map<TaskCase.Domain.Entities.Order>(request);
            var result = await _orderService.CreateOrderAsync(dto);

            if (!result.Succeeded)
                return await OptResult<CreateOrderCommandResponse>
                               .FailureAsync(result.Messages);

            var response = _mapper.Map<CreateOrderCommandResponse>(result.Data);
            return await OptResult<CreateOrderCommandResponse>
                           .SuccessAsync(response);
        });
    }
}
