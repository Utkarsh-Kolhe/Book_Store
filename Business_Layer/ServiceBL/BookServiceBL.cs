using Bussiness_Layer.InterfaceBL;
using Model_Layer.Models;
using Repository_Layer.Entity;
using Repository_Layer.InterfaceRL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.ServiceBL
{
    public class BookServiceBL : IBookInterfaceBL
    {
        private readonly IBookInterfaceRL _bookInterfaceRL;

        public BookServiceBL(IBookInterfaceRL bookInterfaceRL)
        {
            _bookInterfaceRL = bookInterfaceRL;
        }
        public bool AddBook(BookModel model, int adminId)
        {
            return _bookInterfaceRL.AddBook(model, adminId);
        }

        public List<BookEntity> GetAllBooks()
        {
            return _bookInterfaceRL.GetAllBooks();
        }

        public BookEntity GetBook(int bookId)
        {
            return _bookInterfaceRL.GetBook(bookId);
        }

        public BookEntity EditBookDetails(int bookId, BookModel model)
        {
            return _bookInterfaceRL.EditBookDetails(bookId, model);
        }

        public bool DeleteBook(int bookId)
        {
            return _bookInterfaceRL.DeleteBook(bookId);
        }
    }
}
