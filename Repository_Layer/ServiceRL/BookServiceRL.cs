using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model_Layer.Models;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.InterfaceRL;

namespace Repository_Layer.ServiceRL
{
    public class BookServiceRL : IBookInterfaceRL
    {
        private readonly BookStoreContext _context;

        public BookServiceRL(BookStoreContext context)
        {
            _context = context;
        }

        public bool AddBook(BookModel model, int adminId)
        {
            BookEntity bookEntity = new BookEntity();

            bookEntity.BookName = model.BookName;
            bookEntity.Author = model.Author;
            bookEntity.Price = Convert.ToInt32(model.Price);
            bookEntity.Quantity = Convert.ToInt32(model.Quantity);
            bookEntity.AdminId = adminId;

            _context.Books.Add(bookEntity);
            _context.SaveChanges();

            return true;
        }

        public List<BookEntity> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public BookEntity GetBook(int bookId)
        {
            var book =  _context.Books.FirstOrDefault(x => x.BookId == bookId);

            if (book != null)
            {
                return book;
            }

            return null;

        }

        public BookEntity EditBookDetails(int bookId, BookModel model)
        {
            var book = _context.Books.FirstOrDefault(e => e.BookId == bookId);
            if (book != null)
            {
                book.BookName = IsEmpty(model.BookName, book.BookName);
                book.Author = IsEmpty(model.Author, book.Author);
                book.Price = Convert.ToInt32(IsEmpty(Convert.ToString(model.Price), Convert.ToString(book.Price)));
                book.Quantity = Convert.ToInt32(IsEmpty(Convert.ToString(model.Quantity), Convert.ToString(book.Quantity)));

                _context.SaveChanges();

                return book;
            }
            return null;
        }

        public bool DeleteBook(int bookId)
        {
            var book = _context.Books.FirstOrDefault(e => e.BookId == bookId);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();

                return true;
            }
            return false;
        }

        public string IsEmpty(string newString, string oldString)
        {
            if(newString.Length == 0)
            {
                return oldString;
            }
            return newString;
        }
    }
}
