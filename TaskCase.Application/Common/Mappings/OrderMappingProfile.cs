using AutoMapper;
using TaskCase.Application.Features.Commands.Order.CreateOrder;
using TaskCase.Domain.Entities;

namespace TaskCase.Application.Common.Mappings;
public sealed class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<CreateOrderCommandRequest, Order>()
            .ForMember(d => d.UserId, o => o.MapFrom(_ => 1))
            .ForMember(d => d.Items, o => o.MapFrom(src =>
                src.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = 0
                })))
            .ForMember(d => d.Guid, o => o.MapFrom(_ => Guid.NewGuid()))
            .ForMember(d => d.CreatedDate, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.UpdatedDate, o => o.MapFrom(_ => DateTime.UtcNow));

        CreateMap<Order, CreateOrderCommandResponse>();

    }
}
