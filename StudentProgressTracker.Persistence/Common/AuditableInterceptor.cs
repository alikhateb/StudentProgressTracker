using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using StudentProgressTracker.Domain.Common;
using StudentProgressTracker.Shared.CurrentUser;

namespace StudentProgressTracker.Persistence.Common;

public class AuditableInterceptor(ICurrentUserService currentUserService) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result
    )
    {
        var context = eventData.Context;
        if (context is null)
        {
            return base.SavingChanges(eventData, result);
        }

        context.ChangeTracker.DetectChanges();
        var entries = context.ChangeTracker.Entries<Auditable>().ToList();
        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.SetCreatedById(currentUserService.GetUserId());
                    entry.Entity.SetCreationDate(DateTime.UtcNow);
                    entry.Entity.SetCreatedByName(currentUserService.GetUserName());
                    break;
                case EntityState.Modified:
                    entry.Entity.SetModifiedById(currentUserService.GetUserId());
                    entry.Entity.SetModificationDate(DateTime.UtcNow);
                    entry.Entity.SetModifiedByName(currentUserService.GetUserName());
                    break;
                case EntityState.Detached:
                case EntityState.Unchanged:
                case EntityState.Deleted:
                default:
                    break;
            }
        }

        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken()
    )
    {
        var context = eventData.Context;
        if (context is null)
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        context.ChangeTracker.DetectChanges();
        var entries = context.ChangeTracker.Entries<Auditable>().ToList();
        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.SetCreatedById(currentUserService.GetUserId());
                    entry.Entity.SetCreationDate(DateTime.UtcNow);
                    entry.Entity.SetCreatedByName(currentUserService.GetUserName());
                    break;
                case EntityState.Modified:
                    entry.Entity.SetModifiedById(currentUserService.GetUserId());
                    entry.Entity.SetModificationDate(DateTime.UtcNow);
                    entry.Entity.SetModifiedByName(currentUserService.GetUserName());
                    break;
                case EntityState.Detached:
                case EntityState.Unchanged:
                case EntityState.Deleted:
                default:
                    break;
            }
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
