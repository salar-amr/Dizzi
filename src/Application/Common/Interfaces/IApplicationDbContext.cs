using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
