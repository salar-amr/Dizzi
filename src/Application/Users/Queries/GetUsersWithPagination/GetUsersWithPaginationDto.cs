#pragma warning disable CS8618

using Application.Helpers;
using DNTPersianUtils.Core;
using Domain.Enums;

namespace Application.Users.Queries.GetUsersWithPagination;

public class GetUsersWithPaginationDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}
