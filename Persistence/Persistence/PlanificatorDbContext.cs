using Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Persistence.Persistence
{
    public class PlanificatorDbContext : DbContext
    {
        public DbSet<SpeakerProfile> SpeakerProfiles { get; set; }
        public DbSet<Presentation> Presentations { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PresentationTag> PresentationTags { get; set; }
        public DbSet<PresentationSpeaker> PresentationSpeakers { get; set; }

        public PlanificatorDbContext(DbContextOptions<PlanificatorDbContext> options)
        : base(options)
        {
        }

        public PlanificatorDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                IConfigurationRoot configuration = builder.Build();
                optionsBuilder.UseSqlServer(@configuration.GetConnectionString("Default"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SpeakerProfile>()
                    .Property(s => s.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();
            modelBuilder.Entity<SpeakerProfile>()
                    .Property(s => s.LastName)
                    .HasMaxLength(50)
                    .IsRequired();
            modelBuilder.Entity<SpeakerProfile>()
                    .Property(s => s.Email)
                    .HasMaxLength(255)
                    .IsRequired();
            modelBuilder.Entity<SpeakerProfile>()
                    .Property(s => s.Bio)
                    .HasMaxLength(100)
                    .IsRequired(false);
            modelBuilder.Entity<SpeakerProfile>()
                    .Property(s => s.PhotoPath)
                    .HasMaxLength(200)
                    .IsRequired(false);
            modelBuilder.Entity<SpeakerProfile>()
                    .Property(s => s.Company)
                    .HasMaxLength(60)
                    .IsRequired(false);

            modelBuilder.Entity<Presentation>()
                .Property(s => s.Title)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Presentation>()
                .Property(s => s.ShortDescription)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<Presentation>()
                .Property(s => s.LongDescription)
                .HasMaxLength(800)
                .IsRequired();

            modelBuilder.Entity<Tag>()
                .Property(s => s.TagName)
                .HasMaxLength(50)
                .IsRequired();

            //PresentationTag Many to Many Configuration
            modelBuilder.Entity<PresentationTag>().HasKey(pt => new { pt.PresentationId, pt.TagId });

            modelBuilder.Entity<PresentationTag>()
                .HasOne<Presentation>(pt => pt.Presentation)
                .WithMany(p => p.PresentationTags)
                .HasForeignKey(pt => pt.PresentationId)
                .IsRequired();

            modelBuilder.Entity<PresentationTag>()
                .HasOne<Tag>(pt => pt.Tag)
                .WithMany(t => t.PresentationTags)
                .HasForeignKey(pt => pt.TagId)
                .IsRequired();

            //PresentationSpeaker Many to Many Configuration
            modelBuilder.Entity<PresentationSpeaker>().HasKey(pt => new { pt.PresentationId, pt.SpeakerId });

            modelBuilder.Entity<PresentationSpeaker>()
                .HasOne<Presentation>(pt => pt.Presentation)
                .WithMany(p => p.PresentationSpeakers)
                .HasForeignKey(pt => pt.PresentationId)
                .IsRequired();

            modelBuilder.Entity<PresentationSpeaker>()
                .HasOne<SpeakerProfile>(pt => pt.SpeakerProfile)
                .WithMany(t => t.PresentationSpeakers)
                .HasForeignKey(pt => pt.SpeakerId)
                .IsRequired();

            //Presentation Many to One Speaker Relationship
            modelBuilder.Entity<Presentation>()
                .HasOne<SpeakerProfile>(p => p.PresentationOwner)
                .WithMany(s => s.OwnedPresentations)
                .IsRequired();

            modelBuilder.Entity<SpeakerProfile>()
                .HasMany<Presentation>(s => s.OwnedPresentations)
                .WithOne(p => p.PresentationOwner)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}