using System.Reflection;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins;
using GymManagement.Domain.Common;
using GymManagement.Domain.Gyms;
using GymManagement.Domain.Subscriptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Common.Persistence;

public class GymManagementDbContext : DbContext, IUnitOfWork
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public DbSet<Admin> Admins { get; set; } = null!;
    public DbSet<Subscription> Subscriptions { get; set; } = null!;
    public DbSet<Gym> Gyms { get; set; } = null!;

    public GymManagementDbContext(
        DbContextOptions options,
        IHttpContextAccessor httpContextAccessor
    )
        : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task CommitChangesAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .SelectMany(x => x.Entity.PopDomainEvents())
            .ToList();

        AddDomainEventsToOfflineProcessingQueue(domainEvents);

        await SaveChangesAsync();
    }

    private void AddDomainEventsToOfflineProcessingQueue(List<IDomainEvent> domainEvents)
    {
        // fetch queue from http context or create a new queue if it doesnt exist
        var domainEventsQueue =
            _httpContextAccessor.HttpContext!.Items.TryGetValue("DomainEventsQueue", out var value)
            && value is Queue<IDomainEvent> existingDomainEventsQueue
                ? existingDomainEventsQueue
                : new Queue<IDomainEvent>();

        // add domain events to the end of queue
        domainEvents.ForEach(domainEventsQueue.Enqueue);

        // store the queue to http context
        _httpContextAccessor.HttpContext!.Items["DomainEventsQueue"] = domainEventsQueue;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
