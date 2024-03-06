
using FluentResults;

namespace Application.RetailGiftAccountTransactions.Commands.BatchIncreaseRetailBalance;

public record CreateUserCommand : IRequest<Result<string>>
{
    public string User { get; set; } = string.Empty;
}
