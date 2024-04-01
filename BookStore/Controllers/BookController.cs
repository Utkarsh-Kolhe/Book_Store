using Bussiness_Layer.InterfaceBL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model_Layer.Models;
using Repository_Layer.Entity;
using System.Security.Claims;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookInterfaceBL _bookInterfaceBL;

        public BookController(IBookInterfaceBL bookInterfaceBL)
        {
            _bookInterfaceBL = bookInterfaceBL;
        }

        [HttpPost]
        [Authorize(Roles ="admin")]
        public ResponseModel<BookModel> AddBook(BookModel bookModel)
        {
            var responseModel = new ResponseModel<BookModel>();
            try
            {
                var _adminId = User.FindFirstValue("UserId");
                int adminId = Convert.ToInt32(_adminId);

                var result = _bookInterfaceBL.AddBook(bookModel, adminId);

                if (result)
                {
                    responseModel.Message = "Book added successfully.";
                    responseModel.Data = bookModel;
                }
                else
                {
                    responseModel.Success = false;
                    responseModel.Message = "Book did not added.";
                }
            }
            catch (Exception ex)
            {
                responseModel.Success = false;
                responseModel.Message = ex.Message;
            }
            return responseModel;
        }

        [HttpGet]
        public ResponseModel<List<BookEntity>> GetAllBooks()
        {
            var responseModel = new ResponseModel<List<BookEntity>>();
            try
            {
                var data = _bookInterfaceBL.GetAllBooks();
                if(data.Count != 0)
                {
                    responseModel.Message = "Books retrived successfully.";
                    responseModel.Data = data;
                }
                else
                {
                    responseModel.Success = false;
                    responseModel.Message = "There is no book.";
                }
            }
            catch (Exception ex)
            {
                responseModel.Success = false;
                responseModel.Message = ex.Message;
            }
            return responseModel;
        }

        [HttpGet("{bookId}")]
        public ResponseModel<BookEntity> GetBook(int bookId)
        {
            var responseModel = new ResponseModel<BookEntity>();
            try
            {
                var book = _bookInterfaceBL.GetBook(bookId);
                if(book != null)
                {
                    responseModel.Message = "Book retrived successfully.";
                    responseModel.Data = book;
                }
                else
                {
                    responseModel.Success = false;
                    responseModel.Message = "There is no book.";
                }
            }
            catch (Exception ex)
            {
                responseModel.Success = false;
                responseModel.Message = ex.Message;
            }
            return responseModel;
        }

        [HttpPut("{bookId}")]
        [Authorize(Roles ="admin")]
        public ResponseModel<BookEntity> EditBookDetails(int bookId,  BookModel model)
        {
            var responseModel = new ResponseModel<BookEntity>();
            try
            {
                var result = _bookInterfaceBL.EditBookDetails(bookId, model);

                if(result != null)
                {
                    responseModel.Message = "Book edited successfully.";
                    responseModel.Data = result;
                }
                else
                {
                    responseModel.Success = false;
                    responseModel.Message = "Book not found";
                }
            }
            catch(Exception ex)
            {
                responseModel.Success= false;
                responseModel.Message = ex.Message;
            }
            return responseModel;
        }

        [HttpDelete("{bookId}")]
        [Authorize(Roles = "admin")]
        public ResponseModel<string> DeleteBook(int bookId)
        {
            var responseModel = new ResponseModel<string>();
            try
            {
                var result = _bookInterfaceBL.DeleteBook(bookId);

                if(result)
                {
                    responseModel.Message = "Book Delete Successfully.";
                }
                else
                {
                    responseModel.Success = false;
                    responseModel.Message = "Book not found.";
                }
            }
            catch (Exception ex)
            {
                responseModel.Success = false;
                responseModel.Message = ex.Message;
            }
            return responseModel;
        }
    }
}
