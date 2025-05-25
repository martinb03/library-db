using System;
using System.Collections.Generic;
using LIbraryUI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LIbraryUI.Data;

public partial class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<AuthorsBook> AuthorsBooks { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookCondition> BookConditions { get; set; }

    public virtual DbSet<BookStatus> BookStatuses { get; set; }

    public virtual DbSet<Borrowing> Borrowings { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<GenresBook> GenresBooks { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<ViewBook> ViewBooks { get; set; }

    public virtual DbSet<ViewCustomer> ViewCustomers { get; set; }

    public virtual DbSet<ViewCustomerBorrowing> ViewCustomerBorrowings { get; set; }

    public virtual DbSet<ViewOverdue> ViewOverdues { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorsId).HasName("authors_pkey");

            entity.ToTable("authors");

            entity.HasIndex(e => e.Name, "authors_name_key").IsUnique();

            entity.Property(e => e.AuthorsId).HasColumnName("authors_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<AuthorsBook>(entity =>
        {
            entity.HasKey(e => new { e.AuthorId, e.BookId }).HasName("authors_books_pkey");

            entity.ToTable("authors_books");

            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.IsMainAuthor)
                .HasDefaultValue(false)
                .HasColumnName("is_main_author");

            entity.HasOne(d => d.Author).WithMany(p => p.AuthorsBooks)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("authors_books_author_id_fkey");

            entity.HasOne(d => d.Book).WithMany(p => p.AuthorsBooks)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("authors_books_book_id_fkey");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("books_pkey");

            entity.ToTable("books");

            entity.HasIndex(e => e.Isbn, "books_isbn_key").IsUnique();

            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.ConditionId).HasColumnName("condition_id");
            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .HasColumnName("isbn");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.Condition).WithMany(p => p.Books)
                .HasForeignKey(d => d.ConditionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("books_condition_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Books)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("books_status_id_fkey");
        });

        modelBuilder.Entity<BookCondition>(entity =>
        {
            entity.HasKey(e => e.ConditionId).HasName("book_condition_pkey");

            entity.ToTable("book_condition");

            entity.HasIndex(e => e.Name, "book_condition_name_key").IsUnique();

            entity.Property(e => e.ConditionId).HasColumnName("condition_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<BookStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("book_status_pkey");

            entity.ToTable("book_status");

            entity.HasIndex(e => e.Name, "book_status_name_key").IsUnique();

            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Borrowing>(entity =>
        {
            entity.HasKey(e => e.BorrowingId).HasName("borrowings_pkey");

            entity.ToTable("borrowings");

            entity.Property(e => e.BorrowingId).HasColumnName("borrowing_id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.BorrowDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("borrow_date");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.DueDate).HasColumnName("due_date");
            entity.Property(e => e.ReturnDate).HasColumnName("return_date");

            entity.HasOne(d => d.Book).WithMany(p => p.Borrowings)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("borrowings_book_id_fkey");

            entity.HasOne(d => d.Customer).WithMany(p => p.Borrowings)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("borrowings_customer_id_fkey");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("customers_pkey");

            entity.ToTable("customers");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.ContactInfo)
                .HasMaxLength(255)
                .HasColumnName("contact_info");
            entity.Property(e => e.MembershipDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("membership_date");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("genres_pkey");

            entity.ToTable("genres");

            entity.HasIndex(e => e.Name, "genres_name_key").IsUnique();

            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<GenresBook>(entity =>
        {
            entity.HasKey(e => new { e.GenreId, e.BookId }).HasName("genres_books_pkey");

            entity.ToTable("genres_books");

            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.IsMainGenre)
                .HasDefaultValue(false)
                .HasColumnName("is_main_genre");

            entity.HasOne(d => d.Book).WithMany(p => p.GenresBooks)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("genres_books_book_id_fkey");

            entity.HasOne(d => d.Genre).WithMany(p => p.GenresBooks)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("genres_books_genre_id_fkey");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("staff_pkey");

            entity.ToTable("staff");

            entity.HasIndex(e => e.User, "staff_user_key").IsUnique();

            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.IsAdmin)
                .HasDefaultValue(false)
                .HasColumnName("is_admin");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.User)
                .HasMaxLength(100)
                .HasColumnName("user");
        });

        modelBuilder.Entity<ViewBook>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_books");

            entity.Property(e => e.Authors).HasColumnName("authors");
            entity.Property(e => e.Condition)
                .HasMaxLength(255)
                .HasColumnName("condition");
            entity.Property(e => e.Genres).HasColumnName("genres");
            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .HasColumnName("isbn");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
        });

        modelBuilder.Entity<ViewCustomer>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_customers");

            entity.Property(e => e.ActiveBorrowings).HasColumnName("active_borrowings");
            entity.Property(e => e.ContactInfo)
                .HasMaxLength(255)
                .HasColumnName("contact_info");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.MembershipDate).HasColumnName("membership_date");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ViewCustomerBorrowing>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_customer_borrowings");

            entity.Property(e => e.BookTitle)
                .HasMaxLength(255)
                .HasColumnName("book_title");
            entity.Property(e => e.BorrowDate).HasColumnName("borrow_date");
            entity.Property(e => e.BorrowingId).HasColumnName("borrowing_id");
            entity.Property(e => e.Condition)
                .HasMaxLength(255)
                .HasColumnName("condition");
            entity.Property(e => e.Customer)
                .HasMaxLength(255)
                .HasColumnName("customer");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.DaysOverdue).HasColumnName("days_overdue");
            entity.Property(e => e.DueDate).HasColumnName("due_date");
            entity.Property(e => e.IsOverdue).HasColumnName("is_overdue");
            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .HasColumnName("isbn");
            entity.Property(e => e.ReturnDate).HasColumnName("return_date");
        });

        modelBuilder.Entity<ViewOverdue>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_overdue");

            entity.Property(e => e.Book)
                .HasMaxLength(255)
                .HasColumnName("book");
            entity.Property(e => e.BorrowDate).HasColumnName("borrow_date");
            entity.Property(e => e.BorrowingId).HasColumnName("borrowing_id");
            entity.Property(e => e.Customer)
                .HasMaxLength(255)
                .HasColumnName("customer");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.DaysOverdue).HasColumnName("days_overdue");
            entity.Property(e => e.DueDate).HasColumnName("due_date");
            entity.Property(e => e.IsOverdue).HasColumnName("is_overdue");
            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .HasColumnName("isbn");
            entity.Property(e => e.ReturnDate).HasColumnName("return_date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
