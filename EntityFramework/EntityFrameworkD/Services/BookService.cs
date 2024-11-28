using Microsoft.EntityFrameworkCore;
using P7AppAPI.Context;
using P7AppAPI.Data;
using P7AppAPI.Models;
using System.Diagnostics;
using System.Reflection;

namespace P7AppAPI.Services
{
    public class BookService : IBookService
    {
        private readonly BooksApiContext _context;
        public BookService(BooksApiContext context)
        {
            _context = context;
        }
        public async Task<MainResponse> AddBook(BookDTO bookDTO)
        {
            var response = new MainResponse();
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                for (int i = 15; i < 1015; i++)
                {
                    await _context.AddAsync(new Book
                {
                    Author=bookDTO.Author,
                    Title=bookDTO.Title,
                });
                
                    await _context.SaveChangesAsync();
                }
                sw.Stop();
                double time = (double)sw.ElapsedMilliseconds / 1000;
                response.Content = "Średni czas wykonania - " + time.ToString();
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }
            return response;

        }

        public async Task<MainResponse> DeleteBook(DeleteBookDTO bookDTO)
        {
            var response = new MainResponse();
            try
            {
                response.ErrorMessage = "jg";
                Stopwatch sw = new Stopwatch();
                sw.Start();
                for (int i = 3002; i < 4002; i++)
                {
                    var existingBook = _context.Books.Where(f => f.Id == i).FirstOrDefault();
                    if (existingBook != null)
                    {
                        _context.Remove(existingBook);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        Console.WriteLine("kurwa mac - " + i);
                        response.IsSuccess=false;
                        response.ErrorMessage = "nmk";
                        break;
                    }
                }
                sw.Stop();
                if (response.ErrorMessage=="jg")
                {
                    double time = (double)sw.ElapsedMilliseconds / 1000;
                    response.Content = "Średni czas wykonania - " + time.ToString();
                    response.IsSuccess = true;
                }

            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<MainResponse> GetAllBook()
        {
            var response = new MainResponse();
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                
                for (int i = 15; i < 1015;i++)
                {
                      response.Content = await _context.Books.ToListAsync();
                }
                sw.Stop();
                double time = (double)sw.ElapsedMilliseconds / 1000;
                //response.Content="/nŚredni czas wykonania - "+time.ToString()+"\n";
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }
            return response;
        }



        public async Task<MainResponse> GetBookById(int id)
        {
            var response = new MainResponse();
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                for (int i = 0; i < 1000; i++)
                {
                    await _context.Books.Where(f => f.Id == id).FirstOrDefaultAsync();
                }
                sw.Stop();
                double time = (double)sw.ElapsedMilliseconds / 1000;
                response.Content = "Średni czas wykonania - " + time.ToString() + "\n";
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<MainResponse> UpdateBook(BookDTO bookDTO)
        {
            var response = new MainResponse();
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                for (int i = 0; i < 1000; i++)
                {
                    var existingBook = _context.Books.Where(f => f.Id == bookDTO.Id).FirstOrDefault();
                    if (existingBook != null)
                    {
                        existingBook.Title = bookDTO.Title;
                        existingBook.Author = bookDTO.Author;
                        await _context.SaveChangesAsync();
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Content = "No book with this id";
                    }
                }
                sw.Stop();
                double time = (double)sw.ElapsedMilliseconds / 1000;
                response.Content = "Średni czas wykonania - " + time.ToString();

            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }
            return response;

        }

        public async Task<MainResponse> AddBookTransaction(BookDTO bookDTO)
        {
            var response = new MainResponse();
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                
                    for (int i = 15; i < 1015; i++)
                    {
                    using var transakcja = _context.Database.BeginTransaction();
                    try
                    {
                        await _context.AddAsync(new Book
                        {
                            Author = bookDTO.Author,
                            Title = bookDTO.Title,
                        });

                        await _context.SaveChangesAsync();
                    }
                    catch
                    {
                        transakcja.Rollback();
                        throw;
                    }

                    }
                
                sw.Stop();
                double time = (double)sw.ElapsedMilliseconds / 1000;
                response.Content = "Średni czas wykonania - " + time.ToString();
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }
            return response;

        }
    }
}
