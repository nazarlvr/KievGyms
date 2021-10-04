using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace KievGyms
{
    public partial class GymDBContext : DbContext
    {
        public GymDBContext()
        {
        }

        public GymDBContext(DbContextOptions<GymDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Gym> Gyms { get; set; }
        public virtual DbSet<GymMembership> GymMemberships { get; set; }
        public virtual DbSet<Specialization> Specializations { get; set; }
        public virtual DbSet<Trainer> Trainers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server= WIN-HUNRDISTUA0\\MSSQLSERVER02; Database= GymDB; Trusted_Connection= True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.ClientDateOfBirth).HasColumnType("date");

                entity.Property(e => e.ClientFullName).IsRequired();

                entity.Property(e => e.ClientMobilePhone)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.Property(e => e.DistrictId).HasColumnName("DistrictID");

                entity.Property(e => e.DistrictName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Gym>(entity =>
            {
                entity.Property(e => e.GymId).HasColumnName("GymID");

                entity.Property(e => e.DistrictId).HasColumnName("DistrictID");

                entity.Property(e => e.GymInfo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.GymName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Gyms)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Gyms_Districts");
            });

            modelBuilder.Entity<GymMembership>(entity =>
            {
                entity.Property(e => e.GymMembershipId).HasColumnName("GymMembershipID");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.GymId).HasColumnName("GymID");

                entity.Property(e => e.GymMembershipExpiryDate).HasColumnType("date");

                entity.Property(e => e.GymMembershipInfo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.GymMembershipPrice).HasColumnType("money");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.GymMemberships)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GymMemberships_Clients");

                entity.HasOne(d => d.Gym)
                    .WithMany(p => p.GymMemberships)
                    .HasForeignKey(d => d.GymId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GymMemberships_Gyms");
            });

            modelBuilder.Entity<Specialization>(entity =>
            {
                entity.Property(e => e.SpecializationId).HasColumnName("SpecializationID");

                entity.Property(e => e.SpecializationName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Trainer>(entity =>
            {
                entity.Property(e => e.TrainerId).HasColumnName("TrainerID");

                entity.Property(e => e.GymId).HasColumnName("GymID");

                entity.Property(e => e.SpecializationId).HasColumnName("SpecializationID");

                entity.Property(e => e.TrainerDateOfBirth).HasColumnType("date");

                entity.Property(e => e.TrainerFullName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TrainerSalary).HasColumnType("money");

                entity.HasOne(d => d.Gym)
                    .WithMany(p => p.Trainers)
                    .HasForeignKey(d => d.GymId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trainers_Gyms");

                entity.HasOne(d => d.Specialization)
                    .WithMany(p => p.Trainers)
                    .HasForeignKey(d => d.SpecializationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trainers_Specializations");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
