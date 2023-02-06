// <copyright file="PaginatedItemsViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore;

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.API.Models.ViewModels;

/// <summary>
/// Represents a paginated view model.
/// </summary>
/// <typeparam name="T">The type of the items.</typeparam>
public class PaginatedItemsViewModel<T>
{
    /// <summary>
    /// The page size.
    /// </summary>
    public const int PageSize = 5;

    /// <summary>
    /// Initializes a new instance of the <see cref="PaginatedItemsViewModel{T}"/> class.
    /// </summary>
    /// <param name="items">The items.</param>
    /// <param name="totalItems">The total items.</param>
    /// <param name="pageIndex">The page index.</param>
    /// <param name="pageSize">The page size.</param>
    public PaginatedItemsViewModel(IEnumerable<T> items, int totalItems, int pageIndex, int pageSize)
    {
        this.PageIndex = pageIndex;
        this.TotalItems = totalItems;
        this.Items = items;
    }

    /// <summary>
    /// Gets the page index.
    /// </summary>
    public int PageIndex { get; private set; }

    /// <summary>
    /// Gets the total items.
    /// </summary>
    public int TotalItems { get; private set; }

    /// <summary>
    /// Gets the total pages.
    /// </summary>
    public int TotalPages => (int)Math.Ceiling(this.TotalItems / (double)PageSize);

    /// <summary>
    /// Gets the items.
    /// </summary>
    public IEnumerable<T> Items { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the page has a previous page.
    /// </summary>
    public bool HasPreviousPage => this.PageIndex > 0;

    /// <summary>
    /// Gets a value indicating whether the page has a next page.
    /// </summary>
    public bool HasNextPage => (this.PageIndex + 1) < this.TotalPages;

    /// <summary>
    /// Creates a new instance of the <see cref="PaginatedItemsViewModel{T}"/> class.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="pageIndex">The page index.</param>
    /// <returns>The paginated items view model.</returns>
    public static async Task<PaginatedItemsViewModel<T>> CreateAsync(IQueryable<T> source, int pageIndex)
    {
        var count = await source.CountAsync();
        var items = await source.Skip(pageIndex * PageSize).Take(PageSize).ToListAsync();
        return new PaginatedItemsViewModel<T>(items, count, pageIndex, PageSize);
    }
}