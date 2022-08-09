using app23Receiver.DataContext;
using app23Receiver.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app23Receiver
{
    public class ValidatorAndFlagChanger
    {
        private User userToCheck;
        public ValidatorAndFlagChanger(User userToCheck)
        {
            this.userToCheck = userToCheck;
        }

        public bool CheckingParameters()
        {
            UserValidator userValidator = new UserValidator();
            if (string.IsNullOrEmpty(userToCheck.Name) == false && string.IsNullOrEmpty(userToCheck.Surname) == false && string.IsNullOrEmpty(userToCheck.Email) == false && userToCheck.Age != 0 && userValidator.Validate(userToCheck).IsValid == true)
            {
                FlagChanger();
                return true;
            } else
            {
                return false;
            }
        }

        public void FlagChanger()
        {
            using (var results = new UserDbContext())
            {
                var userToEdit = results.Users.FirstOrDefault(x => x.Email == userToCheck.Email);
                if (userToEdit != null)
                {
                    userToEdit.IsActive = true;
                }
                results.SaveChanges();
            }
        }
    }
}
