using ComplexModel.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace ComplexModel.Data
{
    public class DatabaseContext :DbContext
    {
       
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<UnitItem> UnitItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderedItem> OrderedItems { get; set; }


        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Blog>()
            //    .Property(b => b.Url)
            //    .IsRequired();

            //Item Model
            modelBuilder.Entity<Item>()
                        .Property(e => e.Name)
                        .IsRequired(true)
                        .HasMaxLength(50);
            //modelBuilder.Entity<Item>()
            //            .Property(e => e.Price)
            //            .IsRequired(true)
            //            .HasColumnType("decimal(18,0)");
            

         

            //Unit
            //modelBuilder.Entity<Unit>()
            //            .Property(e => e.UnitType)
            //            .IsRequired(true)
            //            .HasMaxLength(50);

            //OrderedItem
            modelBuilder.Entity<OrderedItem>()
                        .HasKey(k => new { k.OrderId, k.ItemId, k.UnitId });
            modelBuilder.Entity<OrderedItem>()
                        .HasOne(o => o.Order)
                        .WithMany(oi=>oi.OrderItem)
                        .HasForeignKey(f=>f.OrderId)
                        .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<OrderedItem>()
                       .HasOne(i => i.Item)
                       .WithMany(oi => oi.OrderedItems)
                       .HasForeignKey(f => f.ItemId)
                       .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<OrderedItem>()
                       .HasOne(u => u.Unit)
                       .WithMany(oi => oi.OrderedItems)
                       .HasForeignKey(f => f.UnitId)
                       .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<OrderedItem>()
            //            .Property(c => c.customerName)
            //            .IsRequired(true);
            //modelBuilder.Entity<OrderedItem>()
            //            .Property(q => q.Quantity)
            //            .IsRequired(true);
            //modelBuilder.Entity<OrderedItem>()
            //            .Property(s => s.Sub_Total)
            //            .IsRequired(true);


            //UnitItem
            modelBuilder.Entity<UnitItem>()
                      .HasKey(k => new { k.UnitId, k.ItemId});
            modelBuilder.Entity<UnitItem>()
                        .HasOne(u => u.Unit)
                        .WithMany(ui => ui.UnitItems)
                        .HasForeignKey(f => f.UnitId)
                        .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UnitItem>()
                       .HasOne(i => i.Item)
                       .WithMany(ui => ui.UnitItems)
                       .HasForeignKey(f => f.ItemId)
                       .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UnitItem>()
                        .Property(q => q.Quatity)
                        .IsRequired(true);
            modelBuilder.Entity<UnitItem>()
                       .Property(p => p.PricePerUnit)
                       .IsRequired(true);
        }
        #endregion


        //public DbSet<Student> Students { get; set; }
        //public DbSet<Course> Courses { get; set; }
        //public DbSet<ViewModelNew.Models.StudentCourse> StudentCourse { get; set; }
    }
}
