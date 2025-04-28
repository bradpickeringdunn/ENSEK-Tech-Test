using System;
using System.Collections.Generic;
using ENSEK.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ENSEK.DataAccess;

public partial class ENSEKDbContext : DbContext
{
    public ENSEKDbContext()
    {
    }

    public ENSEKDbContext(DbContextOptions<ENSEKDbContext> options)
        : base(options)
    {
        Database.SetCommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<MeterReading> MeterReadings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=ENSEK;Trusted_Connection=True; Encrypt=false ;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.Property(e => e.AccountId).ValueGeneratedNever();
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<MeterReading>(entity =>
        {
            entity.HasKey(e => new { e.AccountId, e.MeterReadingDateTime });

            entity.Property(e => e.MeterReadingDateTime).HasColumnType("datetime");
            entity.Property(e => e.MeterReadValue).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Account).WithMany(p => p.MeterReadings)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MeterReadings_Accounts");

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
