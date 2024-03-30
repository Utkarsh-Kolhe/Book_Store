using Model_Layer.Models;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Hashing;
using Repository_Layer.InterfaceRL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.ServiceRL
{
    public class UserServiceRL : IUserInterfaceRL
    {
        private readonly BookStoreContext _bookStoreContext;
        private readonly HashingPassword _password;

        public UserServiceRL(BookStoreContext bookStoreContext, HashingPassword password)
        {
            _bookStoreContext = bookStoreContext;
            _password = password;
        }

        public bool Registration(UserRegistrationModel model)
        {
            var user = _bookStoreContext.UsersRegistration.FirstOrDefault(e => e.EmailAddress == model.EmailAddress);
            if (user == null)
            {
                UserRegistrationEntity userRegistrationEntity = new UserRegistrationEntity();

                userRegistrationEntity.FirstName = model.FirstName;
                userRegistrationEntity.LastName = model.LastName;
                userRegistrationEntity.EmailAddress = model.EmailAddress;
                userRegistrationEntity.Password = _password.HashPassword(model.Password);

                _bookStoreContext.UsersRegistration.Add(userRegistrationEntity);
                _bookStoreContext.SaveChanges();

                return true;
            }

            return false; // if user is already exists.
        }
    }
}
