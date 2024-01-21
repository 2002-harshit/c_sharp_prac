using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace bank_proj_db_mvc.Models;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Sbaccount> Sbaccounts { get; set; }

    public virtual DbSet<Sbtransaction> Sbtransactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=mysecretpassword");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sbaccount>(entity =>
        {
            entity.HasKey(e => e.Accountnumber).HasName("sbaccount_pkey");

            entity.ToTable("sbaccount");

            entity.Property(e => e.Accountnumber).HasColumnName("accountnumber");
            entity.Property(e => e.Currentbalance).HasColumnName("currentbalance");
            entity.Property(e => e.Customeraddress)
                .HasMaxLength(255)
                .HasColumnName("customeraddress");
            entity.Property(e => e.Customername)
                .HasMaxLength(255)
                .HasColumnName("customername");
            
        });

        modelBuilder.Entity<Sbtransaction>(entity =>
        {
            entity.HasKey(e => e.Transactionid).HasName("sbtransactions_pkey");

            entity.ToTable("sbtransaction");

            entity.Property(e => e.Transactionid)
                .HasDefaultValueSql("nextval('sbtransactions_transactionid_seq'::regclass)")
                .HasColumnName("transactionid");
            entity.Property(e => e.Accountnumber).HasColumnName("accountnumber");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Transactiondate).HasColumnName("transactiondate");
            entity.Property(e => e.Transactiontype)
                .HasMaxLength(50)
                .HasColumnName("transactiontype");

            entity.HasOne(d => d.AccountnumberNavigation).WithMany(p => p.Sbtransactions)
                .HasForeignKey(d => d.Accountnumber)
                .HasConstraintName("sbtransactions_accountnumber_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
