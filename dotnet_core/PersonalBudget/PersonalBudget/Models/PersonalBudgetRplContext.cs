using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBudget.Models
{
    public class PersonalBudgetRplContext : DbContext
    {
        public DbSet<Categorie> Categorie { get; set; }
        public DbSet<TransactionType> TransactionType { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Release> Release { get; set; }

        public PersonalBudgetRplContext()
        {
        }

        public PersonalBudgetRplContext(DbContextOptions<PersonalBudgetRplContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Categorie>(entity =>
            {
                entity.ToTable("categorie");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserId)
                    .HasName("userId_index");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("userId")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<TransactionType>(entity =>
            {
                entity.ToTable("transactionType");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("transaction");

                entity.HasIndex(e => e.CategorieId)
                    .HasName("fk_transaction_categorie_idx");

                entity.HasIndex(e => e.TypeId)
                   .HasName("fk_transaction_type_idx");

                entity.HasIndex(e => e.Id)
                    .HasName("ID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => new { e.MonthRef, e.YearRef, e.UserId })
                    .HasName("fields_index");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.CategorieId)
                    .IsRequired()
                    .HasColumnName("categorieId")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.TypeId)
                    .IsRequired()
                    .HasColumnName("typeId")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DtTransaction)
                    .HasColumnName("dtTransaction")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.MonthRef).HasColumnName("monthRef");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("userId")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.YearRef).HasColumnName("yearRef");

                entity.HasOne(d => d.Categorie)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.CategorieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_transaction_categorie");

                entity.HasOne(d => d.TransactionType)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_transaction_type");
            });

            modelBuilder.Entity<Release>(entity =>
            {
                entity.ToTable("release");

                entity.HasIndex(e => e.TransactionId)
                    .HasName("fk_release_transaction_idx");

                entity.HasIndex(e => e.Id)
                    .HasName("ID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => new { e.MonthRef, e.YearRef })
                    .HasName("fields_index");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.TransactionId)
                    .IsRequired()
                    .HasColumnName("transactionId")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.MonthRef).HasColumnName("monthRef");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.YearRef).HasColumnName("yearRef");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.Releases)
                    .HasForeignKey(d => d.TransactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_release_transaction");
            });
        }
    }
}
