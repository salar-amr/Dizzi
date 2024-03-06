using Application.Common.Mappings;
using Application.Common.Models;

namespace Application.Users.Queries.GetUsersWithPagination;

public class GetUsersWithPaginationQueryHandler :
    IRequestHandler<GetUsersWithPaginationQuery, PaginatedList<GetUsersWithPaginationDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;

    public GetUsersWithPaginationQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<PaginatedList<GetUsersWithPaginationDto>> Handle(GetUsersWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        return new PaginatedList<GetUsersWithPaginationDto> { };
    }
}
