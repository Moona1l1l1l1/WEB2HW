using Microsoft.AspNetCore.Mvc;
using BooksApi.Models;

namespace BooksApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        // Хранилище книг в памяти (без базы данных)
        private static readonly List<Book> Books = new()
        {
            new Book { Id = 1, Title = "Война и мир", Author = "Лев Толстой", Year = 1869 },
            new Book { Id = 2, Title = "Преступление и наказание", Author = "Фёдор Достоевский", Year = 1866 }
        };

        // GET api/books
        // Возвращает список всех книг
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            return Ok(Books);
        }

        // GET api/books/5
        // Возвращает одну книгу по Id (дополнительно, для удобства)
        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // POST api/books
        // Принимает новую книгу в формате JSON и добавляет её в список
        [HttpPost]
        public ActionResult<Book> AddBook([FromBody] Book newBook)
        {
            newBook.Id = Books.Count > 0 ? Books.Max(b => b.Id) + 1 : 1;
            Books.Add(newBook);
            return CreatedAtAction(nameof(GetBook), new { id = newBook.Id }, newBook);
        }
    }
}
