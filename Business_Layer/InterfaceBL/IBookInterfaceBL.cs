﻿using Model_Layer.Models;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.InterfaceBL
{
    public interface IBookInterfaceBL
    {
        public bool AddBook(BookModel model, int adminId);

        public List<BookEntity> GetAllBooks();

        public BookEntity GetBook(int bookId);

        public BookEntity EditBookDetails(int bookId, BookModel model);

        public bool DeleteBook(int bookId);
    }
}
