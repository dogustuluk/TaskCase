using MediatR;
using TaskCase.Application.Common.GenericObjects;

namespace TaskCase.Application.Features.Queries.Order.GetAllOrder;
public sealed class GetOrdersQueryRequest : IRequest<OptResult<List<TaskCase.Domain.Entities.Order>>> { }