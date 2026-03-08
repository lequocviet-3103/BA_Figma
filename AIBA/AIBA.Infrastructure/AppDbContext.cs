using AIBA.Domain.Enities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIBA.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<RequirementAnalysis> Analyses { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
                entity.Property(p => p.CreatedAt).IsRequired();
                
                entity.HasMany(p => p.Analyses)
                    .WithOne()
                    .HasForeignKey("ProjectId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RequirementAnalysis>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Idea).IsRequired();
                entity.Property(r => r.UserStories);
                entity.Property(r => r.UseCases);
                entity.Property(r => r.FunctionalRequirements);
                entity.Property(r => r.DatabaseSchema);
                entity.Property(r => r.ApiSuggestions);
                entity.Property(r => r.CreatedAt).IsRequired();
                entity.Property<Guid>("ProjectId").IsRequired();
            });
        }
    }
}
