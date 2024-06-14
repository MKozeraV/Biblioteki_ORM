using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using P7AppAPI.Data;

namespace P7AppAPI.Context;

public partial class BooksApiContext : DbContext
{
    public BooksApiContext()
    {
    }

    public BooksApiContext(DbContextOptions<BooksApiContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
