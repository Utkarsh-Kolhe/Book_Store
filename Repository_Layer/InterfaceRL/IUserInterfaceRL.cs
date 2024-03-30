using Model_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.InterfaceRL
{
    public interface IUserInterfaceRL
    {
        public bool Registration(UserRegistrationModel model);

        public string Login(UserLoginModel model);
    }
}
