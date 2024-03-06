namespace Application.Users.Queries.GetUsersWithPagination;

public class GetUsersWithPaginationQueryValidator :
    AbstractValidator<GetUsersWithPaginationQuery>
{
    public GetUsersWithPaginationQueryValidator()
    {
        RuleFor(property => property.PageSize)
            .Must(size => size is <= 50 and >= 1)
            .WithMessage("The maximum number of item values that can be displayed is 50");
    }
}
