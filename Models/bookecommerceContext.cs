using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace bookecommercewebsite.Models
{
    public partial class bookecommerceContext : DbContext
    {
        public bookecommerceContext()
        {
        }

        public bookecommerceContext(DbContextOptions<bookecommerceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Bookcat> Bookcats { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-OQ9O9RQL;Database=bookecommerce;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("book");

                entity.Property(e => e.Bookid).HasColumnName("bookid");

                entity.Property(e => e.Bookauthor)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("bookauthor");

                entity.Property(e => e.Bookcatid).HasColumnName("bookcatid");

                entity.Property(e => e.Bookimage).HasColumnName("bookimage");

                entity.Property(e => e.Bookname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("bookname");

                entity.Property(e => e.Bookprice)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("bookprice");
            });

            modelBuilder.Entity<Bookcat>(entity =>
            {
                entity.ToTable("bookcat");

                entity.Property(e => e.Bookcatid).HasColumnName("bookcatid");

                entity.Property(e => e.Bookcatname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("bookcatname");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .HasColumnName("role");

                entity.Property(e => e.Useraddress)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("useraddress");

                entity.Property(e => e.Usercontact)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("usercontact");

                entity.Property(e => e.Useremail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("useremail");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("username");

                entity.Property(e => e.Userpassword)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("userpassword");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
