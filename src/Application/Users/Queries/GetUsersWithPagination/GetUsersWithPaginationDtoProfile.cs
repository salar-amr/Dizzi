namespace Application.Users.Queries.GetUsersWithPagination;

public class GetUsersWithPaginationDtoProfile : Profile
{
    public GetUsersWithPaginationDtoProfile()
    {
        CreateMap<User, GetUsersWithPaginationDto>();

    }
}
