using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Day27_EFCore_FirstTimeSolo_DatabaseFirst.Models
{
    public partial class SupersDbContext : DbContext
    {
        public SupersDbContext()
        {
        }

        public SupersDbContext(DbContextOptions<SupersDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Mission> Mission { get; set; }
        public virtual DbSet<Super> Super { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=SupersDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mission>(entity =>
            {
                entity.Property(e => e.DueDate).HasColumnType("date");

                entity.Property(e => e.MissionName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Super)
                    .WithMany(p => p.Mission)
                    .HasForeignKey(d => d.SuperId)
                    .HasConstraintName("FK__Mission__SuperId__398D8EEE");
            });

            modelBuilder.Entity<Super>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Power)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SuperName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
