using Microsoft.EntityFrameworkCore;
using SmartSoftware.Domain.Entities;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.DistributedEvents;

namespace DistDemoApp
{
    public class TodoDbContext : SmartSoftwareDbContext<TodoDbContext>, IHasEventOutbox, IHasEventInbox
    {
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<TodoSummary> TodoSummaries { get; set; }
        public DbSet<OutgoingEventRecord> OutgoingEvents { get; set; }
        public DbSet<IncomingEventRecord> IncomingEvents { get; set; }

        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ConfigureEventOutbox();
            modelBuilder.ConfigureEventInbox();

            modelBuilder.Entity<TodoItem>(b =>
            {
                b.Property(x => x.Text).IsRequired().HasMaxLength(128);
            });
        }
    }
}