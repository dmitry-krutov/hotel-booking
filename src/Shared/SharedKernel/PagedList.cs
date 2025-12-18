using System.Collections.ObjectModel;

namespace SharedKernel;

public class PagedList<T>
{
    public PagedList(
        IReadOnlyCollection<T> items,
        int totalCount,
        int pageNumber,
        int pageSize)
    {
        if (pageSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than zero.");

        if (pageNumber <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be greater than zero.");

        Items = new ReadOnlyCollection<T>(items.ToList());
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    public IReadOnlyCollection<T> Items { get; }

    public int TotalCount { get; }

    public int PageNumber { get; }

    public int PageSize { get; }

    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

    public PagedList<TProjection> Select<TProjection>(Func<T, TProjection> selector)
    {
        var projectedItems = Items
            .Select(selector)
            .ToList();

        return new PagedList<TProjection>(projectedItems, TotalCount, PageNumber, PageSize);
    }
}