using Bussiness_Layer.InterfaceBL;
using Model_Layer.Models;
using Repository_Layer.InterfaceRL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.ServiceBL
{
    public class UserServiceBL : IUserInterfaceBL
    {
        private readonly IUserInterfaceRL _userInterfaceRL;

        public UserServiceBL(IUserInterfaceRL userInterfaceRL)
        {
            _userInterfaceRL = userInterfaceRL;
        }

        public bool Registration(UserRegistrationModel model)
        {
            return _userInterfaceRL.Registration(model);
        }

        public string Login(UserLoginModel model)
        {
            return _userInterfaceRL.Login(model);
        }
    }
}
