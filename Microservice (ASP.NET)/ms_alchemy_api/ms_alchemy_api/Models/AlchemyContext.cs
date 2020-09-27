using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ms_alchemy_api.Models
{
    public partial class AlchemyContext : DbContext
    {
        public AlchemyContext()
        {
        }

        public AlchemyContext(DbContextOptions<AlchemyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Palettes> Palettes { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Palettes>(entity =>
            {
                entity.ToTable("palettes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Base)
                    .IsRequired()
                    .HasColumnName("base")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.ColorArray)
                    .IsRequired()
                    .HasColumnName("colorArray")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnName("createDate")
                    .HasColumnType("datetime2(6)")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Display)
                    .HasColumnName("display")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Login)
                    .HasName("UQ__users__7838F2721020692F")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("createDate")
                    .HasColumnType("datetime2(6)")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastLoginDate)
                    .HasColumnName("lastLoginDate")
                    .HasColumnType("datetime2(6)")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastLoginIp)
                    .IsRequired()
                    .HasColumnName("lastLoginIp")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.ReadPermission).HasColumnName("readPermission");

                entity.Property(e => e.TokenExpiration).HasColumnName("tokenExpiration");

                entity.Property(e => e.WritePermission).HasColumnName("writePermission");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
