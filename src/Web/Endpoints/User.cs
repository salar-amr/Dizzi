using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Users.Queries.GetUsersWithPagination;

namespace Web.Endpoints;

public class User : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
        // .RequireAuthorization()
           .MapGet(GetUsersWithPagination, "{retailId}");
    }

    public async Task<PaginatedList<GetUsersWithPaginationDto>> GetUsersWithPagination(ISender sender, IUser user, GetUsersWithPaginationQuery getUsersWithPaginationQuery)
        => await sender.Send(getUsersWithPaginationQuery);
}
