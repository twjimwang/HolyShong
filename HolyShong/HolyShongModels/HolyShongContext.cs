using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace HolyShong.HolyShongModels
{
    public partial class HolyShongContext : DbContext
    {
        public HolyShongContext()
            : base("name=HolyShongConnection")
        {
        }

        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<ItemDetail> ItemDetail { get; set; }
        public virtual DbSet<ProductOption> ProductOption { get; set; }
        public virtual DbSet<ProductOptionDetail> ProductOptionDetail { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
                .HasMany(e => e.Item)
                .WithRequired(e => e.Cart)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Item>()
                .HasMany(e => e.ItemDetail)
                .WithRequired(e => e.Item)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductOption>()
                .HasMany(e => e.ProductOptionDetail)
                .WithRequired(e => e.ProductOption)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductOptionDetail>()
                .HasMany(e => e.ItemDetail)
                .WithRequired(e => e.ProductOptionDetail)
                .HasForeignKey(e => e.ProductOptionDetailId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductOptionDetail>()
                .HasMany(e => e.ItemDetail1)
                .WithRequired(e => e.ProductOptionDetail1)
                .HasForeignKey(e => e.ProductOptionId)
                .WillCascadeOnDelete(false);
        }
    }
}
