using InterviewService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterviewService.Data
{
    public class InterviewDbContext : DbContext
    {
        public InterviewDbContext(DbContextOptions<InterviewDbContext> options) : base(options)
        {
            
        }

        public DbSet<Interview> Interviews { get; set; }
        public DbSet<Question> Questions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Interview>(entity =>
            {
                entity.HasKey(i => i.Id);
                entity.Property(i => i.CandidateId).IsRequired();
                entity.Property(i => i.Role).IsRequired();
                entity.HasMany(i => i.Questions)
                        .WithOne(q => q.Interview)
                        .HasForeignKey(i => i.InterviewId)
                        .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(i => i.Id);
                entity.Property(q => q.Text).IsRequired();
                entity.Property(a => a.Answer)
                        .HasMaxLength(5000);
                entity.Property(q => q.Source)
                        .HasMaxLength(100);
            });
            
        }
    }
}
