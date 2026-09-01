using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions;

public interface IApplicationDbContext
{
    DbSet<Project> Projects { get; }

    DbSet<ProjectHistory> ProjectHistories { get; }

    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default);
}