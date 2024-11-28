using P7AppAPI.Models;

namespace P7AppAPI.Services
{
    public interface IBookService
    {
        Task<MainResponse> AddBook(BookDTO bookDTO);
        Task<MainResponse> AddBookTransaction(BookDTO bookDTO);

        Task<MainResponse> UpdateBook(BookDTO bookDTO);

        Task<MainResponse> DeleteBook(DeleteBookDTO bookDTO);

        Task<MainResponse> GetAllBook();

        Task<MainResponse> GetBookById(int id);
    }
}
