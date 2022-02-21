using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using GetirTestApi.Abstractions;
using GetirTestApi.Domain;

using MediatR;

namespace GetirTestApi.Infrastructure
{
    public class OperationsContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public OperationsContext(DbContextOptions<OperationsContext> options)
            : base(options) { }

        public OperationsContext(DbContextOptions<OperationsContext> options, IMediator mediator)
            : base(options)
        {
            ArgValidator.NotNull(mediator, nameof(mediator));

            _mediator = mediator;

            Database.EnsureCreated();
        }

        public DbSet<Customer> Customers { get; set; }

        public async Task<int> Commit()
        {
            await _mediator.DispatchDomainEvents(this);

            return await base.SaveChangesAsync();
        }

        public async Task Rollback()
        {
            foreach (var entity in base.ChangeTracker.Entries())
                switch (entity.State)
                {
                    case EntityState.Added: entity.State = EntityState.Detached; break;
                    case EntityState.Modified: entity.State = EntityState.Unchanged; break;
                    case EntityState.Deleted: await entity.ReloadAsync(); break;

                    default: break;
                }
        }
    }
}
