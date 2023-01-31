using Microsoft.EntityFrameworkCore;

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Models.ViewModels;

public class PaginatedItemsViewModel<T>
{
    public PaginatedItemsViewModel(IEnumerable<T> items, int totalItems, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalItems = totalItems;
        Items = items;
    }

    public int PageIndex { get; private set; }

    public int PageSize { get; private set; }

    public int TotalItems { get; private set; }

    public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);

    public IEnumerable<T> Items { get; private set; }

    public bool HasPreviousPage => (PageIndex > 0);

    public bool HasNextPage => ((PageIndex + 1) < TotalPages);

    public static async Task<PaginatedItemsViewModel<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedItemsViewModel<T>(items, count, pageIndex, pageSize);
    }
}
