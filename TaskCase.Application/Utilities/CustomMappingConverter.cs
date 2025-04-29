using AutoMapper;
using TaskCase.Application.Common.GenericObjects;

namespace TaskCase.Application.Utilities;

public class PaginatedListConverter<TSource, TDestination> : ITypeConverter<PaginatedList<TSource>, PaginatedList<TDestination>>
    where TSource : class
    where TDestination : class
{
    public PaginatedList<TDestination> Convert(PaginatedList<TSource> source, PaginatedList<TDestination> destination, ResolutionContext context)
    {
        var mappedData = context.Mapper.Map<List<TDestination>>(source.Data);

        return new PaginatedList<TDestination>
        {
            Data = mappedData,
            Pagination = new Pagination
            {
                PageIndex = source.Pagination.PageIndex,
                TotalPages = source.Pagination.TotalPages,
                TotalRecords = source.Pagination.TotalRecords,
                PageSize = source.Pagination.PageSize,
                HasPreviousPage = source.Pagination.HasPreviousPage,
                HasNextPage = source.Pagination.HasNextPage
            }
        };
    }
}

