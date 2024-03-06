using System.Globalization;
using Application.Helpers;
using Domain.Enums;
using FluentResults;

namespace Application.RetailGiftAccountTransactions.Commands.BatchIncreaseRetailBalance;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<string>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var result = new Result<string>();

        //DO SOMETHING

        return result.WithSuccess("");
    }
}
