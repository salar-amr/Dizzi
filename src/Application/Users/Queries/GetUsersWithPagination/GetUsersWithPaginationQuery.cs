using Application.Common.Models;

namespace Application.Users.Queries.GetUsersWithPagination;

public record GetUsersWithPaginationQuery : PaginatedQuery,
    IRequest<PaginatedList<GetUsersWithPaginationDto>>
{
    public int Id { get; set; }
    public string Name { get; set; }
}
