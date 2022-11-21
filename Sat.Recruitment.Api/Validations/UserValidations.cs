using Sat.Recruitment.Api.Entities;
using System;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Validations
{
    public class UserValidations
    {
        //Validate errors
        public void ValidateErrors(User user, ref string errors)
        {
            if (String.IsNullOrWhiteSpace(user.Name))
                //Validate if Name is null
                errors = "The name is required";
            if (String.IsNullOrWhiteSpace(user.Email))
                //Validate if Email is null
                errors = errors + " The email is required";
            if (String.IsNullOrWhiteSpace(user.Email))
                //Validate if Address is null
                errors = errors + " The address is required";
            if (String.IsNullOrWhiteSpace(user.Phone))
                //Validate if Phone is null
                errors = errors + " The phone is required";
        }

        // Check if user exists
        public bool CheckDuplicated(List<User> users, User newUser)
        {
            foreach (var user in users)
            {
                if (user.Email == newUser.Email ||
                    user.Phone == newUser.Phone ||
                    user.Name == newUser.Name ||
                    user.Address == newUser.Address)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
