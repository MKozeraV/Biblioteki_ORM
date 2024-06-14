using Microsoft.EntityFrameworkCore;
using P7AppAPI.Context;
using P7AppAPI.Data;
using P7AppAPI.Models;
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
                
                await _context.AddAsync(new Book
                {
                    Author=bookDTO.Author,
                    Title=bookDTO.Title,
                });

                await _context.SaveChangesAsync();
                response.IsSuccess = true;
                response.Content = "Book added";
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
                var existingBook = _context.Books.Where(f => f.Id == bookDTO.Id).FirstOrDefault();
                if (existingBook != null)
                {
                    _context.Remove(existingBook);
                    await _context.SaveChangesAsync();
                    response.IsSuccess = true;
                    response.Content = "Record deleted";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Content = "No book with this id";
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
                response.Content = await _context.Books.ToListAsync();
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
                response.Content = await _context.Books.Where(f=>f.Id == id).FirstOrDefaultAsync();
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
                var existingBook = _context.Books.Where(f=>f.Id == bookDTO.Id).FirstOrDefault();
                if(existingBook != null)
                {
                    existingBook.Title = bookDTO.Title;
                    existingBook.Author = bookDTO.Author;
                    await _context.SaveChangesAsync();
                    response.IsSuccess = true;
                    response.Content = "Record updated";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Content = "No book with this id";
                }


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
