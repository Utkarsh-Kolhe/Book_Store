using Bussiness_Layer.InterfaceBL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model_Layer.Models;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInterfaceBL _userInterfaceBL;

        public UserController(IUserInterfaceBL userInterfaceBL)
        {
            _userInterfaceBL = userInterfaceBL;
        }

        [HttpPost]
        [Route("regestration")]
        public ResponseModel<UserRegistrationModel> Registration(UserRegistrationModel model)
        {
            ResponseModel<UserRegistrationModel> responseModel = new ResponseModel<UserRegistrationModel>();
            try
            {
                var result = _userInterfaceBL.Registration(model);

                if(result)
                {
                    responseModel.Message = "User registered successfully.";
                    responseModel.Data = model;
                }
                else
                {
                    responseModel.Success = false;
                    responseModel.Message = "User already exists.";
                }

            }
            catch (Exception ex)
            {
                responseModel.Success = false;
                responseModel.Message = ex.Message;
            }
            return responseModel;
        }

        [HttpPost]
        [Route("login")]
        public ResponseModel<string> Login(UserLoginModel model)
        {
            ResponseModel<string> responseModel = new ResponseModel<string>();
            try
            {
                var token = _userInterfaceBL.Login(model);

                if (token != null)
                {
                    responseModel.Message = "User logged in successfully.";
                    responseModel.Data = token;
                }
                else
                {
                    responseModel.Success = false;
                    responseModel.Message = "Invalid credentials.";
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
