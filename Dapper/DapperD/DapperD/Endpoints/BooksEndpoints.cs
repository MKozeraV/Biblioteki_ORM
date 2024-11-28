using Dapper;
using DapperD.Models;
using DapperD.Services;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace DapperD.Endpoints
{
    public static class BooksEndpoints
    {
        public static void MapBooksEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapGet("books", async (SqlConnectionFactory sqlConnectionFactory) =>
            {
                Stopwatch sw = new Stopwatch();
                using var connection = sqlConnectionFactory.Create();
                const string sql = "SELECT * FROM Books";
                sw.Start();
                for (int i = 0; i < 10000; i++)
                {
                    var books = await connection.QueryAsync<Book>(sql);
                }
                sw.Stop();
                double time = (double)sw.ElapsedMilliseconds/10000;

                return Results.Ok("Sredni czas wykonania operacji w milisekundach - "+time);
            });

            builder.MapGet("books/{id}", async (int id, SqlConnectionFactory sqlConnectionFactory)=>
            {

                using var connection = sqlConnectionFactory.Create();

                const string sql = "SELECT * FROM Books WHERE id_k = @BookID";

                var book = await connection.QuerySingleOrDefaultAsync<Book>(sql,
                    new {BookID = id});

                return book is not null ? Results.Ok(book) : Results.NotFound();
            });

            builder.MapGet("booksTime/{id}", async (int id, SqlConnectionFactory sqlConnectionFactory) =>
            {
                Stopwatch sw = new Stopwatch();
                using var connection = sqlConnectionFactory.Create();

                const string sql = "SELECT * FROM Books WHERE id_k = @BookID";
                sw.Start();
                for (int i = 0; i < 10000; i++)
                {
                    var book = await connection.QuerySingleOrDefaultAsync<Book>(sql,
                        new { BookID = id });
                }
                sw.Stop();
                double time = (double)sw.ElapsedMilliseconds / 10000;

                return Results.Ok("Sredni czas wykonania operacji w milisekundach - " + time);
            });

            builder.MapGet("booksCountTime", async (SqlConnectionFactory sqlConnectionFactory) =>
            {
                Stopwatch sw = new Stopwatch();
                using var connection = sqlConnectionFactory.Create();

                const string sql = "SELECT COUNT(*) FROM Books";
                sw.Start();
                var books = 0;
                for (int i = 0; i < 10000; i++)
                {
                    books = await connection.ExecuteScalarAsync<int>(sql);
                }
                sw.Stop();
                double time = (double)sw.ElapsedMilliseconds / 10000;
                
                return Results.Ok("Sredni czas wykonania operacji w milisekundach - " + time);
            });

            builder.MapPost("books", async (Book book, SqlConnectionFactory sqlConnectionFactory) =>
            {

                using var connection = sqlConnectionFactory.Create();

            const string sql = """
                INSERT INTO Books 
                (id_k,nazwa,autor,gatunek)
                VALUES
                (@id_k,@nazwa,@autor,@gatunek)
                """;
            await connection.ExecuteAsync(sql, book);

                return Results.Ok();
            });
            builder.MapPost("booksTime", async (Book book, SqlConnectionFactory sqlConnectionFactory) =>
            {
                Stopwatch sw = new Stopwatch();
                using var connection = sqlConnectionFactory.Create();
                sw.Start();
                for (int i = 10; i < 10010; i++)
                {   
                    book.id_k = i;
                    const string sql = """
                INSERT INTO Books 
                (id_k,nazwa,autor,gatunek)
                VALUES
                (@id_k,@nazwa,@autor,@gatunek)
                """;
                    await connection.ExecuteAsync(sql, book);
                }
                sw.Stop();
                double time = (double)sw.ElapsedMilliseconds / 10000;

                return Results.Ok("Sredni czas wykonania operacji w milisekundach - " + time);
            });

            builder.MapPost("booksTimeTransaction", async (Book book, SqlConnectionFactory sqlConnectionFactory) =>
            {
                Stopwatch sw = new Stopwatch();
                using var connection = sqlConnectionFactory.Create();
                connection.Open();
                sw.Start();
                for (int i = 10; i < 10010; i++)
                {
                    using (var tran = connection.BeginTransaction())
                    { 
                    book.id_k = i;
                    const string sql = """
                INSERT INTO Books 
                (id_k,nazwa,autor,gatunek)
                VALUES
                (@id_k,@nazwa,@autor,@gatunek)
                """;
                    await connection.ExecuteAsync(sql, book, tran);
                    await tran.CommitAsync();
                        
                }
                }
                sw.Stop();
                double time = (double)sw.ElapsedMilliseconds / 10000;

                return Results.Ok("Sredni czas wykonania operacji w milisekundach - " + time);
            });

            builder.MapPut("books/{id}", async (int id,Book book, SqlConnectionFactory sqlConnectionFactory) =>
            {
                book.id_k = id;
                using var connection = sqlConnectionFactory.Create();

                const string sql = """
                UPDATE Books 
                SET nazwa = @nazwa, autor=@autor, gatunek=@gatunek
                WHERE
                id_k=@id_k
                """;

                await connection.ExecuteAsync(sql, book);


                return Results.NoContent();
            });

            builder.MapPut("booksTime/{id}", async (int id, Book book, SqlConnectionFactory sqlConnectionFactory) =>
            {
                Stopwatch sw = new Stopwatch();
                book.id_k = id;
                using var connection = sqlConnectionFactory.Create();

                const string sql = """
                UPDATE Books 
                SET nazwa = @nazwa, autor=@autor, gatunek=@gatunek
                WHERE
                id_k=@id_k
                """;
                sw.Start();
                for (int i = 0; i < 10000; i++)
                {
                    await connection.ExecuteAsync(sql, book);
                }
                sw.Stop();

                double time = (double)sw.ElapsedMilliseconds / 10000;

                return Results.Ok("Sredni czas wykonania operacji w milisekundach - " + time);
            });

            builder.MapDelete("books/{id}", async (int id, SqlConnectionFactory sqlConnectionFactory) =>
            {

                using var connection = sqlConnectionFactory.Create();

                const string sql = "DELETE FROM Books WHERE id_k = @BookID";

                await connection.ExecuteAsync(sql,new { BookID = id });

                return Results.NoContent();
            });

            builder.MapDelete("booksTime/{id}", async (int id, SqlConnectionFactory sqlConnectionFactory) =>
            {
                Stopwatch sw = new Stopwatch();
                using var connection = sqlConnectionFactory.Create();
                sw.Start();

                for (int i = 10; i < 10010; i++)
                {

                    string sql = String.Concat("DELETE FROM Books WHERE id_k = ",i);
                    Console.WriteLine(sql);
                    await connection.ExecuteAsync(sql, new { BookID = id });
                }
                sw.Stop();
                double time = (double)sw.ElapsedMilliseconds / 10000;

                return Results.Ok("Sredni czas wykonania operacji w milisekundach - " + time);
            });



        }
    }
}
