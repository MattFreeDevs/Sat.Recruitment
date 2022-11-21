using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sat.Recruitment.Api.Entities;
using Sat.Recruitment.Api.Repositories;
using Sat.Recruitment.Api.Validations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sat.Recruitment.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserValidations _userValidations;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _userValidations = new UserValidations();
        }

        #region CRUD Methods
        public Result CreateUser(User user)
        {
            //Validate inputs
            var errors = "";
            _userValidations.ValidateErrors(user, ref errors);

            if (!String.IsNullOrEmpty(errors))
                return new Result()
                {
                    IsSuccess = false,
                    Errors = errors
                };

            //Add bussiness logic
            user.Money = AddMoney(user);
            user.Email = NormalizeEmail(user);

            var _users = _userRepository.GetAll();

            //Check if the user already exists
            if (!_userValidations.CheckDuplicated(_users, user))
            {
                //Here we would add the User to the ddbb

                //return _userRepository.CreateUser(user);

                Debug.WriteLine("User Created");

                return new Result()
                {
                    IsSuccess = true,
                    Errors = "User Created"
                };
            }
            else
            {
                Debug.WriteLine("The user is duplicated");

                return new Result()
                {
                    IsSuccess = false,
                    Errors = "The user is duplicated"
                };
            }
        }
        #endregion

        #region Bussiness Process
        private decimal AddMoney(User user)
        {
            var money = Convert.ToDecimal(user.Money);
            switch (user.UserType)
            {
                case "Normal":
                    if (money > 100)
                        AddPercentage(0.12, ref money);
                    if (money < 100 && money > 10)
                        AddPercentage(0.8, ref money);
                    return money;
                case "SuperUser":
                    if (money > 100)
                        AddPercentage(0.20, ref money);
                    return money;
                case "Premium":
                    if (money > 100)
                        AddPercentage(2.00, ref money);
                    return money;
                default:
                    return money;
            }
            
        }

        private void AddPercentage(double perc, ref decimal money)
        {
            var percentage = Convert.ToDecimal(perc);
            var gif = money * percentage;
            money = money + gif;
        }

        private string NormalizeEmail(User user)
        {
            //Normalize email
            var aux = user.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            user.Email = string.Join("@", new string[] { aux[0], aux[1] });
            return user.Email;
        }
        #endregion
    }
}
